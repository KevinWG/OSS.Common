#region Copyright (C) 2017 Kevin (OSS开源实验室) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：OSSCore —— 实体基类
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-4-21
*       
*****************************************************************************/

#endregion

using OSS.Common.ComModels.Enums;

namespace OSS.Common.ComModels
{
    #region  和租户相关的基类
    /// <inheritdoc />
    public class BaseUIdAndStateMo : BaseUIdMo
    {
        /// <summary>
        /// 状态信息
        /// </summary>
        public CommonStatus status { get; set; }
    }
    /// <inheritdoc />
    public class BaseUIdMo : BaseMo
    {
        /// <summary>
        ///  用户Id
        /// </summary>
        public string u_id { get; set; }
    }


    /// <inheritdoc />
    public class BaseStateMo: BaseMo
    {
        /// <summary>
        /// 状态信息
        /// </summary>
        public CommonStatus status { get; set; }
    }

    #endregion
    
    public class BaseMo
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public string id { get; set; }

        ///// <summary>
        /////  租户Id
        ///// </summary>
        //public string t_id { get; set; }

        /// <summary>
        ///  创建时间
        /// </summary>
        public long add_time { get; set; }

        /// <summary>
        ///  修改时间
        /// </summary>
        public long m_time { get; set; }

        /// <summary>
        ///  应用来源
        /// </summary>
        public string app_source { get; set; }
    }



}