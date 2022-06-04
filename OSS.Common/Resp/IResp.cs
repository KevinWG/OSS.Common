
#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：OSSCore

/***************************************************************************
*　　	文件功能描述：通用返回响应实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*****************************************************************************/

#endregion

namespace OSS.Common.Resp
{
    /// <summary>
    ///  只读结果响应实体接口
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    public interface IResp<out TType> : IResp
    {
        /// <summary>
        ///  响应类型数据
        /// </summary>
        public TType data { get; }
    }

    /// <summary>
    ///  只读结果实体接口
    /// </summary>
    public interface IResp
    {
        /// <summary>
        /// 【业务响应编码】
        /// 一般情况下：
        ///  0  成功
        ///  13xxx   参数相关错误 
        ///  14xxx   用户授权相关错误
        ///  15xxx   服务器内部相关错误信息
        ///  16xxx(及其他)   系统级定制错误信息，如升级维护等
        /// 也可依据第三方自行定义数值
        /// </summary>
        public int code
        {
            get;
        }

        /// <summary>
        ///  系统响应编码
        /// </summary>
        public int sys_code { get; }

        /// <summary>
        /// 状态信息(错误描述等)
        /// </summary>
        public string msg { get; }
    }
}
