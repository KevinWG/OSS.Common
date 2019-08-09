using System;
using System.Threading;
using System.Threading.Tasks;

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
        Task StartJob(CancellationToken cancellationToken);

        /// <summary>
        ///  结束任务
        /// </summary>
        Task StopJob(CancellationToken cancellationToken);
    }

    internal class InternalExecutor : IJobExecutor
    {
        private readonly Func<CancellationToken, Task> _startAction;

        private readonly Func<CancellationToken, Task> _stopAction;


        /// <inheritdoc />
        public InternalExecutor(Func<CancellationToken, Task> startAction, Func<CancellationToken, Task> stopAction)
        {
            _startAction = startAction;
            _stopAction = stopAction;
        }

        public Task StartJob(CancellationToken cancellationToken)
        {
            return _startAction?.Invoke(cancellationToken) ?? Task.CompletedTask;
        }

        public Task StopJob(CancellationToken cancellationToken)
        {
            return _stopAction?.Invoke(cancellationToken) ?? Task.CompletedTask;
        }
    }
}
