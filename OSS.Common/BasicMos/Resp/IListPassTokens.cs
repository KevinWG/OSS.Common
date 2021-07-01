using System.Collections.Generic;

namespace OSS.Common.BasicMos.Resp
{
    /// <summary>
    ///  列表通行token接口
    /// </summary>
    public interface IListPassTokens
    {
        /// <summary>
        ///  和结果列表对应的token字典
        /// </summary>
        public Dictionary<string, string> tokens { get; }

        /// <summary>
        ///  列表关联外部字段token字典
        /// </summary>
        public Dictionary<string, Dictionary<string,string>> relate_tokens { get;  }
    }
}