

using OS.Common.ComModels.Enums;

namespace OS.Common.ComModels
{
    public class ResultModel
    {
        /// <summary>
        /// 空构造函数
        /// </summary>
        public ResultModel()
        {
            Ret = 200;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="message"></param>
        public ResultModel(int ret, string message = "")
        {
            Ret = ret;
            Message = message;
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="message"></param>
        public ResultModel(ResultTypes ret, string message = "")
        {
            this.Ret = (int) ret;
            this.Message = message;
        }


        /// <summary>
        /// 返回结果
        ///  2xx   成功相关状态（如： 200）
        ///  3xx   参数相关错误 
        ///  4xx   用户授权相关错误
        ///  5xx   服务器内部相关错误信息
        ///  6xx   系统级定制错误信息，如升级维护等
        /// </summary>
        public int Ret { get; set; }

        /// <summary>
        /// 错误或者状态
        /// </summary>
        public string Message { get; set; }
    }


    public class ResultIdModel : ResultModel
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id"></param>
        public ResultIdModel(long id)
            : base(200, null)
        {
            Id = id;
        }


           /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="message"></param>
        public ResultIdModel(int ret=200, string message = ""):base(ret,message)
        {
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="message"></param>
        public ResultIdModel(ResultTypes ret = ResultTypes.Success, string message = "")
            : base(ret, message)
        {
        }

        /// <summary>
        /// 返回的关键值，如返回添加是否成功并返回id
        /// </summary>
        public long Id { get; set; }
    }
}
