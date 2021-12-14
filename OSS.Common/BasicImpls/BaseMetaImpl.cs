using System;
using System.Threading.Tasks;
using OSS.Common.Resp;

namespace OSS.Common.BasicImpls
{
    /// <summary>
    ///  通用配置实现基类
    /// </summary>
    [Obsolete("废弃，属于过度设计，引入问题大于实际带来的效果！")]
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
            return Task.FromResult(defaultMeta != null
                ? new Resp<TMetaType>(defaultMeta)
                : new Resp<TMetaType>().WithResp(RespTypes.OperateObjectNull,
                    "未发现任何配置信息,你可在构造函数中注入IMetaProvider实现，也可以重写GetDefaultMeta方法来完成配置的提供!"));
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