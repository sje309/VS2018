/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：Redis
*文件名称：RedisConfig
*创建人：  shuyizhi
*创建时间：2018/4/13 13:40:37
*文件描述: 
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using ServiceStack.Redis;

namespace Redis
{
    public sealed class RedisConfig : ConfigurationSection
    {
        public static string RedisConnection
        {
            get { return Config.GetConfigValue("redisServer"); }
        }
        public static int RedisPort
        {
            get { return Config.GetConfigInt("redisPort"); }
        }

        public static int MaxActive
        {
            get { return Config.GetConfigInt("maxActive"); }
        }
        public static int MaxIdle
        {
            get { return Config.GetConfigInt("maxIdle"); }
        }
        public static int MaxWait
        {
            get { return Config.GetConfigInt("maxWait"); }
        }
      
    }
}
