using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.Extensions.Redis
{
    public static class DateTimeExt
    {
        /// <summary>
        /// 获取Unix时间戳
        /// </summary>
        /// <returns></returns>
        public static int GetUnixTime(this DateTime dt)
        {
            var ts = dt.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt32(ts.TotalSeconds * 1000);
        }
    }
}
