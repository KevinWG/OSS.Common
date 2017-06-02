#OS.Common
	通用基础实现，提供主要有以下的内容：
	1. 基础用户系统设备信息实体定义。
	2. 主流加解密方案实现（）
	3. 常规实体DTO转化，静态扩展方法（时间，字符串等等处理）
	4. 基础日志，缓存，配置辅助静态类及默认方案实现
	5. 全局结果，分页实体定义

###Authrization
	用户授权信息模块，可以在上下文中使用MemberShiper，其中主要包含两个属性：
	1. AppAuthorize 
		对应的是应用授权信息主要是应用来源，客户端的类型等
	2. MemberInfo 
		对应的是当前的用户信息，用户名称 等
	MemberShiper 中提供了GetToken方法，方便加密用户Id，同时有一个对应的GetTokenDetail来从token中解密用户id信息
	使用的是加密方式为Aes加密

	使用方法：可以在请求中添加中间件或者filter，来对其进行赋值，然后可以在具体的业务中进行调用。
	需要注意的是以上两个变量皆为线程变量，如果你使用的异步线程，请注意传值给对应的线程。

###ComModels
	系统默认实体信息，分页实体 ， 结果实体，方便全局使用，
	其中结果实体ResultMo包含三个属性： 
	Ret-状态码，在ResultTypes的枚举中已经把常见的状态信息做了简单定义，这些状态码和Http状态码基本相对应，
	其中0表示为Success，同时定义了个IsSuccess()的方法扩展，此值为快速判断返回是否正确。

	Message-对应状态码的消息
	IsSuccess-快速判断状态码是否正确，默认0时正确

###Encrypt
	系统加密基础库
	主要包含：md5（Md5）,aes（AesRijndael）,sha1（Sha1）,hmacsha1（HmacSha1-加盐sha1加密方式这几种加密算法

###Extention
	系统扩展方法，主要包含：
	字符串转化扩展，如： "0".ToInt32()，"xxx".Base64UrlEncode()等
	时间转化扩展，如：DateTime.Now.ToUtcSeconds() 等
	Task扩展方法，如： Task.WaitResult() 等
	UrlCode扩展方法，如 "name=n&code=1".UrlEncode();
	枚举扩展方法，如： typeof(Enum).ToEnumDirs();
	xml序列化扩展方法， 如： "<xml><name>test</name></xml>".DeserializeXml<Type>();
	
	还有自定义验证属性像 OSRequiredAttribute等，可以使用IsOsValidate来验证是否验证通过。

###Modules
	系统扩展模块和对应的默认实现,其中主要有下边三个部分:

####一.CacheModule  
	缓存模块

####二.DirConfig  
	字典配置模块

####三.LogModule  
	日志模块


