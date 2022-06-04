namespace OSS.Common.Resp;

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