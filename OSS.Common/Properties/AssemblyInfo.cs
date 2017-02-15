using System.Resources;
using System.Reflection;

// 有关程序集的一般信息由以下
// 控制。更改这些特性值可修改
// 与程序集关联的信息。
[assembly: AssemblyTitle("OSS.Common")]
#if NETFW
[assembly: AssemblyDescription("通用基础 Framework 版本实现")]
#else
[assembly: AssemblyDescription("通用基础 Standard 版本实现")]
#endif
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("OSS")]
[assembly: AssemblyProduct("OSS.Common")]
[assembly: AssemblyCopyright("版权所有(C) OSS开源系列 2017")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: NeutralResourcesLanguage("zh-Hans")]

// 程序集的版本信息由下列四个值组成: 
//
//      主版本
//      次版本
//      生成号
//      修订号
//
//可以指定所有这些值，也可以使用“生成号”和“修订号”的默认值，
// 方法是按如下所示使用“*”: :
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.1.2.2")]
[assembly: AssemblyFileVersion("1.1.2.2")]
