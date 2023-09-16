namespace OSS.Common
{
    /// <summary>
    /// 时间区间
    /// </summary>
    public class DateRange : IDateRange
    {
        /// <summary>
        ///  开始时间
        /// </summary>
        public DateTime start { get; set; }

        /// <summary>
        ///  结束时间
        /// </summary>
        public DateTime end { get; set; }
    }

    /// <summary>
    ///   时间区间
    /// </summary>
    public interface IDateRange
    {
        /// <summary>
        ///  开始时间
        /// </summary>
        public DateTime start { get; set; }

        /// <summary>
        ///  结束时间
        /// </summary>
        public DateTime end { get; set; }
    }



}
