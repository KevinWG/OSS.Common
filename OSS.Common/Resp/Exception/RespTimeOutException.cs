namespace OSS.Common.Resp;

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
    public RespTimeOutException(string msg) : base(SysRespCodes.TimeOut, msg)
    {
    }
}

/// <summary>
///  拒绝请求异常
/// </summary>
public class RespNotAllowedException : RespException
{

    /// <summary>
    ///  拒绝请求异常
    /// </summary>
    public RespNotAllowedException() : this(string.Empty)
    {
    }

    /// <summary>
    ///  超时异常
    /// </summary>
    /// <param name="msg"> 异常消息描述 </param>
    public RespNotAllowedException(string msg) : base(SysRespCodes.NotAllowed, msg)
    {
    }
}