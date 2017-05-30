using System;

namespace OSS.Common.ComUtils
{
    /// <summary>
    ///   Ioc简单实例容器实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class InsContainer<T>
    {
        /// <summary>
        ///  具体实例 
        /// 根据设置时参数会返回具体单例还是新的实例
        /// </summary>
        public static T Instance
        {
            get
            {
                if (instanceCreater == null)
                    throw new Exception(string.Concat( " 并没有设置", typeof(T).Name, "对应的映射类型！ "));
                return instanceCreater();
            }
        }


        private static Func<T> instanceCreater;
        private static T instance;

        /// <summary>
        ///  设置具体类型映射关系
        /// </summary>
        /// <typeparam name="TInstance"></typeparam>
        /// <param name="isSingle">是否是单例模式</param>
        public static void Set<TInstance>(bool isSingle = true)
            where TInstance : T, new()
        {
            if (instanceCreater == null)
                instanceCreater = () =>
                {
                    if(isSingle) return new TInstance();

                    if (instance != null) return instance;
                    return instance = new TInstance();
                };
        }
    }
}
