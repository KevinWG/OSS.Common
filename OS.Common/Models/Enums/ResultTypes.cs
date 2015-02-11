using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS.Common.Models.Enums
{
    public enum ResultTypes
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 200,

        /// <summary>
        /// 参数错误
        /// </summary>
        ParaError = 301,

        /// <summary>
        /// 条件不满足
        /// </summary>              
        ParaNotMeet = 310,

        /// <summary>
        /// 添加失败
        /// </summary>
        AddFail = 320,

        /// <summary>
        /// 更新失败
        /// </summary>
        UpdateFail = 330,

        /// <summary>
        /// 对象不存在
        /// </summary>
        ObjectNull = 400,

        /// <summary>
        /// 当前用户未授权（特殊）
        /// </summary>
        UserNull = 404,

        /// <summary>
        /// 对象已存在
        /// </summary>
        ObjectExsit = 410,

        /// <summary>
        /// 对象状态不正常
        /// </summary>
        ObjectStateError = 420,

        /// <summary>
        /// 没有权限
        /// </summary>
        NoRight = 430,

        /// <summary>
        /// 内部错误（服务器错误）
        /// </summary>
        InnerError = 500

    }
}
