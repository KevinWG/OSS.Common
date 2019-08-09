using System.Threading;
using System.Threading.Tasks;

namespace OSS.Common.Interfaces.Job
{
 
    /// <summary>
    /// 任务基类
    ///       如果执行时间过长，重复触发时 当前任务还在进行中，则不做任何处理
    /// </summary>
    public abstract class BaseJobExcutor : IJobExecutor
    {
        /// <summary>
        ///  运行状态
        /// </summary>
        public bool IsRuning { get; private set; }
        
        /// <summary>
        ///   开始任务
        /// </summary>
        public async Task StartJob(CancellationToken cancellationToken)
        {
            //  任务依然在执行中，不需要再次唤起
            if (IsRuning)
                return;

            IsRuning = true;
            
            await Excute();

            IsRuning = false;

            return;
        }



        /// <summary>
        ///  任务执行
        /// </summary>
        protected abstract Task Excute();



        /// <summary>
        /// 结束任务
        /// </summary>
        public Task StopJob(CancellationToken cancellationToken)
        {
            IsRuning = false;
            return Task.CompletedTask;
        }
    }
}
