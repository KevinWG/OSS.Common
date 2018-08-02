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
using System.Collections.Generic;
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
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="message"></param>
        public ResultMo(int ret, string message = "")
        {
            this.ret = ret;
            this.msg = message;
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="message"></param>
        public ResultMo(ResultTypes ret, string message = "")
        {
            this.ret = (int) ret;
            this.msg = message;
        }

        /// <summary>
        /// 返回结果
        /// 一般情况下：
        ///  0  成功
        ///  13xx   参数相关错误 
        ///  14xx   用户授权相关错误
        ///  15xx   服务器内部相关错误信息
        ///  16xx   系统级定制错误信息，如升级维护等
        /// 也可依据第三方自行定义数值
        /// </summary>
        public int ret { get; set; }

        /// <summary>
        /// 状态信息(错误描述等)
        /// </summary>
        public string msg { get; set; }
    }

    /// <summary>
    ///   ResultMo 扩展
    /// </summary>
    public static class ResultExtention
    {
        /// <summary>
        ///  是否是Success
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public static bool IsSuccess(this ResultMo res)=>
            res.ret == 0;

        /// <summary>
        /// 是否是对应的结果类型
        /// </summary>
        /// <param name="res"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsResultType(this ResultMo res, ResultTypes type) =>
            res.ret == (int) type;
    }


    /// <summary>
    /// 带Id的结果实体
    /// </summary>
    public class ResultIdMo : ResultMo
    {
        /// <summary>
        /// 
        /// </summary>
        public ResultIdMo()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id"></param>
        public ResultIdMo(long id)
        {
            this.id = id;
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
        public long id { get; set; }
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
        {
            this.data = data;
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
        public TType data { get; set; }
    }


    /// <summary>
    /// 自定义泛型的结果实体
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    public class ResultListMo<TType> : ResultMo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ResultListMo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="data"></param>
        public ResultListMo(IList<TType> data)
        {
            this.data = data;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="message"></param>
        public ResultListMo(int ret, string message = "")
            : base(ret, message)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="message"></param>
        public ResultListMo(ResultTypes ret, string message = "")
            : base(ret, message)
        {
        }

        /// <summary>
        ///  结果类型数据
        /// </summary>
        public IList<TType> data { get; set; }
    }


    /// <summary>
    ///  结果实体映射类
    /// </summary>
    public static class ResultMoMap
    {
        /// <summary>
        ///   将结果实体转换成其他结果实体
        /// </summary>
        /// <typeparam name="TResult">输出对象</typeparam>
        /// <typeparam name="TPara"></typeparam>
        /// <returns>输出对象</returns>
        public static ResultMo<TResult> ConvertToResult<TPara, TResult>(this TPara res,
            Func<TPara, TResult> func)
            where TPara : ResultMo
        {
            var ot = new ResultMo<TResult>
            {
                ret = res.ret,
                msg = res.msg
            };

            if (func != null && res.IsSuccess())
                ot.data = func(res);
            
            return ot;
        }



        /// <summary>
        /// 转化到结果实体
        /// </summary>
        /// <typeparam name="TPara"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="res"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static ResultMo<TResult> ConvertToResult<TPara, TResult>(this ResultMo<TPara> res,
            Func<TPara, TResult> func)
        {
            var ot = new ResultMo<TResult>
            {
                ret = res.ret,
                msg = res.msg
            };

            if (func != null && res.IsSuccess())
                ot.data = func(res.data);

            return ot;
        }
        /// <summary>
        /// 转化到结果实体
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="res"></param>
        /// <returns></returns>
        public static ResultMo<TResult> ConvertToResult<TResult>(this ResultMo res)
        {
            return ConvertToResult<ResultMo, TResult>(res,null);
        }


        /// <summary>
        ///  转化到结果实体
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="res"></param>
        /// <returns></returns>
        [Obsolete]
        public static ResultMo<TResult> ConvertToResultOnly<TResult>(this ResultMo res)
        {
            return ConvertToResult<ResultMo, TResult>(res,null);
        }

        /// <summary>
        /// 转化到结果实体
        /// </summary>
        /// <typeparam name="TPara"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="res"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static TResult ConvertToResultInherit<TPara, TResult>(this TPara res,
            Func<TPara, TResult> func)
            where TPara : ResultMo
            where TResult : ResultMo,new()
        {
            if (func != null && res.IsSuccess())
                return func(res);

            return new TResult(){
                ret = res.ret,
                msg = res.msg
            };
        }

        /// <summary>
        /// 转化到结果实体
        /// </summary>
        /// <typeparam name="TPara"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="res"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static TResult ConvertToResultInherit<TPara, TResult>(this ResultMo<TPara> res,
            Func<TPara, TResult> func)
            where TResult : ResultMo,new()
        {
            if (func != null && res.IsSuccess())
                return func(res.data);

            return new TResult()
            {
                ret = res.ret,
                msg = res.msg
            };
        }

        /// <summary>
        /// 转化到结果实体
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="res"></param>
        /// <returns></returns>
        public static TResult ConvertToResultInherit<TResult>(this ResultMo res)
            where TResult : ResultMo, new()
        {
            return ConvertToResultInherit<ResultMo, TResult>(res);
        }
    }
}
