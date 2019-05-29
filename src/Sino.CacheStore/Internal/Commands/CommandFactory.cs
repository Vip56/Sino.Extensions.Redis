using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.CacheStore.Internal
{
    /// <summary>
    /// 命令工厂基类
    /// </summary>
    public class CommandFactory : ICommandFactory
    {
        public string Name { get; set; }

        public event EventHandler<CacheStoreCommand> OnCreateCommand;

        public CommandFactory(string name)
        {
            Name = name;
        }

        protected void OnCommand(CacheStoreCommand command)
        {
            OnCreateCommand?.Invoke(this, command);
        }

        /// <summary>
        /// 进行授权
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns>命令对象</returns>
        public virtual StatusCommand CreateAuthCommand(string token) => throw new NotImplementedException();

        /// <summary>
        /// 心跳检测，通常用于测试与服务的链接是否仍然生效，或者用于测量延迟值。
        /// </summary>
        /// <returns>命令对象</returns>
        public virtual StatusCommand CreatePingCommand() => throw new NotImplementedException();

        /// <summary>
        /// 请求服务器关闭与当前客户端的连接
        /// </summary>
        /// <returns>命令对象</returns>
        public virtual StatusCommand CreateQuitCommand() => throw new NotImplementedException();

        /// <summary>
        /// 切换到指定的数据库
        /// </summary>
        /// <param name="dbName">数据库索引</param>
        /// <returns>命令对象</returns>
        public virtual StatusCommand CreateSelectCommand(string dbName) => throw new NotImplementedException();

        /// <summary>
        /// 检查给定key是否存在
        /// </summary>
        /// <param name="key">需要判断的key</param>
        /// <returns>命令对象</returns>
        public virtual BoolCommand CreateExistsCommand(string key) => throw new NotImplementedException();

        /// <summary>
        /// 返回key所关联的字符串值。
        /// </summary>
        /// <param name="key">需要获取的key</param>
        /// <returns>命令对象</returns>
        public virtual BytesCommand CreateGetCommand(string key) => throw new NotImplementedException();

        /// <summary>
        /// 设置指定key的值
        /// </summary>
        /// <param name="key">需要设置的key</param>
        /// <param name="value">需要设置的值</param>
        /// <param name="expirationSeconds">过期时间（秒）</param>
        /// <param name="expirationMilliseconds">过期时间（毫秒）</param>
        /// <param name="exists">其他限定条件</param>
        /// <returns>命令对象</returns>
        public virtual StatusCommand CreateSetCommand(string key, object value, int? expirationSeconds = null, long? expirationMilliseconds = null, CacheStoreExistence? exists = null) => throw new NotImplementedException();

        /// <summary>
        /// 为给定key设置生存时间，当key过期时，它会被自动删除。
        /// </summary>
        /// <remarks>
        /// 在Redis 2.4版本中，过期时间的延迟在1秒钟之内，也就是说
        /// key已经过期，但它还是可能在过期之后一秒中之内被访问到，
        /// 而在新的Redis 2.6版本中，延迟降低到1毫秒之内。
        /// </remarks>
        /// <param name="key">需要设置的key</param>
        /// <param name="seconds">设置的时间，单位为秒</param>
        /// <returns>命令对象</returns>
        public virtual BoolCommand CreateExpireCommand(string key, int seconds) => throw new NotImplementedException();

        /// <summary>
        /// 为给定key设置生存时间，当key过期时，它会被自动删除。
        /// </summary>
        /// <param name="key">需要设置的key</param>
        /// <param name="milliseconds">超时时间，单位为毫秒</param>
        /// <returns>命令对象</returns>
        public virtual BoolCommand CreatePExpireCommand(string key, long milliseconds) => throw new NotImplementedException();

        /// <summary>
        /// 删除给定的一个或多个key，不存在的key将会忽略。
        /// </summary>
        /// <param name="keys">需要删除的key</param>
        /// <returns>命令对象</returns>
        public virtual IntCommand CreateRemoveCommand(params string[] keys) => throw new NotImplementedException();

        /// <summary>
        /// 删除哈希表key中的一个或多个指定域，不存在的域将被忽略。
        /// </summary>
        /// <param name="key">需要删除的key</param>
        /// <param name="fields">需要删除的域数组</param>
        /// <returns>命令对象</returns>
        public virtual IntCommand CreateHDelCommand(string key, params string[] fields) => throw new NotImplementedException();

        /// <summary>
        /// 查看哈希表key中给定域field是否存在
        /// </summary>
        /// <param name="key">需要查询的key</param>
        /// <param name="field">需要查询的域</param>
        /// <returns>命令对象</returns>
        public virtual BoolCommand CreateHExistsCommand(string key, string field) => throw new NotImplementedException();

        /// <summary>
        /// 返回哈希表key中给定域的值
        /// </summary>
        /// <param name="key">需要查询的key</param>
        /// <param name="field">需要查询的域</param>
        /// <returns>命令对象</returns>
        public virtual BytesCommand CreateHGetCommand(string key, string field) => throw new NotImplementedException();

        /// <summary>
        /// 返回哈希表key中域的数量。
        /// </summary>
        /// <param name="key">需要查询的key</param>
        /// <returns>命令对象</returns>
        public virtual IntCommand CreateHLenCommand(string key) => throw new NotImplementedException();

        /// <summary>
        /// 将哈希表key中的域的值设置为value。如果key和域不存在
        /// 则会自动创建，如果域已经存在，旧值将会覆盖。
        /// </summary>
        /// <param name="key">需要保存的key</param>
        /// <param name="field">需要保存的域</param>
        /// <param name="value">需要保存的值</param>
        /// <returns>命令对象</returns>
        public virtual BoolCommand CreateHSetCommand(string key, string field, byte[] value) => throw new NotImplementedException();

        /// <summary>
        /// 将哈希表key中的域的值设置为value,当且仅当域field不存在。
        /// </summary>
        /// <param name="key">需要保存的key</param>
        /// <param name="field">需要保存的域</param>
        /// <param name="value">需要保存的值</param>
        /// <returns>命令对象</returns>
        public virtual BoolCommand CreateHSetWithNoExistCommand(string key, string field, byte[] value) => throw new NotImplementedException();

        /// <summary>
        /// 移除并返回列表key的头元素
        /// </summary>
        /// <param name="key">列表key</param>
        /// <returns>命令对象</returns>
        public virtual BytesCommand CreateLPopCommand(string key) => throw new NotImplementedException();

        /// <summary>
        /// 返回列表key中指定下标的元素。
        /// </summary>
        /// <param name="key">需要查询的列表key</param>
        /// <param name="index">下标，从0开始</param>
        /// <returns>命令对象</returns>
        public virtual BytesCommand CreateLIndexCommand(string key, long index) => throw new NotImplementedException();

        /// <summary>
        /// 返回列表key的长度。
        /// </summary>
        /// <param name="key">查询的key</param>
        /// <returns>命令对象</returns>
        public virtual IntCommand CreateLLenCommand(string key) => throw new NotImplementedException();

        /// <summary>
        /// 将一个或多个值插入到列表key的表头
        /// </summary>
        /// <param name="key">列表key</param>
        /// <param name="value">插入的值</param>
        /// <returns>命令对象</returns>
        public virtual IntCommand CreateLPushCommand(string key, object value) => throw new NotImplementedException();

        /// <summary>
        /// 移除并返回列表key的尾元素
        /// </summary>
        /// <param name="key">列表key</param>
        /// <returns>命令对象</returns>
        public virtual BytesCommand CreateRPopCommand(string key) => throw new NotImplementedException();

        /// <summary>
        /// 将一个或多个值value插入到列表key的尾巴。
        /// </summary>
        /// <param name="key">列表key</param>
        /// <param name="values">添加的值</param>
        /// <returns>命令对象</returns>
        public virtual IntCommand CreateRPushCommand(string key, object value) => throw new NotImplementedException();

        /// <summary>
        /// 计算给定字符串中被设置为1的比特位数量。
        /// </summary>
        /// <param name="key">需要计算的key</param>
        /// <param name="start">起始位，可选</param>
        /// <param name="end">结束位，可选</param>
        /// <returns>命令对象</returns>
        public virtual IntCommand CreateBitCountCommand(string key, long? start = null, long? end = null) => throw new NotImplementedException();

        /// <summary>
        /// 对key所储存的字符串值设置或清除指定偏移量上的位
        /// </summary>
        /// <param name="key">需要设置的key</param>
        /// <param name="offset">偏移量</param>
        /// <param name="value">设置的值</param>
        /// <returns>命令对象</returns>
        public virtual BoolCommand CreateSetBitCommand(string key, uint offset, bool value) => throw new NotImplementedException();

        /// <summary>
        /// 获取key存储的字符串上指定偏移量上的位
        /// </summary>
        /// <param name="key">需要获取的key</param>
        /// <param name="offset">偏移量</param>
        /// <returns>命令对象</returns>
        public virtual BoolCommand CreateGetBitCommand(string key, uint offset) => throw new NotImplementedException();

        /// <summary>
        /// 将key中储存的数字值减一。
        /// </summary>
        /// <param name="key">需要计算的key</param>
        /// <returns>命令对象</returns>
        public virtual IntCommand CreateDecrCommand(string key) => throw new NotImplementedException();

        /// <summary>
        /// 将key中储存的数字减decrement。
        /// </summary>
        /// <param name="key">需要计算的key</param>
        /// <param name="decrement">需要减去的值</param>
        /// <returns>命令对象</returns>
        public virtual IntCommand CreateDecrByCommand(string key, long decrement) => throw new NotImplementedException();

        /// <summary>
        /// 将key中存储的数字值增一。
        /// </summary>
        /// <param name="key">需要计算的key</param>
        /// <returns>命令对象</returns>
        public virtual IntCommand CreateIncrCommand(string key) => throw new NotImplementedException();

        /// <summary>
        /// 将key所存储的值加上increment。
        /// </summary>
        /// <param name="key">需要计算的key</param>
        /// <param name="increment">需要增加的值</param>
        /// <returns>命令对象</returns>
        public virtual IntCommand CreateIncrByCommand(string key, long increment) => throw new NotImplementedException();
    }
}
