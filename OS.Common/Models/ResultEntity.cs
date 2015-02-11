namespace OS.Common.Models
{
    public class ResultEntity
    {
        /// <summary>
        ///  200成功
        /// </summary>
        public int ret { get; set; }
        /// <summary>
        /// 错误或者状态
        /// </summary>
        public string message { get; set; }
    }
}
