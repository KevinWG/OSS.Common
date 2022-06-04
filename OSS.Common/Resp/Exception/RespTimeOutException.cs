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
    public RespTimeOutException(string msg) : base(SysRespTypes.TimeOut, msg)
    {
    }
}