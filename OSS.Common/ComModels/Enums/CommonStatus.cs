#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：OSSCore

/***************************************************************************
*　　	文件功能描述：排序枚举
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*****************************************************************************/

#endregion


namespace OSS.Common.ComModels.Enums
{
    /// <summary>
    ///     通用状态码 （如果需要更多状态需要自定义枚举，此值不再新增）
    ///     不同的领域对象可能会一到多个
    /// </summary>
    public enum CommonStatus
    {
        /// <summary>
        ///     删除状态
        /// </summary>
        Delete = -1000,

        /// <summary>
        ///  自定义扩展  【取消】
        /// </summary>
        Canceled = -100,

        /// <summary>
        /// 自定义扩展   【审核失败】
        /// </summary>
        Failed = -10,

        /// <summary>
        ///   正常原始状态
        /// </summary>
        Original = 0,

        /// <summary>
        /// 初步提交待审核状态 【提交待确认】【提交待审核】
        /// </summary>
        WaitConfirm = 10,

        /// <summary>
        ///  确认通过激活状态  【确认成功】【审核通过】【已上架/线】
        /// </summary>
        Confirmed = 20,

        /// <summary>
        ///  完成结束状态
        /// </summary>
        Finished = 100
    }
}
