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
    ///  通用配置实现基类
    /// </summary>
    public abstract class BaseMetaImpl<TMetaType>
        where TMetaType : class
    {
        private readonly IMetaProvider<TMetaType> _metaProvider;

        /// <summary>
        /// 构造函数
        /// </summary>
        protected BaseMetaImpl()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="metaProvider">动态的配置提供者</param>
        protected BaseMetaImpl(IMetaProvider<TMetaType> metaProvider)
        {
            _metaProvider = metaProvider;
        }

        /// <summary>
        /// 获取当前元配置信息
        /// </summary>
        /// <returns></returns>
        public Task<Resp<TMetaType>> GetMeta()
        {
            if (_metaProvider != null)
            {
                return _metaProvider.GetMeta();
            }

            var defaultMeta = GetDefaultMeta();
            return Task.FromResult(new Resp<TMetaType>(defaultMeta));
        }


        #region 扩展虚方法

        /// <summary>
        /// 获取默认配置信息（如：静态固定的配置信息）
        /// </summary>
        /// <returns></returns>
        protected virtual TMetaType GetDefaultMeta()
        {
            return null;
        }

        #endregion
    }
}