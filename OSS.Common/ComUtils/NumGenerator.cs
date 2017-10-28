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

using System;

namespace OSS.Common.ComUtils
{
    /// <summary>
    ///  唯一编码生成类
    /// </summary>
    public class NumGenerator
    {
        // 符号位(1位) + Timestamp(41位) + WorkId(10位) + sequence(12位)  = 编号Id (64位)

        //【sequence 部分】  随机序列  12位
        static long sequence = 0L;
        const long maxSequence = -1L ^ (-1L << sequenceBitLength);
        const int sequenceBitLength = 12;

        // 【WorkId部分】 工作Id 10位
        const long maxWorkerId = -1L ^ (-1L << workerIdBitLength);
        const int workerLeftShift = sequenceBitLength;
        const int workerIdBitLength = 10;

        /// <summary>
        ///  当前的工作id 最大值不能超过（2的11次方 - 1）
        /// </summary>
        public long WorkId { get; }

        // 【Timestamp部分】
        const int timestampLeftShift = sequenceBitLength + workerIdBitLength;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="workId">当前的工作id 最大值不能超过（2的11次方 - 1）</param>
        public NumGenerator(long workId)
        {
            if (workId > maxWorkerId || workId < 0)
            {
                throw new ArgumentException("workId", $"worker Id can't be greater than {workId} or less than 0");
            }
            WorkId = workId;
        }

        private long lastTimestamp = 0;
        public long NextNum()
        {
            var timestamp = GetTimestamp();
            if (timestamp < lastTimestamp)
            {
                //如果当前时间小于上一次ID生成的时间戳，说明系统时钟回退过这个时候应当抛出异常
                throw new ArgumentException($"Clock moved backwards.  Refusing to generate id for {lastTimestamp - timestamp} milliseconds");
            }

            // 如果是同一时间生成的，则进行毫秒内序列
            if (lastTimestamp == timestamp)
            {
                sequence = (sequence + 1) & maxSequence;

                //毫秒内序列溢出
                //阻塞到下一个毫秒,获得新的时间戳
                if (sequence == 0)
                    timestamp = WaitNextMillis();
            }
            //时间戳改变，毫秒内序列重置
            else
                sequence = 0L;

            //上次生成ID的时间截
            lastTimestamp = timestamp;
            return (timestamp << timestampLeftShift)
                   | (WorkId << workerLeftShift)
                   | sequence;
        }

        /// <summary>
        ///  当前毫秒内序列使用完，等待下一毫秒
        /// </summary>
        /// <returns></returns>
        protected long WaitNextMillis()
        {
            long timeTicks;
            do
            {
                timeTicks = GetTimestamp();
            }
            while (timeTicks <= lastTimestamp);
            return timeTicks;
        }


        private static readonly long timeStartTicks = new DateTime(2017, 10, 1).Ticks;

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        private static long GetTimestamp()
        {
            return (DateTime.UtcNow.Ticks - timeStartTicks) / 10000;
        }
    }

    /// <summary>
    ///  唯一数字编码生成静态通用类
    /// </summary>
    public class NumUtil
    {
        private static readonly NumGenerator generator = new NumGenerator(0);
        /// <summary>
        /// 单机生成唯一数字编号
        /// </summary>
        /// <returns></returns>
        public static long UniNum()
        {
            return generator.NextNum();
        }


    }
}
