#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：通用返回结果实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*****************************************************************************/

#endregion

using System;
using OSS.Common.ComModels.Enums;

namespace OSS.Common.ComModels
{
    /// <summary>
    /// 结果实体
    /// </summary>
    public class ResultMo
    {
        /// <summary>
        /// 空构造函数
        /// </summary>
        public ResultMo()
        {
            Ret = (int) ResultTypes.Success;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="message"></param>
        public ResultMo(int ret, string message = "")
        {
            Ret = ret;
            Message = message;
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="message"></param>
        public ResultMo(ResultTypes ret, string message = "")
        {
            this.Ret = (int) ret;
            this.Message = message;
        }

        /// <summary>
        /// 返回结果
        /// 一般情况下：
        ///  2xx   成功相关状态（如： 200）
        ///  3xx   参数相关错误 
        ///  4xx   用户授权相关错误
        ///  5xx   服务器内部相关错误信息
        ///  6xx   系统级定制错误信息，如升级维护等
        /// 也可依据第三方自行定义数值
        /// </summary>
        public int Ret { get; set; }

        /// <summary>
        /// 错误或者状态
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///   是否成功
        /// </summary>
        public bool IsSuccess => Ret == (int) ResultTypes.Success;

    }


    /// <summary>
    /// 带Id的结果实体
    /// </summary>
    public class ResultIdMo : ResultMo
    {
        /// <summary>
        /// 
        /// </summary>
        public ResultIdMo():base()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id"></param>
        public ResultIdMo(long id)
            : base(200, null)
        {
            Id = id;
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="message"></param>
        public ResultIdMo(int ret, string message):base(ret,message)
        {
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="message"></param>
        public ResultIdMo(ResultTypes ret, string message)
            : base(ret, message)
        {
        }

        /// <summary>
        /// 返回的关键值，如返回添加是否成功并返回id
        /// </summary>
        public long Id { get; set; }
    }


    /// <summary>
    /// 自定义泛型的结果实体
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    public class ResultMo<TType> : ResultMo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ResultMo()
        {
        
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="data"></param>
        public ResultMo(TType data)
            : base(200, null)
        {
            Data = data;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="message"></param>
        public ResultMo(int ret, string message = "")
            : base(ret, message)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="message"></param>
        public ResultMo(ResultTypes ret, string message = "")
            : base(ret, message)
        {
        }

        /// <summary>
        ///  结果类型数据
        /// </summary>
        public TType Data { get; set; }
    }

    /// <summary>
    ///  
    /// </summary>
    public static class ResultMoMap
    {
        /// <summary>
        ///   将结果实体转换成其他结果实体
        /// </summary>
        /// <typeparam name="TResult">输出对象</typeparam>
        /// <typeparam name="TPara"></typeparam>
        /// <returns>输出对象</returns>
        public static ResultMo<TResult> ConvertToResult<TPara, TResult>(this ResultMo<TPara> source,
            Func<TPara, TResult> func=null)
        {
            ResultMo<TResult> ot = new ResultMo<TResult>();
            ot.Ret = source.Ret;
            ot.Message = source.Message;

            if (func != null && source.Data!=null)
            {
                ot.Data = func(source.Data);
            }
            return ot;
        }

        /// <summary>
        ///   将结果实体转换成其他结果实体   --转化结果是通过 泛型 定义的Result实体
        ///     仅转化 Ret和 Message 的值  
        /// </summary>
        /// <typeparam name="TResult">输出对象</typeparam>
        /// <returns>输出对象</returns>
        public static ResultMo<TResult> ConvertToResultOnly<TResult>(this ResultMo source)
        {
            ResultMo<TResult> ot = new ResultMo<TResult>();
            ot.Ret = source.Ret;
            ot.Message = source.Message;
            return ot;
        }

        /// <summary>
        ///  将结果实体转换成其他结果实体   --转化结果是通过 继承 定义的Result实体  
        ///    仅转化 Ret和 Message 的值
        /// </summary>
        /// <typeparam name="TResult">输出对象</typeparam>
        /// <returns>输出对象</returns>
        public static TResult ConvertToResult<TResult>(this ResultMo source)
            where TResult : ResultMo, new()
        {
            TResult ot = new TResult();
            ot.Ret = source.Ret;
            ot.Message = source.Message;
            return ot;
        }



    }
}
