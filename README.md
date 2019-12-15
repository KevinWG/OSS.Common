# OS.Common
通用基础实现，当前是以标准库的形式提供，在net46分支下有标准库和Framework 4.6的两个版本实现吧  
如果有问题，也可以在公众号(OSSCore)中提问:  
![OSSCore](http://img1.static.OSSCore.com/wei_qr.jpg)  

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

## BasicImpls
  系统默认实体信息如应用配置实体，基类BaseMo，及通用状态枚举
  通用响应实体Resp（还包括IdResp,LongIdResp,ListResp，PageListResp），以及对应相关扩展方法实现。

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

