#OSS.Common


## Authrization
用户授权信息模块，在当前请求时对其进行赋值。
	AppAuthorize 对应的是应用授权信息主要是应用来源，客户端的类型等
	MemberInfo 对应的是当前的用户信息，用户名称 id  已经对应的扩展信息
在当前Request中对以上两个变量赋值，方便在具体方法内使用，注意以上两个是线程静态变量，在请求过程中如果多线程需要注意变量传递

##  ComModels
系统默认实体信息，分页实体  和 ResultMo

##  ComUtils
通用静态帮助方法

##  Encrypt
系统加密基础库

##  Extention
系统扩展方法   如： "0".ToInt32()，以及DTO等实体映射属性赋值等扩展方法

##  Modules
系统扩展模块和对应的默认实现
###  AsynModule  
异步模块

###  CacheModule  
缓存模块

###  DirConfig  
字典配置模块

###  LogModule  
日志模块
