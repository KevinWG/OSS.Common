using System;
using System.Threading;
using System.Threading.Tasks;
using OSS.Common.Plugs.LogPlug;

namespace OSS.Common.Interfaces.Job
{
    /// <summary>
    ///   定时器基础类
    /// </summary>
    public class BaseTimerTrigger:IDisposable
    {
        private Timer _timer;

    private readonly TimeSpan _dueTime;
    private readonly TimeSpan _periodTime;

    private readonly IJobExecutor _jobExcutor;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dueTime">到期开始执行时间</param>
    /// <param name="periodTime">间隔时间</param>
    /// <param name="jobExcutor">任务执行者</param>
    protected BaseTimerTrigger(TimeSpan dueTime, TimeSpan periodTime, IJobExecutor jobExcutor)
    {
        _dueTime = dueTime;
        _periodTime = periodTime;
        _jobExcutor = jobExcutor;
    }


    #region  基础方法

    /// <summary>
    ///   配置并触发定时器    
    /// </summary>
    /// <returns></returns>
    private void StartTimerTrigger()
    {
        if (_timer == null)
            _timer = new Timer(ExcuteJob, _jobExcutor, _dueTime, _periodTime);
        else
            _timer.Change(_dueTime, _periodTime);
    }

    /// <summary>
    ///  停止定时器
    /// </summary>
    private void StopTimerTrigger()
    {
        _timer?.Change(Timeout.Infinite, Timeout.Infinite);
    }

  

    /// <inheritdoc />
    public void Dispose()
    {
        _timer?.Dispose();
    }

    #endregion

    /// <summary>
    ///  系统级任务执行启动
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            StartTimerTrigger();
        }
        catch (Exception e)
        {
            LogUtil.Error($"启动定时任务({nameof(GetType)})时出错，信息：{e}", string.Empty, "System_TimerJob");
        }

        return Task.CompletedTask;
    }

    /// <summary>
    ///  系统级任务执行关闭
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task StopAsync(CancellationToken cancellationToken)
    {
        try
        {
            _jobExcutor.StopJob();
            StopTimerTrigger();
        }
        catch (Exception e)
        {
            LogUtil.Error($"停止定时任务({nameof(GetType)})时出错，信息：{e}", String.Empty, "System_TimerJob");
        }
        return Task.CompletedTask;
    }

    private void ExcuteJob(object obj)
    {
        try
        {
            var excutor = obj as IJobExecutor;
            excutor?.StartJob();
        }
        catch (Exception e)
        {
            LogUtil.Error($"执行任务({nameof(GetType)})时出错，信息：{e}", String.Empty, "System_TimerJob");
        }
    }
}
}
