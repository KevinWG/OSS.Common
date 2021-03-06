﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace OSS.Common.BasicImpls
{
    /// <inheritdoc />
  [Obsolete("使用 BaseMetaImpl ")] 
    public class BaseApiConfigProvider<TConfigType> : BaseMetaProvider<TConfigType>
        where TConfigType : class
    {
        /// <inheritdoc />
        public BaseApiConfigProvider()
        {
        }

        /// <inheritdoc />
        public BaseApiConfigProvider(TConfigType config) : base(config)
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
    [Obsolete("使用 BaseMetaImpl ")]
    public class BaseMetaProvider<TConfigType>
        where TConfigType : class
    {
        private TConfigType _configInstance;
        private static AsyncLocal<Dictionary<object, TConfigType>>
            _contextConfig; // new AsyncLocal<TConfigType>();

        /// <summary>
        ///   当前模块名称
        /// </summary>
        [Obsolete]
        public string ModuleName { get; set; } = "default";

        /// <summary>
        ///  配置模式    
        /// </summary>
        public ConfigProviderMode ConfigMode { get; private set; } = ConfigProviderMode.Default;


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
            SetInstanceConfig(config);
        }
        /// <summary>
        /// 设置实例配置信息
        /// </summary>
        /// <param name="config"></param>
        private void SetInstanceConfig(TConfigType config)
        {
            ConfigMode = ConfigProviderMode.Instance;
            _configInstance = config;
        }

        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <returns></returns>
        protected TConfigType GetConfig()
        {
            TConfigType t = default;
            switch (ConfigMode)
            {
                case ConfigProviderMode.Default:
                    t = GetDefaultConfig();
                    break;
                case ConfigProviderMode.Context:
                    t = GetContextConfig();
                    break;
                case ConfigProviderMode.Instance:
                    t = _configInstance;
                    break;
            }

            t ??= _configInstance ?? GetDefaultConfig();

            if (t != null)
                return t;

            throw new ArgumentNullException(
                "当前配置信息为空，请通过构造函数中赋值，或重写GetDefaultConfig返回默认设置，或者SetContextConfig方法设置当前上下文配置信息");
        }

        #region 扩展虚方法

        /// <summary>
        /// 获取默认配置信息
        ///    如果上下文配置不存在，且构造函数也没有传入配置信息，执行此方法
        /// </summary>
        /// <returns></returns>
        protected virtual TConfigType GetDefaultConfig()
        {
            return null;
        }

        #endregion

        #region 赋值操作

        private static readonly object _lockObj = new object();

        /// <summary>
        ///  设置上下文配置信息，当前配置在当前上下文中有效
        /// </summary>
        /// <param name="config"></param>
        public void SetContextConfig(TConfigType config)
        {
            if (_contextConfig?.Value == null)
            {
                lock (_lockObj)
                {
                    if (_contextConfig?.Value == null)
                    {
                        _contextConfig ??= new AsyncLocal<Dictionary<object, TConfigType>>();

                        _contextConfig.Value = new Dictionary<object, TConfigType>();
                    }
                }
            }

            ConfigMode = ConfigProviderMode.Context;
            if (_contextConfig.Value.ContainsKey(this))
                _contextConfig.Value[this] = config;
            else
                _contextConfig.Value.Add(this, config);
        }
      

        #endregion
        
     

        private TConfigType GetContextConfig()
        {
            var dir = _contextConfig?.Value;
            if (dir == null)
            {
                return null;
            }
            return dir.TryGetValue(this, out TConfigType va) ? va : null;
        }
    }

    /// <summary>
    ///  配置的提供方式
    /// </summary>
    public enum ConfigProviderMode
    {
        /// <summary>
        /// 默认配置
        /// </summary>
        Default,

        /// <summary>
        ///  实例构造函数设置
        /// </summary>
        Instance,

        /// <summary>
        ///  上下文设置
        /// </summary>
        Context
    }
}
