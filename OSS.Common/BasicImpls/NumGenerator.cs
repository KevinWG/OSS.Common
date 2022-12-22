#region Copyright (C) 2019 公众号：OSSCore

/***************************************************************************
*　　	文件功能描述：全局辅助类 - 唯一数字编号生成类
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*       
*****************************************************************************/

#endregion

using System;

namespace OSS.Common
{
    /// <summary>
    /// 生成兼容js的编号（53bit）
    /// </summary>
    public class SmallNumGenerator : BaseNumGenerator
    {
        // 符号位(1位) + Timestamp(41位 最长70年) + WorkId( 3 位) + sequence （9 位）  = 编号Id (53位)

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="workId">当前的工作id 最大值不能超过（7   2^3-1）</param>
        public SmallNumGenerator(int workId) : base(workId, 9, 3)
        {
        }
    }

    /// <summary>
    ///  唯一编码生成类
    /// </summary>
    public class NumGenerator : BaseNumGenerator
    {
        // 符号位(1位) + Timestamp(41位 最长70年) + WorkId(10) + sequence(12) = 编号Id (64位)

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="workId">当前的工作id 最大值不能超过（2^10 - 1）</param>
        public NumGenerator(int workId) : base(workId, 12, 10)
        {
        }
    }

    /// <summary>
    ///  数字编号生成基类
    /// </summary>
    public class BaseNumGenerator
    {
        //【sequence 部分】  随机序列 
        private long _maxSequence; // 最大值
        private int _sequenceBitLength; //长度

        // 【WorkId部分】 工作Id 
        private long _maxWorkerId;    // 最大值
        private int _workerIdBitLength; // 长度

        /// <summary>
        ///  workerId需要偏移的位置
        /// </summary>
        protected int WorkerLeftShift { get; private set; }

        /// <summary>
        /// Timestamp 需要偏移的位置
        /// </summary>
        protected int TimestampLeftShift { get; private set; }

        private void InitailConfig(int seqBitLength, int worIdBitLength)
        {
            _sequenceBitLength = seqBitLength;
            _maxSequence       = -1L ^ (-1L << _sequenceBitLength);

            _workerIdBitLength = worIdBitLength;
            _maxWorkerId       = -1L ^ (-1L << _workerIdBitLength);

            WorkerLeftShift    = _sequenceBitLength;
            TimestampLeftShift = WorkerLeftShift + _workerIdBitLength;
        }

        /// <summary>
        ///  获取当前的工作ID
        /// </summary>
        public long WorkId { get; internal set; }


        private long _sequence;   //  时间戳下 序号值
        private long _timestamp;  // 最后一次的时间戳值

        private readonly object obj = new object();// 
        /// <summary>
        /// 构造函数
        /// </summary>
        protected BaseNumGenerator(int workId, int seqBitLength, int worIdBitLength)
        {
            InitailConfig(seqBitLength, worIdBitLength);

            if (workId > _maxWorkerId || workId < 0)
            {
                throw new ArgumentException("workId", $"工作Id不能大于 {_maxWorkerId} 或 小于 0");
            }
            WorkId = workId;
        }


        /// <summary>
        ///  生成编号
        /// </summary>
        /// <returns></returns>
        public long NewNum()
        {
            long ts, seq;
            lock (obj)
            {
                SetTimestampAndSeq();
                ts = _timestamp;
                seq = _sequence;
            }
            return CombineNum(ts, seq);
        }

        /// <summary>
        ///  根据指定时间戳，获取能够成的id范围
        /// </summary>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public (long min, long max) GetNumRange(long milliseconds)
        {
            var min = CombineNum(milliseconds, 0);
            var max = CombineNum(milliseconds, _maxSequence);
            return (min, max);
        }

        /// <summary>
        ///   组合数字ID
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        private long CombineNum(long timestamp, long sequence)
        {
            return (timestamp << TimestampLeftShift)
                         | (WorkId << WorkerLeftShift)
                         | sequence;
        }

        private void SetTimestampAndSeq()
        {
            var newTimestamp = TimeMilliNum();
            if (newTimestamp < _timestamp)
            {
                //如果当前时间小于上一次ID生成的时间戳，说明系统时钟回退过这个时候应当抛出异常
                throw new ArgumentException(
                    $"当前时间小于上次生成时间 {_timestamp - newTimestamp} 毫秒，注意系统时间是否发生变化！");
            }

            // 如果是同一时间生成的，则进行毫秒内序列
            if (_timestamp == newTimestamp)
            {
                _sequence = (_sequence + 1) & _maxSequence;

                //毫秒内序列溢出
                //阻塞到下一个毫秒,获得新的时间戳
                if (_sequence == 0)
                    newTimestamp = WaitNextMillis(_timestamp);
            }
            //时间戳改变，毫秒内序列重置
            else
                _sequence = 0L;

            _timestamp = newTimestamp;
        }

        /// <summary>
        ///  当前毫秒内序列使用完，等待下一毫秒
        /// </summary>
        /// <returns></returns>
        protected long WaitNextMillis(long curTimeSpan)
        {
            long timeTicks;
            do
            {
                timeTicks = TimeMilliNum();
            } while (timeTicks <= curTimeSpan);
            return timeTicks;
        }
        
        private static readonly long _timeStartTicks = new DateTime(2020, 1, 1).ToUniversalTime().Ticks;

        /// <summary>
        ///  时间戳数字编号（精度 毫秒
        /// </summary>
        /// <returns></returns>
        private static long TimeMilliNum()
        {
            return (DateTime.UtcNow.Ticks - _timeStartTicks) / 10000;
        }
    }
}
