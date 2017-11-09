# OS.Common
通用基础实现，当前是以标准库的形式提供，在net46分支下有标准库和Framework 4.6的两个版本实现吧  
如果有问题，也可以在公众号(osscoder)中提问:  
![osscoder](http://img1.static.osscoder.com/wei_qr.jpg)  

当前类库主要提供以下的内容：  
1. 基础用户系统设备信息实体定义。
2. 主流加解密方案实现（）
3. 常规实体DTO转化，静态扩展方法（时间，字符串等等处理）
4. 基础日志，缓存，配置辅助静态类及默认方案实现
5. 全局结果，分页实体定义
## Authrization
 用户授权信息模块，可以在上下文中使用MemberShiper，其中主要包含两个属性：  

 1. AppAuthorize   
	对应的是应用授权信息主要是应用来源，客户端的类型等
 2. Identity   
	对应的是当前的用户信息，用户名称 等  
 MemberShiper 中提供了GetToken方法，方便加密用户Id，同时有一个对应的GetTokenDetail来从token中解密用户id信息  
 使用的是加密方式为Aes加密


## ComModels
 系统默认实体信息如应用配置实体，分页实体，结果实体，方便全局使用  
 其中结果实体ResultMo包含两个属性比较重要：  
 ret-状态码，在ResultTypes的枚举中已经把常见的状态信息做了简单定义  
	其中0表示为Success，同时定义了个IsSuccess()的方法扩展，此值为快速判断返回是否正确  
 msg-对应状态码的消息  

## Encrypt
 系统加密基础库，主要包含：  
 md5（Md5）,aes（AesRijndael）,sha1（Sha1）,hmac（HMACSHA）-加盐sha加密方式这几种加密算法  

## Extention
 系统扩展方法，主要包含：  
 字符串转化扩展，如： "0".ToInt32()，"xxx".Base64UrlEncode()等  
 时间转化扩展，如：DateTime.Now.ToUtcSeconds() 等  
Task扩展方法，如： Task.WaitResult() 等  
UrlCode扩展方法，如 "name=n&code=1".UrlEncode();  
枚举扩展方法，如： typeof(Enum).ToEnumDirs();  
xml序列化扩展方法， 如： "<xml><name>test</name></xml>".DeserializeXml<Type>();  

## Plugs
系统扩展模块和对应的默认实现，同时在Plugs目录下定义了ModuleNames类，含有系统常见模块  
如果你有高级定制需求，比如在系统不同模块使用插件的不同实现，可以在程序入口处定义指定模块名称对应的插件实现，而无需修改业务代码  

这里主要包含以下三种插件  

### 一.CachePlug  
缓存模块，内部定义了ICachePlug接口，和一个 DefaultCachePlug默认缓存实现  
使用时，可以直接使用 CacheUtil 静态类下的方法，比如Set方法  
	
	CacheUtil.Set("key","va",TimeSpan.FromHours(1),"moduleName")

其中 modulename 是可选参数  
如果传入，则在程序入口处可以给 OsConfig.CacheProvider(Func<string, ICachePlug>) 委托赋值  
  我们就可以根据使用频率，数据重要性，以及和其它系统的依赖情况来定义特定模块下的不同缓存实现  
  比如设置不同的模块分别Redis，Memcache等，又或者我们可以设置不同的模块使用Redis的不同实例。  
	
当然，如果这个委托没有定义，或者委托返回空，就会使用默认缓存实现，如果我们希望全站换到相同的缓存实现，则直接返回对应实现即可。  

### 二.DirConfigPlug  
配置模块， 和缓存的实现形式相同，定义了一个 IDirConfigPlug 接口，和一个 DefaultDirConfigPlug 实现  
使用时，直接使用 DirConfigUtil 静态类。  
默认实现是利用Xml序列化的方式，保存到本地文件中，依然可以定义指定模块名称的特定数据源的配置实例。  
比如项目本身的配置可以设置数据库实现，依赖其他项目的，可以通过接口实现。  


### 三.LogPlug  
日志模块，一样，内部定义了一个 ILogPlug 接口，和一个 DefaultLogPlug 默认实现  
使用时，直接使用 LogUtil 静态类。  
默认实现是通过保存到本地文件的形式实现，每个小时生成一个文件。  
依然可以定义指定模块名称的特定数据源的配置实例，比如有些发送邮件，有些发送短信通知等。  


