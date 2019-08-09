using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OSS.Common.Interfaces.Job
{
    /// <summary>
    ///  列表循环处理任务执行
    ///  从 GetExcuteSource() 获取执行数据源，循环并通过 ExcuteItem() 执行个体任务，直到没有数据源返回
    ///       如果执行时间过长，重复触发时 当前任务还在进行中，则不做任何处理
    /// </summary>
    public abstract class BaseListJobExcutor<IType> : IJobExecutor
    {
        private bool _isExcuteOnce;

        /// <summary>
        ///  列表任务执行者
        /// </summary>
        public BaseListJobExcutor():this(false)
        {
        }
        
        /// <summary>
        ///  列表任务执行者
        /// </summary>
        /// <param name="excuteOnce">是否只获取一次数据源</param>
        public BaseListJobExcutor(bool excuteOnce)
        {
            _isExcuteOnce = excuteOnce;
        }
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
            int page=0;
            IList<IType> list; // 结清实体list

            await OnBegin();
            while (IsRuning && (list =await GetExcuteSource(page++))?.Count > 0)
            {
                for (var i = 0; IsRuning && i < list?.Count; i++)
                {
                    await ExcuteItem(list[i], i);
                }

                if (_isExcuteOnce)
                {
                    break;
                }
            }

            await OnEnd();
            IsRuning = false;
        }

        /// <summary>
        ///   获取list数据源, 此方法会被循环调用
        /// </summary>
        /// <returns></returns>
        protected abstract Task<IList<IType>> GetExcuteSource(int page);

        /// <summary>
        ///  个体任务执行
        /// </summary>
        /// <param name="item">单个实体</param>
        /// <param name="index">在数据源中的索引</param>
        protected abstract Task ExcuteItem(IType item, int index);
   

        /// <summary>
        /// 结束任务
        /// </summary>
        public Task StopJob(CancellationToken cancellationToken)
        {
            IsRuning = false;
            return Task.CompletedTask;
        }


        /// <summary>
        ///  此轮任务开始
        /// </summary>
        protected virtual Task OnBegin()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        ///  此轮任务结束
        /// </summary>
        protected virtual Task OnEnd()
        {
            return Task.CompletedTask;
        }
        
    }
}
