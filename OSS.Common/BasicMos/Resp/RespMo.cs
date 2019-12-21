#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：OSSCore

/***************************************************************************
*　　	文件功能描述：通用返回响应实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*****************************************************************************/

#endregion

using System.Collections.Generic;

namespace OSS.Common.BasicMos.Resp
{
    /// <summary>
    /// 响应实体
    /// </summary>
    public class Resp
    {
        /// <summary>
        /// 构造响应类
        /// </summary>
        public Resp()
        {
            _ret = 0; // 初始化为正常
        }

        /// <summary>
        ///  构造响应类
        /// </summary>
        /// <param name="ret">【业务】响应标识</param>
        /// <param name="message">响应信息描述</param>
        public Resp(int ret, string message)
        {
            this.ret = ret;
            this.msg = message;
        }


        /// <summary>
        ///  构造响应类
        /// </summary>
        /// <param name="ret">【业务】响应标识</param>
        /// <param name="message">响应信息描述</param>
        public Resp(RespTypes ret, string message)
            : this((int) ret, message)
        {
        }

        /// <summary>
        ///  构造响应类
        /// </summary>
        /// <param name="sysRet">【系统/框架】 响应标识</param>
        /// <param name="message">响应信息描述</param>
        public Resp(SysRespTypes sysRet, string message = null)
        {
            this.sys_ret = (int)sysRet;
            this.msg     = message;
        }

        private int _ret;

        /// <summary>
        /// 【业务响应】
        /// 一般情况下：
        ///  0  成功
        ///  13xxx   参数相关错误 
        ///  14xxx   用户授权相关错误
        ///  15xxx   服务器内部相关错误信息
        ///  16xxx   系统级定制错误信息，如升级维护等
        /// 也可依据第三方自行定义数值
        /// </summary>
        public int ret
        {
            get
            {
                if (sys_ret != 0 && _ret == 0)
                    _ret = (int) RespTypes.InnerError;

                return _ret;
            }
            set => _ret = value;
        }
        
        /// <summary>
        ///  系统响应
        /// </summary>
        public int sys_ret { get; set; }

        /// <summary>
        /// 状态信息(错误描述等)
        /// </summary>
        public string msg { get; set; }

    }

    /// <inheritdoc />
    public class LongIdResp : IdResp<long>
    {
        /// <inheritdoc />
        public LongIdResp() 
        {
        }

        /// <inheritdoc />
        public LongIdResp(long id) : base(id)
        {
        }
    }

    /// <inheritdoc />
    public class IdResp: IdResp<string>
    {
        /// <inheritdoc />
        public IdResp()
        {
        }

        /// <inheritdoc />
        public IdResp(string id) : base(id)
        {
        }
    }





    /// <summary>
    /// 带Id的响应实体
    /// </summary>
    public class IdResp<IdType> : Resp
    {
        /// <inheritdoc />
        /// <summary>
        /// 构造响应类
        /// </summary>
        public IdResp()
        {
        }

        /// <inheritdoc />
        public IdResp(IdType id) => this.id = id;

        /// <summary>
        /// Id
        /// </summary>
        public IdType id { get; set; }
    }

    /// <summary>
    ///  响应实体协变接口
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    public interface IResp<out TType>{}

    /// <summary>
    /// 响应实体泛型
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    public class Resp<TType> : Resp, IResp<TType>
    {
        /// <inheritdoc />
        public Resp()
        {
        }

        /// <inheritdoc />
        public Resp(TType data)
        {
            this.data = data;
        }
        
        /// <summary>
        ///  响应类型数据
        /// </summary>
        public TType data { get; set; }
    }


    /// <inheritdoc />
    public class ListResp<TType> : Resp<IList<TType>>
    {
        /// <inheritdoc />
        public ListResp()
        {
        }

        /// <inheritdoc />
        public ListResp(IList<TType> data)
        {
            this.data = data;
        }

    }
}
