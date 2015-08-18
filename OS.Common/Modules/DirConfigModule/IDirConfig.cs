using OS.Common.ComModels;

namespace OS.Common.Modules.DirConfigModule
{
    /// <summary>
    /// 字典配置接口
    /// </summary>
    public interface IDirConfig
    {

        /// <summary>
        /// 添加字典配置
        /// </summary>
        /// <param name="key">配置关键字</param>
        /// <param name="dirConfig">配置具体信息</param>
        /// <typeparam name="TConfig">配置信息类型</typeparam>
        /// <returns></returns>
        ResultModel SetDirConfig<TConfig>(string key, TConfig dirConfig) where TConfig : class ,new();


        /// <summary>
        /// 添加字典配置
        /// </summary>
        /// <param name="key">配置关键字</param>
        /// <typeparam name="TConfig">配置信息类型</typeparam>
        /// <returns></returns>
        TConfig GetDirConfig<TConfig>(string key) where TConfig : class ,new();

        /// <summary>
        /// 移除配置信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        ResultModel RemoveDirConfig(string key);

    }
}