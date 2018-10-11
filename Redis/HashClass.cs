/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：Redis
*文件名称：HashClass
*创建人：  shuyizhi
*创建时间：2018/9/12 14:57:18
*文件描述: 使用CSRedis框架测试 Redis Hash测试
************************************************************************/

using CSRedis;
using System;
using System.Collections.Generic;

namespace Redis
{
    public class HashClass
    {
        /// <summary>
        /// HSet和HGet 命令测试
        /// </summary>
        public static void TestHGETAndHSET()
        {
            using (RedisClient client = new RedisClient(RedisConfig.RedisConnection, RedisConfig.RedisPort))
            {
                bool res = client.HSet("site", "redis", "redis.com");
                if (res)
                {
                    Console.WriteLine("hset成功,\thget(\"site\",\"redis\"): " + client.HGet("site", "redis"));
                }
                else
                {
                    Console.WriteLine("hset失败");
                }
            }
        }

        /// <summary>
        /// HMSet和HMGet测试
        /// </summary>
        public static void TestHMGetAndHMSet()
        {
            using (RedisClient client = new RedisClient(RedisConfig.RedisConnection, RedisConfig.RedisPort))
            {
                string res = client.HMSet("runoobkey", "name", "redis tutorial", "description", "redsi baisc commands for caching", "likes", "20", "visitors", "23000");
                if (!string.IsNullOrEmpty(res) && res.ToLower() == "ok")
                {
                    Console.WriteLine("hmset成功,hmget结果如下：");

                    //HMGet同步获取
                    //string[] hmGets = client.HMGet("runoobkey", "name", "description", "likes", "visitors");
                    //foreach(string get in hmGets)
                    //{
                    //    Console.WriteLine(get);
                    //}

                    //HMGetAsync异步获取
                    //Task<string[]> task = client.HMGetAsync("runoobkey", "name", "description", "likes", "visitors");
                    //task.Wait();
                    //string[] results = task.Result;
                    //Console.WriteLine("HMGetAsync获取");
                    //if (null != results && results.Length > 0)
                    //{
                    //    foreach(string result in results)
                    //    {
                    //        Console.WriteLine(result);
                    //    }
                    //}

                    //HVals()获取
                    string[] results = client.HVals("runoobkey");
                    if (null != results && results.Length > 0)
                    {
                        foreach (string result in results)
                        {
                            Console.WriteLine(result);
                        }
                    }

                    Console.WriteLine("runoobkey hash中总共有: " + client.HLen("runoobkey") + "个fields");
                }
            }
        }

        /// <summary>
        /// 判断给定的key、field是否存在，存在则删除
        /// </summary>
        public static void TestHExistsAndHDel()
        {
            using (RedisClient client = new RedisClient(RedisConfig.RedisConnection, RedisConfig.RedisPort))
            {
                bool exists = client.HExists("runoobkey", "name");
                if (exists)
                {
                    string result = "key:runoobkey,field:name 存在";
                    long res = client.HDel("runoobkey", "name");
                    if (0 != res)
                    {
                        result += ",已经删除!";
                    }
                    else
                    {
                        Console.WriteLine("删除失败!");
                    }
                }
                else
                {
                    Console.WriteLine("key:runoobkey,filed:name 不存在");
                }
            }
        }

        public static void TestObject()
        {
            using (RedisClient client = new RedisClient(RedisConfig.RedisConnection, RedisConfig.RedisPort))
            {
                DateTime start = DateTime.Now;
                List<User> users = User.GetUsers();
                Console.WriteLine("生成10W条数据的时间: " + (DateTime.Now.Subtract(start).Seconds + " s"));
                //用hash存储
                start = DateTime.Now;
                bool res = false;
                foreach (User u in users)
                {
                    res = client.HSet("myObject", u.Id, u);
                }

                if (res)
                {
                    Console.WriteLine("redis中存入10W条数据成功,耗时: " + (DateTime.Now.Subtract(start).Seconds + " s"));
                }
                else
                {
                    Console.WriteLine("redis存入失败");
                }
            }
        }

        public static void TestDelObject()
        {
            using (RedisClient client = new RedisClient(RedisConfig.RedisConnection, RedisConfig.RedisPort))
            {
                List<User> users = User.GetUsers();
                int count = 0;
                DateTime start = DateTime.Now;
                foreach (var user in users)
                {
                    long res = client.HDel("myObject", user.Id);
                    if (res != 0)
                    {
                        count++;
                    }
                }
                Console.WriteLine("删除消耗的时间: " + (DateTime.Now.Subtract(start).TotalSeconds + " s,总共删除: " + count + " 条数据"));
            }
        }

        public static void TestHSetObject()
        {
            using (RedisClient client = new RedisClient(RedisConfig.RedisConnection, RedisConfig.RedisPort))
            {
                User user = new User();
                user.Address = "安徽省合肥市高新区望江西路与创新大道交叉口中国科学技术大学先进技术院";
                user.Birth = DateTime.Now;
                user.Id = Guid.NewGuid().ToString();
                user.Name = "shuyizhi";

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(user);
                bool resSet = client.HSet("object", "shuyizhi", json);
                if (resSet)
                {
                    Console.WriteLine("插入成功!");
                }
                else
                {
                    Console.WriteLine("插入失败!");
                }

                string resGet = client.HGet("object", "shuyizhi");
                Console.WriteLine("resGet: " + resGet);
                Console.WriteLine("反序列化后: " + Newtonsoft.Json.JsonConvert.DeserializeObject<User>(resGet).ToString());
            }
        }
    }
}