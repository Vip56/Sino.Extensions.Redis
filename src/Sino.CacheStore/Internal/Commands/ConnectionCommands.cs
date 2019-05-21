using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.CacheStore.Internal
{
    public static class ConnectionCommands
    {
        /// <summary>
        /// 进行授权
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns>命令对象</returns>
        public static ResultWithStatus Auth(string password)
        {
            return new ResultWithStatus("AUTH", password);
        }

        /// <summary>
        /// 回显信息
        /// </summary>
        /// <param name="message">需要回显的内容</param>
        /// <returns>命令对象</returns>
        public static ResultWithString Echo(string message)
        {
            return new ResultWithString("ECHO", message);
        }

        /// <summary>
        /// 心跳检测，通常用于测试与服务的链接是否仍然生效，或者用于测量延迟值。
        /// </summary>
        /// <returns>命令对象</returns>
        public static ResultWithStatus Ping()
        {
            return new ResultWithStatus("PING");
        }

        /// <summary>
        /// 请求服务器关闭与当前客户端的连接
        /// </summary>
        /// <returns>命令对象</returns>
        public static ResultWithStatus Quit()
        {
            return new ResultWithStatus("QUIT");
        }

        /// <summary>
        /// 切换到指定的数据库
        /// </summary>
        /// <param name="dbNumber">数据库索引</param>
        /// <returns>命令对象</returns>
        public static ResultWithStatus Select(int dbNumber)
        {
            return new ResultWithStatus("SELECT", dbNumber);
        }
    }
}
