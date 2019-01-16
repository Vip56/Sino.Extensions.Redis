## 简介
该项目基于[CSRedis](https://github.com/Vip56/csredis)进行了源码的大量修改与删减，提供了日常所需的操作，同时不在依赖
`Microsoft.Extensions.Caching.Abstractions`库，从而提升该SDK能够覆盖的`.Net Core`环境，从而避免因不同`.Net Core`
环境需要进行调整，从而统一版本。   

[![Build status](https://ci.appveyor.com/api/projects/status/9an7h8nk47eeod05/branch/master?svg=true)](https://ci.appveyor.com/project/vip56/sino-extensions-redis/branch/master)  
  
## 如何使用
如果开发者需要在`Asp.Net Core`环境中进行使用，该库已经内置的IOC注入，开发者仅需要通过如下方式进行注入即可：
```
services.AddRedisCache(Action<RedisCacheOptions> setupAction);
```
其中`RedisCacheOptions`可以设置如下参数：   

* RedisCacheOptions.Host：Redis服务地址   
* RedisCacheOptions.Port：Redis服务端口   
* RedisCacheOptions.Password：Redis密码   
* RedisCacheOptions.InstanceName：Key前缀   

完成以上服务注册后，在需要使用的地方通过接口`IRedisCache`即可访问对应的服务，当前该接口公开了如下操作方法：   
```
// 返回Key所关联的字符串
string Get(string key);
Task<string> GetAsync(string key);

// 判断key是否存在
bool Exists(string key);
Task<bool> ExistsAsync(string key);

// 重新设置Key的超时时间
void Refresh(string key, int timeout);
Task RefreshAsync(string key, int timeout);

// 设置Key的超时时间，单位秒
bool Expire(string key, int seconds);
Task<bool> ExpireAsync(string key, int seconds);

// 设置Key的超时时间，单位毫秒
bool PExpire(string key, long milliseconds);
Task<bool> PExpireAsync(string key, long milliseconds);

// 移除Key
void Remove(string key);
Task RemoveAsync(string key);

// 设置Key的值
void Set(string key, string value, int? timeout = null);
Task SetAsync(string key, string value, int? timeout = null);

// 仅当Key不存在则设置其值
bool SetNx(string key, string value);
Task<bool> SetNxAsync(string key, string value);

/// 将value追加到key值后，不存在则等同set
long Append(string key, string value);
Task<long> AppendAsync(string key, string value);

// 返回key所存储的字符长度
long StrLen(string key);
Task<long> StrLenAsync(string key);

// 获取key字符串中子字符串
string GetRange(string key, long start, long end);
Task<string> GetRangeAsync(string key, long start, long end);

// 计算给定字符串中被设置为1的比特位数量
long BitCount(string key, long? start = null, long? end = null);
Task<long> BitCountAsync(string key, long? start = null, long? end = null);

// 对Key所储存的字符串值设置或清除指定偏移量上的位
bool SetBit(string key, uint offset, bool value);
Task<bool> SetBitAsync(string key, uint offset, bool value);

// 获取Key存储的字符串上指定偏移量的位
bool GetBit(string key, uint offset);
Task<bool> GetBitAsync(string key, uint offset);

// 将Key中储存的数字值减一
long Decr(string key);
Task<long> DecrAsync(string key);

// 将Key中储存的数字减少固定值
long DecrBy(string key, long decrement);
Task<long> DecrByAsync(string key, long decrement);

// 将Key中储存的数字增加一
long Incr(string key);
Task<long> IncrAsync(string key);

// 将Key中储存的数字增加值
long IncrBy(string key, long increment);
Task<long> IncrByAsync(string key, long increment);

// 删除哈希表Key中的一个或多个指定域
long HDel(string key, params string[] fields);
Task<long> HDelAsync(string key, params string[] fields);

// 查看哈希表Key中给定域field是否存在
bool HExists(string key, string field);
Task<bool> HExistsAsync(string key, string field);

// 返回哈希表Key中指定域的值
string HGet(string key, string field);
Task<string> HGetAsync(string key, string field);

// 返回哈希表Key中域的数量
long HLen(string key);
Task<long> HLenAsync(string key);

// 将哈希表Key中的域的值设置为value
bool HSet(string key, string field, string value);
Task<bool> HSetAsync(string key, string field, string value);

// 将哈希Key中的域的值设置为Value，当且仅当域field不存在
bool HSetNx(string key, string field, string value);
Task<bool> HSetNxAsync(string key, string field, string value);

// 采用阻塞模式获取指定Key数组中任意值
Tuple<string, string> BLPop(int timeout, params string[] keys);
Task<Tuple<string, string>> BLPopAsync(int timeout, params string[] keys);

// 移除并返回列表key的头元素
string LPop(string key);
Task<string> LPopAsync(string key);

// 采用阻塞模式获取指定Key数组中任意值，如果超时则返回RedisProtocolException异常。
Tuple<string, string> BRPop(int timeout, params string[] keys);
Task<Tuple<string, string>> BRPopAsync(int timeout, params string[] keys);

// 返回列表Key中指定下标的元素
string LIndex(string key, long index);
Task<string> LIndexAsync(string key, long index);

// 返回列表Key的长度
long LLen(string key);
Task<long> LLenAsync(string key);

// 将一个或多个值插入到列表key的表头
long LPush(string key, params string[] values);
Task<long> LPushAsync(string key, params string[] values);

// 仅当key存在且为列表则将value插入表头
long LPushX(string key, string value);
Task<long> LPushXAsync(string key, string value);

// 移除并返回列表key的尾元素
string RPop(string key);
Task<string> RPopAsync(string key);

// 将一个或多个值插入到列表尾巴
long RPush(string key, params string[] values);
Task<long> RPushAsync(string key, params string[] values);

// 仅当key存在且为列表则将值插入到表尾巴
long RPushX(string key, params string[] values);
Task<long> RPushXAsync(string key, params string[] values);

// 采用阻塞模式获取source列表中的末尾元素，返回
// 的同时将其添加到destination的头部，如果source
// 不存在任何数据则等待，直到指定的timeout超时。
string BRPopLPush(string source, string destination, int timeout);
Task<string> BRPopLPushAsync(string source, string destination, int timeout);

// 将列表source的最后一个元素弹出并返回，同时将
// 这个元素添加到destination列表的头元素。
string RPopLPush(string source, string destination);
Task<string> RPopLPushAsync(string source, string destination);

// 移除列表key中count个与参数value相等的元素，如果
// count大于0则从表头开始向表尾搜索，如果count小于0
// 则从表尾向表头搜索，count为0则移除所有。
long LRem(string key, long count, string value);
Task<long> LRemAsync(string key, long count, string value);
```   

如果开发者需要通过其他的方式使用，可以直接使用`PoolRedisClient`对象直接进行使用，只需要按照如下方式
进行初始化即可：
```
var client = new RedisCache(new RedisCacheOptions
{
	Host = "127.0.0.1",
	Port = 6379,
	InstanceName = "console_"
});
```   

## 项目设计

### 项目结构
* Sino.Extensions.Redis：核心类库；
* RedisUnitTest：单元测试；
* RedisConsole：实际连接测试；
    
### 主要结构
考虑到实际使用项目进行的大量的删减和重构，所以项目比较小巧，这样便于调试与扩展，主要的目录结构如下所示：
* Commands：所有Redis命令的均在该文件夹下；
* Internal：主要负责Socket的连接以及字节流的发送以及接收；
* Types：Redis命令中需要使用的枚举参数值；
* Utils：扩展官方类，便于开发；   

### 核心类   
为了让开发者快速了解项目从而进行上手，相关的核心类将会进行介绍，便于开发者进行查找：   

#### RedisCache类
该类主要实现了`IRedisCache`接口，提供给`Asp.Net Core`项目下进行使用，同时根据实际项目使用的经验将不常用的
Redis方法进行了删减，仅留下常用的Redis操作指令，通过对该类以及对应接口的修改可以影响到所有使用该类库的`Asp.Net Core`
类库，考虑到兼容性建议开发者切勿删除方法或增加必填参数从而影响业务开发。   

#### PoolRedisClient类
该类主要实现了并发多实例的管理，可以应对高并发场景下对于Redis的考验，当然其中主要是依赖`RedisClient`进行实际的
指令执行的，所以我们可以看到该类基本就是依靠`ConcurrentQueue`与`SemaphoreSlim`进行管理。   

#### XXXXCommands类
该类主要是根据Redis指令的组合进行划分，从该项目中可以发现主要实现了`Connection`、`Hash`、`Key`、`List`、`Set`和`String`
这几种最常用的Redis数据结构，通过改部分可以观察各种Redis指令的拼接。   

#### ReturnTypeWithXXX类   
该类主要是根据Redis指令的返回类型进行汇总，主要被`XXXXCommands`类使用，也是实际存储和拼接Redis指令的对象。   

#### RedisConnector类   
底层负责连接和命令执行与结果解析类，该类中使用了`AsyncConnector`类负责异步方法的执行，通过`RedisIO`负责实际的
数据字节的发给与接收。    

#### RedisIO类   
该类主要负责对Socket流的发送与接收，其中使用`RedisWriter`和`RedisReader`进行基本的底层Redis协议的转换发送与接收。    

### 如何扩展
随着Redis版本的更新越来越多的新指令将会出现，考虑到各种第三方类库的更新进度的限制，为了便于开发者能够快速的自行进行扩展
以下将介绍如何进行指令的快速扩展，首先我们需要根据文档确定其指令的返回数据的类型结构，然后新建对应的`XXXXCommands`文件
然后在其中写入对应的方法，比如Key的Del方法：
```
/// <summary>
/// 删除给定的一个或多个key，不存在的key将会忽略。
/// </summary>
/// <param name="keys">需要删除的key</param>
/// <returns>命令对象</returns>
public static ReturnTypeWithInt Del(params string[] keys)
{
	return new ReturnTypeWithInt("DEL", keys);
}
```
通过如上的方式就可以快速的增加一个新的方法，最后对应的需要在`PoolRedisClient`对象中进行公开，否则我们的单元测试
或者实际使用都无法访问，比如如上的方法在`PoolRedisClient`这样写：
```
public long Del(params string[] keys) => Multi(KeyCommands.Del(keys));
```
可以看到我们仅仅只是需要将对应的Commands的方法名通过`Multi`方法传入即可。这样就很容易的扩展了一个新的Redis指令。   

### 常见使用场景
1. 通过`SetNx`可以实现简单的分布式锁；
2. 通过`Append`、`StrLen`和`GetRange`可以实现时间序列；
3. 通过`BitCount`、`SetBit`和`GetBit`可以实现位图数据；
4. 通过`Decr`、`DecrBy`、`Incr`和`IncrBy`可以实现计数器和限速器；
5. 通过`HDel`、`HExists`、`HGet`、`HLen`、`HSet`和`HSetNx`可以实现内存字典；
6. 通过`BLPop`、`BRPop`、`LIndex`、`LLen`、`LPush`、`LPushX`、`RPop`、`RPush`和`RPushX`可以实现事件处理；
7. 通过`BRPOPLPUSH`、`RPOPLPUSH`和`LRem`可以实现安全队列和循环列表；
   
### 注意
* 已经使用老版本的注意，当前新版已经采用了独立的`IRedisCache`接口，而不在实现`Asp.Net Core`的接口。