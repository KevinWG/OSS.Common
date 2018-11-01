#region Copyright (C) 2017 Kevin (OSS开源系列) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：全局辅助类 - 唯一数字编号生成类
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*       
*****************************************************************************/

#endregion

using OSS.Common.Extention;
using System;
using System.Text;

namespace OSS.Common.ComUtils
{
    /// <summary>
    ///  唯一编码生成类
    /// </summary>
    public class NumGenerator
    {
        // 符号位(1位) + Timestamp(41位) + WorkId(10位) + sequence(12位)  = 编号Id (64位)

        //【sequence 部分】  随机序列  12位
        private const long _maxSequence = -1L ^ (-1L << _sequenceBitLength);
        private const int _sequenceBitLength = 12;

        // 【WorkId部分】 工作Id 10位
        private const long _maxWorkerId = -1L ^ (-1L << _workerIdBitLength);
        private const int _workerLeftShift = _sequenceBitLength;
        private const int _workerIdBitLength = 10;

        // 【Timestamp部分】
        private const int _timestampLeftShift = _workerLeftShift + _workerIdBitLength;


        /// <summary>
        ///  当前的工作id 最大值不能超过（2的11次方 - 1）
        /// </summary>
        public long WorkId { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="workId">当前的工作id 最大值不能超过（2的11次方 - 1）</param>
        public NumGenerator(long workId)
        {
            if (workId > _maxWorkerId || workId < 0)
            {
                throw new ArgumentException("workId", $"worker Id can't be greater than {workId} or less than 0");
            }

            WorkId = workId;
        }

        private long _sequence;
        private long _timestamp;
        private readonly object _lockObj=new object();

        /// <summary>
        ///  生成下一个编号
        /// </summary>
        /// <returns></returns>
        public long NextNum()
        {
            long ts, seq;
            lock (_lockObj)
            {
                SetTimestampAndSeq();
                ts = _timestamp;
                seq = _sequence;
            }
            return CombineNum(ts, seq);
        }

 
        private long CombineNum(long timestamp, long sequence)
        {
            return (timestamp << _timestampLeftShift)
                   | (WorkId << _workerLeftShift)
                   | sequence;
        }


        private void SetTimestampAndSeq()
        {
            var newTimestamp = NumUtil.TimeMilliNum();
            if (newTimestamp < _timestamp)
            {
                //如果当前时间小于上一次ID生成的时间戳，说明系统时钟回退过这个时候应当抛出异常
                throw new ArgumentException(
                    $"生成数字编号时，时间出错，和上次相差 {_timestamp - newTimestamp} 毫秒");
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

            //上次生成ID的时间截
            _timestamp = newTimestamp;

        }

        /// <summary>
        ///  当前毫秒内序列使用完，等待下一毫秒
        /// </summary>
        /// <returns></returns>
        protected long WaitNextMillis(long timestmap)
        {
            long timeTicks;
            do
            {
                timeTicks = NumUtil.TimeMilliNum();
            } while (timeTicks <= timestmap);

            return timeTicks;
        }

    }

    /// <summary>
    ///  唯一数字编码生成静态通用类
    /// </summary>
    public static class NumUtil
    {

        private static readonly long _timeStartTicks = new DateTime(2017, 12, 1).ToUniversalTime().Ticks;
        private static readonly Random _rnd = new Random(DateTime.Now.Millisecond);


        /// <summary>		
        /// 随机数字		
        /// </summary>		
        /// <returns></returns>		
        public static string RandomNum(int length = 4)
        {
            var num = new StringBuilder(length);
            for (var i = 0; i < length; i++)
            {
                num.Append(_rnd.Next(0, 9));
            }

            return num.ToString();
        }

        private static readonly NumGenerator generator = new NumGenerator(0);

        /// <summary>
        /// twitter 的snowflake唯一Id算法(排除机器位)
        /// </summary>
        /// <returns></returns>
        public static long SnowNum()
        {
            return generator.NextNum();
        }

        /// <summary>
        ///  时间戳数字编号（精度 毫秒
        /// </summary>
        /// <returns></returns>
        public static long TimeMilliNum()
        {
            return (DateTime.UtcNow.Ticks - _timeStartTicks) / 10000;
        }

        /// <summary>
        ///  时间戳（秒）+ 主编号的后四位 生成的数字编号
        /// </summary>
        /// <param name="mainNum"></param>
        /// <returns></returns>
        public static long SubTimeNum(long mainNum)
        {
            var suffixNum = mainNum % 10000;
            return string.Concat(TimeMilliNum(), suffixNum).ToInt64();
        }

    }
}
