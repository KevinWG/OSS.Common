#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：OSSCore

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
using System.Collections.Concurrent;
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

    [Obsolete("建议使用BaseApiConfigProvider")]
    public class BaseConfigProvider<TConfigType, TConfigOwnerType>:BaseMetaProvider<TConfigType> 
        where TConfigType : class
        where TConfigOwnerType : class
    {
        /// <inheritdoc />
        public BaseConfigProvider(TConfigType config):base(config)
        {
        }

        /// <summary>
        /// 微信接口配置
        ///  优先级： 上下文设置  =》 实例设置 =》 默认设置
        /// </summary>
        public TConfigType ApiConfig => GetConfig();
    }

    /// <inheritdoc />
    public class BaseApiConfigProvider<TConfigType> : BaseMetaProvider<TConfigType>
        where TConfigType : class
    {
        /// <inheritdoc />
        public BaseApiConfigProvider()
        {
        }

        /// <inheritdoc />
        public BaseApiConfigProvider(TConfigType config):base(config)
        {
        }

        /// <summary>
        /// 接口配置
        ///  优先级： 上下文设置  =》 实例设置 =》 默认设置
        /// </summary>
        public TConfigType ApiConfig => GetConfig();
    }

    /// <summary>
    ///   通用配置基类
    /// </summary>
    /// <typeparam name="TConfigType"></typeparam>
    public class BaseMetaProvider<TConfigType>
        where TConfigType : class
    {
        private TConfigType _config;

        private static AsyncLocal<ConcurrentDictionary<Type, TConfigType>>
            _contextConfig; // new AsyncLocal<TConfigType>();



        /// <summary>
        ///   当前模块名称
        /// </summary>
        public string ModuleName { get; set; } = ModuleNames.Default;

        private static readonly object _lockObj = new object();


        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseMetaProvider()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config"></param>
        public BaseMetaProvider(TConfigType config)
        {
            if (config != null)
                _config = config;
        }

        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <returns></returns>
        protected TConfigType GetConfig()
        {
            var contextConfig = GetContextConfig();
            if (contextConfig != null)
            {
                return contextConfig;
            }

            if (_config != null
                || (_config = GetDefaultConfig()) != null)
            {
                return _config;
            }

            throw new ArgumentNullException("当前配置信息为空，请通过构造函数中赋值，或者SetContextConfig方法设置当前上下文配置信息");
        }

        /// <summary>
        ///  配置模式    
        /// </summary>
        public ConfigProviderMode ConfigMode
        {
            get
            {
                if (_contextConfig?.Value != null)
                {
                    return ConfigProviderMode.Context;
                }

                return _config != null ? ConfigProviderMode.Instance : ConfigProviderMode.Default;
            }
        }



        /// <summary>
        /// 获取默认配置信息
        ///    如果上下文配置不存在，且构造函数也没有传入配置信息，执行此方法
        /// </summary>
        /// <returns></returns>
        protected virtual TConfigType GetDefaultConfig()
        {
            return null;
        }

        /// <summary>
        ///  设置上下文配置信息，当前配置在当前上下文中有效
        /// </summary>
        /// <param name="config"></param>
        public void SetContextConfig(TConfigType config)
        {
            if (_contextConfig == null)
            {
                lock (_lockObj)
                {
                    if (_contextConfig == null)
                    {

                        _contextConfig = new AsyncLocal<ConcurrentDictionary<Type, TConfigType>>
                        {
                            Value = new ConcurrentDictionary<Type, TConfigType>()
                        };
                    }
                }
            }

            _contextConfig.Value.AddOrUpdate(GetType(), config, (t, c) => config);
        }

        internal TConfigType GetContextConfig()
        {
            var dir = _contextConfig?.Value;
            if (dir == null)
            {
                return null;
            }

            return dir.TryGetValue(GetType(), out TConfigType va) ? va : null;
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
