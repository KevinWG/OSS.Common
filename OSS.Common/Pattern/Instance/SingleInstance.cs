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


using System.Diagnostics.CodeAnalysis;

namespace OSS.Common
{
    /// <summary>
    /// 单例基础实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
   public class SingleInstance<T>
        where T : class
    {
        private static          T?     _instance;
        private static readonly object _objLock = new();

        /// <summary>
        ///  获取单例实例
        /// </summary>
        /// <param name="initialCreator"></param>
        /// <returns></returns>
        [SuppressMessage("ReSharper", "PossibleMultipleWriteAccessInDoubleCheckLocking")]
        [SuppressMessage("ReSharper", "ReadAccessInDoubleCheckLocking")]
        public static T GetInstance(Func<T>? initialCreator = null)
        {
            if (_instance != null)
                return _instance;
            
            if (initialCreator==null && typeof(T).GetConstructor(Type.EmptyTypes) == null)
            {
                throw new ArgumentException($"委托创建方法为空，且{typeof(T).Name}不存在无参构造函数，无法创建实例！");
            }

            lock (_objLock)
            {
                if (_instance != null)
                    return _instance;

                _instance = initialCreator == null ? Activator.CreateInstance<T>() : initialCreator.Invoke();
                if (_instance == null)
                {
                    throw new ArgumentException("initialCreator 委托函数返回值为空，无法创建实例！");
                }
                return _instance;
            }
        }

        /// <summary>
        /// 单例实例
        /// </summary>
        public static T Instance => GetInstance();
    }
    
}
