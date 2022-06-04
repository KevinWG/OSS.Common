
namespace OSS.Common.Resp
{
    /// <summary>
    ///  未实现功能异常
    /// </summary>
    public class RespNotImplementException:RespException
    {
        /// <summary>
        ///  未实现功能异常
        /// </summary>
        public RespNotImplementException() : this(string.Empty)
        {
        }

        /// <summary>
        ///  未实现功能异常
        /// </summary>
        /// <param name="msg"> 异常消息描述 </param>
        public RespNotImplementException(string msg) : base(SysRespTypes.NotImplement, msg)
        {
        }
    }


    /// <summary>
    ///  超时异常
    /// </summary>
    public class RespTimeOutException : RespException
    {

        /// <summary>
        ///  超时异常
        /// </summary>
        public RespTimeOutException() : this(string.Empty)
        {
        }

        /// <summary>
        ///  超时异常
        /// </summary>
        /// <param name="msg"> 异常消息描述 </param>
        public RespTimeOutException(string msg) : base(SysRespTypes.TimeOut, msg)
        {
        }
    }




    /// <summary>
    ///  网络异常
    /// </summary>
    public class RespNetworkException : RespException
    {
        /// <summary>
        ///  网络异常
        /// </summary>
        public RespNetworkException() : this(string.Empty)
        {
        }

        /// <summary>
        ///  网络异常
        /// </summary>
        /// <param name="msg"> 异常消息描述 </param>
        public RespNetworkException(string msg) : base(SysRespTypes.NetError, msg)
        {
        }
    }



    /// <summary>
    ///  应用处理异常
    /// </summary>
    public class RespOperateErrorException : RespException
    {
        /// <summary>
        ///  应用处理异常
        /// </summary>
        public RespOperateErrorException() : this("应用处理异常!")
        {
        }

        /// <summary>
        ///  应用处理异常
        /// </summary>
        /// <param name="msg"> 异常消息描述 </param>
        public RespOperateErrorException(string msg) : base((int)SysRespTypes.AppError,(int)RespTypes.OperateFailed, msg)
        {
        }
    }


}
