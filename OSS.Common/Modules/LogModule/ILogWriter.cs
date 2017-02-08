#region Copyright (C) 2016 Kevin (OSS开源作坊) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：日志模块接口
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*****************************************************************************/

#endregion
namespace OSS.Common.Modules.LogModule
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

        /// <summary>
        /// 获取全局唯一日志编码
        /// 主要用于后期排查
        /// </summary>
        /// <returns></returns>
        string GetLogCode(LogInfo info);
    }
}