#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：OSSCore

/***************************************************************************
*　　	文件功能描述：通用返回响应实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*****************************************************************************/

#endregion

using System;

namespace OSS.Common.BasicMos.Resp
{
    /// <inheritdoc />
    [Obsolete("使用 StrResp ")]
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


    /// <inheritdoc />
    [Obsolete("使用 LongResp ")]
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


    /// <summary>
    /// 带Id的响应实体
    /// </summary>
    [Obsolete]
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

}