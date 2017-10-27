#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：OSSCommon - 通用应用账号配置信息
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       创建时间： 2017-5-25
*       
*****************************************************************************/

#endregion

using System;
using System.Threading;
using OSS.Common.Plugs;

namespace OSS.Common.ComModels
{
    /// <summary>
    /// 通用应用账号配置信息  
    /// </summary>
    public class AppConfig
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AppConfig()
        {
        }
        
        /// <summary>
        ///  构造函数
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        public AppConfig(string appId, string appSecret)
        {
            AppId = appId;
            AppSecret = appSecret;
        }

        /// <summary>
        /// 应用账号AppId
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 应用账号AppSecret
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// 操作管理类型
        /// </summary>
        public AppOperateMode OperateMode { get; set; } = AppOperateMode.BySelf;

        /// <summary>
        ///  当 OperateMode = AppOperateMode.ByAgent 时
        ///  代理应用的账号AppId
        /// </summary>
        public string ByAppId { get; set; }
    }

    /// <summary>
    /// 应用操作类型
    /// </summary>
    public enum AppOperateMode
    {
        /// <summary>
        ///  自管理
        /// </summary>
        BySelf,
        /// <summary>
        /// 代理操作
        /// </summary>
        ByAgent
    }


    /// <summary>
    ///   通用配置基类
    /// </summary>
    /// <typeparam name="TConfigType"></typeparam>
    /// <typeparam name="TConfigOwnerType">配置的使用者（防止在同一线程中同一配置类型有两个不同的使用者设置上下文配置信息）</typeparam>
    public class BaseConfigProvider<TConfigType,TConfigOwnerType>
        where TConfigType : class
        where TConfigOwnerType : class
    {

        #region  接口配置信息

        private static readonly AsyncLocal<TConfigType> _contextConfig = new AsyncLocal<TConfigType>();
        private TConfigType _config;

        /// <summary>
        ///  设置上下文配置信息，当前配置在当前上下文中有效
        /// </summary>
        /// <param name="config"></param>
        public static void SetContextConfig(TConfigType config)
        {
            _contextConfig.Value = config;
        }

        /// <summary>
        /// 微信接口配置
        ///  优先级： 上下文设置  =》 实例设置 =》 默认设置
        /// </summary>
        public TConfigType ApiConfig
        {
            get
            {
                if (_contextConfig.Value != null)
                {
                    return _contextConfig.Value;
                }

                if (_config != null 
                    || (_config = GetDefaultConfig()) != null)
                {
                    return _config;
                }

                throw new ArgumentNullException("当前配置信息为空，请通过构造函数中赋值，或者SetContextConfig方法设置当前上下文配置信息");
            }
        }



        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config"></param>
        public BaseConfigProvider(TConfigType config)
        {
            if (config != null)
                _config = config;
        }


        #endregion

        /// <summary>
        ///   当前模块名称
        /// </summary>
        public string ModuleName { get; set; } = ModuleNames.Default;

        /// <summary>
        /// 获取默认配置信息
        /// 如果上下文配置不存在，且构造函数也没有传入配置信息，执行此方法
        /// </summary>
        /// <returns></returns>
        protected virtual TConfigType GetDefaultConfig()
        {
            return null;
        }

        /// <summary>
        ///  配置模式    
        /// </summary>
        public ConfigProviderMode ConfigMode
        {
            get
            {
                if (_contextConfig.Value != null)
                {
                    return ConfigProviderMode.Context;
                }
                return _config != null ? ConfigProviderMode.Instance : ConfigProviderMode.Default;
            }
        }
    }

    /// <summary>
    ///  配置的提供方式
    /// </summary>
    public enum ConfigProviderMode
    {
        /// <summary>
        ///  实例构造函数设置
        /// </summary>
        Instance,


        /// <summary>
        ///  上下文设置
        /// </summary>
        Context,


        /// <summary>
        /// 默认配置
        /// </summary>
        Default

    }

}
