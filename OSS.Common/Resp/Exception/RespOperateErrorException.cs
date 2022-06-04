namespace OSS.Common.Resp;

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