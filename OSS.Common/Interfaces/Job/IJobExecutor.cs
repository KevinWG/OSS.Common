namespace OSS.Common.Interfaces.Job
{
    /// <summary>
    ///  任务 提供者 接口
    /// </summary>
    public interface IJobExecutor
    {
        /// <summary>
        /// 开始任务
        /// </summary>
        void StartJob();

        /// <summary>
        ///  结束任务
        /// </summary>
        void StopJob();
    }
}
