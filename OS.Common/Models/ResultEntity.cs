using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string Message { get; set; }
    }
}
