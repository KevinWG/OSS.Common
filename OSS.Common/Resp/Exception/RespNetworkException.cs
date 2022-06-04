namespace OSS.Common.Resp;

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