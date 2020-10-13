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


        public async Task<Resp<TMetaType>> GetMeta()
        {
            var metaRes = await GetCustomMeta();
            if (metaRes != null)
            {
                return metaRes;
            }

            if (DefaultMeta != null)
                return new Resp<TMetaType>(DefaultMeta);

            return new Resp<TMetaType>().WithResp(RespTypes.ObjectNull, "未发现任何配置信息，请重写GetCustomMeta方法，或配置DefaultMeta！");
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