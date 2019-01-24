using System.Collections.Generic;

namespace OSS.Common.Interfaces.Job
{
    /// <summary>
    ///  列表循环处理任务执行
    ///  从 GetExcuteSource() 获取执行数据源，循环并通过 ExcuteItem() 执行个体任务，直到没有数据源返回
    ///       如果执行时间过长，重复触发时 当前任务还在进行中，则不做任何处理
    /// </summary>
    public abstract class BaseListJobExcutor<IType> : IJobExecutor
    {
        /// <summary>
        ///  运行状态
        /// </summary>
        public bool IsRuning { get; protected set; }


        private int triggerTimes { get; set; } = 0;

        /// <summary>
        ///   开始任务
        /// </summary>
        public void StartJob()
        {
            //  任务依然在执行中，不需要再次唤起
            if (IsRuning)
                return;

            IsRuning = true;
            IList<IType> list = null; // 结清实体list

            OnBegin(triggerTimes++);
            do
            {
                for (var i = 0; IsRuning && i < list?.Count; i++)
                {
                    ExcuteItem(list[i], i);
                }
                list = GetExcuteSource();
            } while (IsRuning && list?.Count > 0);
            OnEnd(triggerTimes);
            IsRuning = false;
        }

        /// <summary>
        ///   获取list数据源
        /// </summary>
        /// <returns></returns>
        protected virtual IList<IType> GetExcuteSource()
        {
            return null;
        }

        /// <summary>
        ///  个体任务执行
        /// </summary>
        /// <param name="item">单个实体</param>
        /// <param name="index">在数据源中的索引</param>
        protected virtual void ExcuteItem(IType item, int index)
        {
        }

        /// <summary>
        /// 结束任务
        /// </summary>
        public void StopJob()
        {
            IsRuning = false;
        }



        /// <summary>
        ///  此轮任务开始
        /// </summary>
        protected virtual void OnBegin(int triggerTimes)
        {

        }

        /// <summary>
        ///  此轮任务结束
        /// </summary>
        protected virtual void OnEnd(int triggerTimes)
        {
        }
    }
}
