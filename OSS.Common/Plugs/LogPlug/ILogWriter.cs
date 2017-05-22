#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：全局插件 -  日志插件接口
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*       
*****************************************************************************/

#endregion

using OSS.Common.Modules.LogModule;

namespace OSS.Common.Plugs.LogPlug
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