namespace OSS.Common.Resp;

public class DefaultSuccessResp : IResp
{
    /// <inheritdoc />
    public int code
    {
        get => (int) RespCodes.Success;
        set => throw new RespException("只读实体属性，不可赋值!");
    }

    /// <inheritdoc />
    public int sys_code
    {
        get => (int)SysRespCodes.Ok;
        set => throw new RespException("只读实体属性，不可赋值!");
    }

    /// <inheritdoc />
    public string msg
    {
        get => string.Empty;
        set => throw new RespException("只读实体属性，不可赋值!");
    }
}