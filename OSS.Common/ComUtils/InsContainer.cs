using System;

namespace OSS.Common.ComUtils
{
    /// <summary>
    ///   Ioc简单实例容器实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class InsContainer<T>
        where T: new()
    {
        /// <summary>
        ///  具体实例 
        /// 根据设置时参数会返回具体单例还是新的实例
        /// </summary>
        public static T Instance {
            get
            {
                if (_insCreater != null)
                {
                    return _insCreater();
                }
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

        private static T _instance;
        private static Func<T> _insCreater;

        private static readonly object _objLock=new object();
        /// <summary>
        ///  设置具体类型映射关系
        /// </summary>
        /// <typeparam name="TInstance"></typeparam>
        /// <param name="insCreater"></param>
        public static void Set<TInstance>(Func<T> insCreater)
            where TInstance : T
        {
            if (insCreater == null)
            {
                throw new ArgumentNullException(nameof(insCreater), "参数不能为空！");
            }
            _insCreater = insCreater;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInstance"></typeparam>
        /// <param name="isSingle"></param>
        public static void Set<TInstance>(bool isSingle=true)
            where TInstance : T, new()
        {
            if (!isSingle)
            {
                _insCreater = () => new TInstance();
            }
            else
            {
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
}
