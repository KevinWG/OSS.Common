using System;
using System.Threading;
using System.Threading.Tasks;
using OSS.Common.Resp;

namespace OSS.Common.BasicImpls
{
    /// <summary>
    /// 元配置信息提供接口定义
    /// </summary>
    /// <typeparam name="TMetaType">元配置信息类型</typeparam>
    [Obsolete("废弃，属于过度设计，引入问题大于实际带来的效果！")]
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
    [Obsolete("废弃，属于过度设计，引入问题大于实际带来的效果！")]
    public class DefaultMetaProvider<TMetaType> : IMetaProvider<TMetaType>
        where TMetaType : class
    {
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
            return metaRes ?? new Resp<TMetaType>().WithResp(RespTypes.OperateObjectNull, "未发现任何配置信息，请重写GetCustomMeta方法，或配置DefaultMeta！");
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