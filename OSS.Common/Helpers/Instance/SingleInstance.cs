#region Copyright (C) 2019 (OSS开源系列) 公众号：OSSCore

/***************************************************************************
*　　	文件功能描述：全局辅助类 - 单例基础实现
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*       
*****************************************************************************/

#endregion

namespace OSS.Common.Helpers.Instance
{
    /// <summary>
    /// 单例基础实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingleInstance<T>
        where T : new()
    {
        private static T _instance;
        private static readonly object _objLock = new object();

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_objLock)
                    {
                        if (_instance == null)
                        {
                            return _instance = new T();
                        }
                    }
                }
                return _instance;
            }
        }
    }
}
