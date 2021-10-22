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

using System;

namespace OSS.Common
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

        /// <summary>
        ///  获取单例实例
        /// </summary>
        /// <param name="initialCreator"></param>
        /// <returns></returns>
        public static T GetInstance(Func<T> initialCreator = null)
        {
            if (_instance == null)
            {
                lock (_objLock)
                {
                    if (_instance != null) 
                        return _instance;

                    if (initialCreator == null)
                        return _instance = new T();

                    _instance = initialCreator.Invoke();
                    if (_instance==null)
                    {
                        throw new ArgumentException("initialCreator 委托函数返回值为空，无法创建实例！");
                    }
                    return _instance;
                }
            }
            return _instance;
        }

        /// <summary>
        /// 单例实例
        /// </summary>
        public static T Instance => GetInstance();
    }
}
