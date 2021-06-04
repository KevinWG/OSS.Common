#region Copyright (C) 2019 (OSS开源系列) 公众号：OSSCore

/***************************************************************************
*　　	文件功能描述：全局辅助类 - 编码生成辅助
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*       
*****************************************************************************/

#endregion

using System;
using System.Text;
using OSS.Common.BasicImpls;
using OSS.Common.Extension;

namespace OSS.Common.Helpers
{
    /// <summary>
    ///  唯一数字编码生成静态通用类
    /// </summary>
    public static class NumHelper
    {
        private static readonly long _timeStartTicks = new DateTime(2019, 1, 1).ToUniversalTime().Ticks;
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



        private static readonly NumGenerator      _generator      = new NumGenerator(0);
        private static readonly SmallNumGenerator _smallGenerator = new SmallNumGenerator(0);

        /// <summary>
        ///  获取 twitter 的snowflake唯一Id算法实例(排除机器位)
        /// </summary>
        /// <returns></returns>
        public static NumGenerator GetSnowNumGenerator(int workId)
        {
            return new NumGenerator(workId);
        }
        
        /// <summary>
        ///  获取 twitter 的snowflake唯一Id算法实例(排除机器位)
        ///   id大小不超过 2^52次方-1
        /// </summary>
        /// <returns></returns>
        public static SmallNumGenerator GetSmallSnowNumGenerator(int workId)
        {
            return new SmallNumGenerator(workId);
        }






        /// <summary>
        /// twitter 的snowflake算法 workid=0 的算法实例：
        /// 生成的Id(排除机器位)
        /// </summary>
        /// <returns></returns>
        public static long SnowNum()
        {
            return _generator.NewNum();
        }

        /// <summary>
        /// twitter 的snowflake算法 workid=0 的算法实例：
        /// 根据指定时间戳，获取能够成的id范围
        /// </summary>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public static (long min, long max) GetSnowNumRange(long milliseconds)
        {
            return _generator.GetNumRange(milliseconds);
        }

        /// <summary>
        /// twitter 的snowflake算法 workid=0 的算法实例：
        /// 生成的大小不超过 2^52次方-1 的Id(排除机器位)
        /// </summary>
        /// <returns></returns>
        public static long SmallSnowNum()
        {
            return _smallGenerator.NewNum();
        }


        /// <summary>
        /// twitter 的snowflake算法 workid=0 的算法实例：
        /// 根据指定时间戳，获取能够成的id范围
        /// </summary>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public static (long min, long max) GetSmallSnowNumRange(long milliseconds)
        {
            return _smallGenerator.GetNumRange(milliseconds);
        }







        /// <summary>
        ///  时间戳（秒）+ 主编号的后四位 生成的数字编号
        /// </summary>
        /// <param name="mainNum"></param>
        /// <returns></returns>
        [Obsolete]
        public static long SubTimeNum(long mainNum)
        {
            var suffixNum = mainNum % 10000;
            return string.Concat(DateTime.Now.ToUtcSeconds(), suffixNum).ToInt64();
        }
        /// <summary>
        ///  时间戳数字编号（精度 毫秒
        /// </summary>
        /// <returns></returns>

        [Obsolete("请使用 DateTime.ToUtcMilliSeconds（OSS.Common.Extension）扩展方法")]
        public static long TimeMilSecsNum()
        {
            return (DateTime.UtcNow.Ticks - _timeStartTicks) / 10000;
        }

    }
}
