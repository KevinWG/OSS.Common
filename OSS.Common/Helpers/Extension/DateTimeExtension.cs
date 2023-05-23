#region Copyright (C) 2014 OS系列开源项目

/*       
　　	文件功能描述：时间扩展

　　	创建人：王超
        创建人Email：1985088337@qq.com
    	创建日期：2014.08.25

　　	修改描述：
*/

#endregion

namespace OSS.Common.Extension;

/// <summary>
/// 时间秒数转化
/// </summary>
public static class DateTimeExtension
{
    private static readonly long startTicks = new DateTime(1970, 1, 1).Ticks;


    /// <summary>
    /// 获取距离 1970-01-01（格林威治时间）的秒数
    /// </summary>
    /// <param name="localTime"></param>
    /// <returns></returns>
    public static long ToUtcSeconds(this DateTime localTime)
    {
        return (localTime.ToUniversalTime().Ticks - startTicks) / 10000000;
    }

    /// <summary>
    /// 距离 1970-01-01（格林威治时间）的秒数转换为当前时间
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    public static DateTime FromUtcSeconds(this long seconds)
    {
        return DateTimeOffset.FromUnixTimeSeconds(seconds)
            .LocalDateTime; // new DateTime(1970, 1, 1).AddSeconds(seconds).ToLocalTime();
    }


    /// <summary>
    /// 获取距离 1970-01-01（格林威治时间）的毫秒数
    /// </summary>
    /// <param name="localTime"></param>
    /// <returns></returns>
    public static long ToUtcMilliseconds(this DateTime localTime)
    {
        return (localTime.ToUniversalTime().Ticks - startTicks) / 10000;
    }

    /// <summary>
    /// 距离 1970-01-01（格林威治时间）的秒数转换为当前时间
    /// </summary>
    /// <param name="milliseconds"></param>
    /// <returns></returns>
    public static DateTime FromUtcMilliseconds(this long milliseconds)
    {
        return DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).LocalDateTime;
    }


    /// <summary>
    /// 获取距离 1970-01-01（格林威治时间）Ticks  精确到0.1微秒（千万分之一秒）
    /// </summary>
    /// <param name="localTime"></param>
    /// <returns></returns>
    public static long ToUtcTicks(this DateTime localTime)
    {
        return localTime.ToUniversalTime().Ticks - startTicks;
    }



    /// <summary>
    /// 获取距离 1970-01-01（本地/北京时间）的秒数
    /// </summary>
    /// <param name="localTime"></param>
    /// <returns></returns>
    public static long ToLocalSeconds(this DateTime localTime)
    {
        return (localTime.Ticks - startTicks) / 10000000;
    }

    /// <summary>
    /// 距离 1970-01-01（本地/北京时间）的秒数转换为当前时间
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    public static DateTime FromLocalSeconds(this long seconds)
    {
        return new DateTime(1970, 1, 1).AddSeconds(seconds);
    }
}