#region Copyright (C) 2019 (OSS开源系列) 公众号：OSSCore

/***************************************************************************
*　　	文件功能描述：全局辅助类 - Ioc简单实例容器
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       
*       
*****************************************************************************/

#endregion

using System;

namespace OSS.Common.Helpers
{
    /// <summary>
    ///   Ioc简单实例容器实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class InsContainer<T>
    {
        private static Func<T> _insCreater;

        /// <summary>
        ///  具体实例 
        /// 根据设置时参数会返回具体单例还是新的实例
        /// </summary>
        public static T Instance => _insCreater != null
            ? _insCreater()
            : throw new NullReferenceException($"未能发现{typeof(T)}在容器具体映射类型/实例");


        /// <summary>
        ///  设置容器内映射的具体类型
        /// </summary>
        /// <typeparam name="TInstance"></typeparam>
        /// <param name="insCreater"></param>
        public static void Set<TInstance>(Func<T> insCreater)
            where TInstance : T
        {
            _insCreater = insCreater ?? throw new ArgumentNullException(nameof(insCreater), "参数不能为空！");
        }

        private static T _instance;
        private static readonly object _objLock = new object();

        /// <summary>
        /// 设置容器内映射的具体类型
        /// </summary>
        /// <typeparam name="TInstance"></typeparam>
        /// <param name="isSingle"></param>
        public static void Set<TInstance>(bool isSingle = true)
            where TInstance : T, new()
        {
            if (!isSingle)
            {
                _insCreater = () => new TInstance();
                return;
            }
            _insCreater = () =>
            {
                if (_instance == null)
                {
                    lock (_objLock)
                    {
                        if (_instance == null)
                        {
                            return _instance = new TInstance();
                        }
                    }
                }
                return _instance;
            };
        }
    }

   
}
