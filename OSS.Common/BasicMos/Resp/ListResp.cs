﻿#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：OSSCore

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