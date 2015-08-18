namespace OS.Common.Modules.LogModule
{
    /// <summary>
    /// 日志写实现接口
    /// </summary>
    public interface ILogWriter
    {
        /// <summary>
        ///   日志写功能
        /// </summary>
        ///<param name="info">日志实体</param>
        void WriteLog(LogInfo info);
    }
}