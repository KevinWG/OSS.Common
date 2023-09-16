
namespace OSS.Common.Resp
{
    /// <summary>
    ///  参数异常
    /// </summary>
    public class RespArgumentException : RespException
    {
        /// <summary>
        ///  参数异常
        /// </summary>
        public RespArgumentException() : this(string.Empty,"参数异常!")
        {
        }

        /// <summary>
        ///  参数异常
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="msg"> 异常消息描述 </param>
        public RespArgumentException(string name,string msg) : base((int)SysRespCodes.AppError, (int)RespCodes.ParaError, string.IsNullOrEmpty(name)?msg:$"[{name}]:{msg}")
        {
        }
    }
}
