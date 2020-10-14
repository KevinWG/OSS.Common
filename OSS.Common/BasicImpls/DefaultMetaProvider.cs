using System;
using System.Threading;
using System.Threading.Tasks;
using OSS.Common.BasicMos.Resp;

namespace OSS.Common.BasicImpls
{
    /// <summary>
    /// 元配置信息提供接口定义
    /// </summary>
    /// <typeparam name="TMetaType">元配置信息类型</typeparam>
    public interface IMetaProvider<TMetaType>
        where TMetaType : class
    {
        /// <summary>
        ///  获取元配置数据的接口方法
        /// </summary>
        /// <returns></returns>
        public Task<Resp<TMetaType>> GetMeta();
    }

    /// <summary>
    /// 元数据缓存类型
    /// </summary>
    public enum CustomMetaCacheType
    {
        /// <summary>
        ///  无处理
        /// </summary>
        None,

        /// <summary>
        ///  当前线程内缓存
        /// </summary>
        ThreadStatic,

        /// <summary>
        ///  上下文（请求线程链下）内缓存
        /// </summary>
        AsyncLocal,
    }

    /// <summary>
    ///  带默认Meta属性的MetaProvider实现
    /// </summary>
    /// <typeparam name="TMetaType"></typeparam>
    public class DefaultMetaProvider<TMetaType> : IMetaProvider<TMetaType>
        where TMetaType : class
    {
        /// <summary>
        ///  默认Meta信息，优先级低于GetCustomMeta方法返回值
        /// </summary>
        public TMetaType DefaultMeta { get; set; }

        /// <summary>
        ///  通过 GetCustomMeta 方法获取的结果缓存类型
        /// </summary>
        public CustomMetaCacheType CacheType { get; protected set; } = CustomMetaCacheType.ThreadStatic;

        [ThreadStatic]
        private static TMetaType _threadStaticMeta;
        private static AsyncLocal<TMetaType>  _asyncLocalMeta;

        /// <inheritdoc />
        public async Task<Resp<TMetaType>> GetMeta()
        {
            var metaRes = await GetCustomMetaWithCache();
            if (metaRes != null)
            {
                return metaRes;
            }

            return DefaultMeta != null ? new Resp<TMetaType>(DefaultMeta)
                : new Resp<TMetaType>().WithResp(RespTypes.ObjectNull, "未发现任何配置信息，请重写GetCustomMeta方法，或配置DefaultMeta！");
        }

        private async Task<Resp<TMetaType>> GetCustomMetaWithCache()
        {
            if (CacheType == CustomMetaCacheType.ThreadStatic && _threadStaticMeta != null)
                return new Resp<TMetaType>(_threadStaticMeta);

            if (CacheType == CustomMetaCacheType.AsyncLocal && _asyncLocalMeta?.Value != null)
                return new Resp<TMetaType>(_asyncLocalMeta.Value);

            var metaRes = await GetCustomMeta();
            if (metaRes == null)
                return null;

            if (!metaRes.IsSuccess()) 
                return metaRes;

            if (CacheType == CustomMetaCacheType.ThreadStatic)
                _threadStaticMeta = metaRes.data;

            if (CacheType != CustomMetaCacheType.AsyncLocal) return metaRes;

            _asyncLocalMeta       ??= new AsyncLocal<TMetaType>();
            _asyncLocalMeta.Value =   metaRes.data;
            return metaRes;
        }

        /// <summary>
        ///  如果此方法返回不为【空】，优先返回
        /// </summary>
        /// <returns></returns>
        protected virtual Task<Resp<TMetaType>> GetCustomMeta()
        {
            return Task.FromResult<Resp<TMetaType>>(null);
        }
    }
}