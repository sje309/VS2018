/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：Redis
*文件名称：RedisManager
*创建人：  shuyizhi
*创建时间：2018/4/13 14:07:36
*文件描述: 
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;

namespace Redis
{
    public class RedisManager
    {
        private static RedisConfig RedisConfig;
        private static PooledRedisClientManager prcm;
        static RedisManager()
        {

        }
        //private static void CreateManager()
        //{
        //    string redisUrl = RedisConfig.RedisConnection + ":" + RedisConfig.RedisPort;
        //    prcm = new PooledRedisClientManager(new string[] { redisUrl }, new string[] { redisUrl },new RedisClientManagerConfig {
        //        max
        //    }
        //        );

        //}
    }
}
