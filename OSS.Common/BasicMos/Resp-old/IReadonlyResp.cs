using System;
using System.Collections.Generic;
using System.Text;

namespace OSS.Common.BasicMos.Resp
{
    /// <summary>
    ///  只读结果响应实体接口
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    [Obsolete()]
    public interface IReadonlyResp<out TType> : IReadonlyResp
    {
        /// <summary>
        ///  响应类型数据
        /// </summary>
        public TType data { get; }
    }

    /// <summary>
    ///  只读结果实体接口
    /// </summary>
    [Obsolete]
    public interface IReadonlyResp
    {
        /// <summary>
        /// 【业务响应】
        /// 一般情况下：
        ///  0  成功
        ///  13xxx   参数相关错误 
        ///  14xxx   用户授权相关错误
        ///  15xxx   服务器内部相关错误信息
        ///  16xxx(及其他)   系统级定制错误信息，如升级维护等
        /// 也可依据第三方自行定义数值
        /// </summary>
        public int ret
        {
            get;
        }

        /// <summary>
        ///  系统响应
        /// </summary>
        public int sys_ret { get; }

        /// <summary>
        /// 状态信息(错误描述等)
        /// </summary>
        public string msg { get; }
    }
}
