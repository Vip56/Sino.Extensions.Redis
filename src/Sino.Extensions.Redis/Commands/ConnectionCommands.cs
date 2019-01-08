namespace Sino.Extensions.Redis.Commands
{
    /// <summary>
    /// Redis Connection底层命令
    /// </summary>
    public static class ConnectionCommands
    {
        /// <summary>
        /// 进行授权
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithStatus Auth(string password)
        {
            return new ReturnTypeWithStatus("AUTH", password);
        }

        /// <summary>
        /// 回显信息
        /// </summary>
        /// <param name="message">需要回显的内容</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithString Echo(string message)
        {
            return new ReturnTypeWithString("ECHO", message);
        }

        /// <summary>
        /// 心跳检测，通常用于测试与服务的链接是否仍然生效，或者用于测量延迟值。
        /// </summary>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithStatus Ping()
        {
            return new ReturnTypeWithStatus("PING");
        }

        /// <summary>
        /// 请求服务器关闭与当前客户端的连接
        /// </summary>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithStatus Quit()
        {
            return new ReturnTypeWithStatus("QUIT");
        }

        /// <summary>
        /// 切换到指定的数据库
        /// </summary>
        /// <param name="dbNumber">数据库索引</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithStatus Select(int dbNumber)
        {
            return new ReturnTypeWithStatus("SELECT", dbNumber);
        }
    }
}
