using System;
using System.Text;
using OSS.Common.BasicImpls;
using OSS.Common.Extention;

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



        private static readonly NumGenerator _generator = new NumGenerator(0);
        private static readonly SmallNumGenerator _smallGenerator = new SmallNumGenerator(0);

        /// <summary>
        ///  设置当前snowflow算法默认workid
        /// </summary>
        /// <param name="workId"></param>
        public static void SetDefaultWorkId(int workId)
        {
            _generator.WorkId = workId;
            _smallGenerator.WorkId = workId;
        }


        /// <summary>
        /// twitter 的snowflake唯一Id算法(排除机器位)
        /// </summary>
        /// <returns></returns>
        public static long SnowNum()
        {
            return _generator.NextNum();
        }

        /// <summary>
        /// twitter 的snowflake唯一Id算法(排除机器位)
        /// </summary>
        /// <returns></returns>
        public static long SmallSnowNum()
        {
            return _smallGenerator.NextNum();
        }


        /// <summary>
        ///  时间戳数字编号（精度 毫秒
        /// </summary>
        /// <returns></returns>
        public static long TimeMilSecsNum()
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
            return string.Concat(TimeMilSecsNum(), suffixNum).ToInt64();
        }

    }
}
