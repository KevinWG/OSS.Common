using System.Collections.Generic;

namespace OSS.Common.Resp
{
    /// <summary>
    ///  列表通行token接口
    /// </summary>
    public interface ITokenList<TType>
    {
        /// <summary>
        ///  列表
        /// </summary>
        IList<TType> data { get; }

        /// <summary>
        ///  列表关联外部字段token字典
        /// </summary>
        public Dictionary<string, Dictionary<string,string>> pass_tokens { get; set; }
    }
}