
namespace OS.Common.Models
{
    public class BaseAutoModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public long create_time { get; set; }

        private int _ret = 200;
        /// <summary>
        /// 结果状态
        /// </summary>
        public int ret {
            get { return _ret; }
            set { _ret = value; }
        }


        /// <summary>
        /// 结果状态描述
        /// </summary>
        public string message { get; set; }
    }
}
