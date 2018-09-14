/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：Redis
*文件名称：CSRedis
*创建人：  shuyizhi
*创建时间：2018/4/13 14:26:22
*文件描述: 使用csredis操作Redis
*参考：https://blog.csdn.net/sparkexpert/article/details/51166626
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSRedis;

namespace Redis
{
    public class CSRedis
    {
        public static void SetKey()
        {
            using (var redis = new RedisClient(RedisConfig.RedisConnection, RedisConfig.RedisPort))
            {
                string ping = redis.Ping();
                string echo = redis.Echo("hello world");
                //DateTime time = GetTime(redis.Time().ToString());
                string time = string.Format("yyyy-MM-dd HH:mm:ss", redis.Time().ToString());
                Console.WriteLine(ping);
                Console.WriteLine(echo);
                Console.WriteLine(time);

                redis.Set("C#Key", "value1");
                redis.Set("C#Key2", "value2");

                Console.WriteLine(redis.Get("C#Key"));
                Console.WriteLine(redis.Get("C#Key2"));


                //for(int i = 0; i < 50000; i++)
                //{
                //    redis.IncrAsync("test1");
                //}
                //redis.TimeAsync().ContinueWith(t => Console.WriteLine(t.Result));
                //string result = redis.GetAsync("test1").Result;
                //Console.WriteLine(result);
            }
        }

        public static void AddHashTest()
        {
            using (var redis = new RedisClient(RedisConfig.RedisConnection, RedisConfig.RedisPort))
            {
                string key = "hashkey";
                string filed = "test";
                string value = "testvalue";

                bool res= redis.HSet(key, filed, value);
                if (res)
                {
                    Console.WriteLine("插入hashtable成功,插入的值为: " + redis.HGet(key, filed));
                }
                else
                {
                    Console.WriteLine("插入hashtable失败");
                }

            }
        }

        public static void GetHashTest(string key,string filed)
        {
            using(var redis=new RedisClient(RedisConfig.RedisConnection, RedisConfig.RedisPort))
            {
                string res = redis.HGet(key, filed);
                if (!string.IsNullOrEmpty(res) && !res.Equals("nil"))
                {
                    Console.WriteLine("获取RedisHash 成功: " + res);
                }
                else
                {
                    Console.WriteLine("获取RedisHash 失败: " + res);
                }
            }
        }

        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name=”timeStamp”></param>
        /// <returns></returns>
        private static DateTime GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime); return dtStart.Add(toNow);
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name=”time”></param>
        /// <returns></returns>
        private static int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
    }
}
