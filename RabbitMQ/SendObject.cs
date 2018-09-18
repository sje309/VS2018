/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：RabbitMQ
*文件名称：SendObject
*创建人：  shuyizhi
*创建时间：2018/8/21 16:05:37
*文件描述: 
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Util;

namespace RabbitMQ
{
    public class SendObject
    {
        private const string QUEUE_NAME = "C Sharp QUEUE";
        public static void SendObjMsg()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = "localhost";
            using(var connection = factory.CreateConnection())
            {
                using(var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(QUEUE_NAME, false, false, false, null);
                    BookModel model = new BookModel();
                    model.Author = "束义志";
                    model.Id = 100;
                    model.Name = "RabbitMQ in Action";
                    model.Price = 265.30m;
                    model.PublishTime = DateTime.Now;
                    channel.BasicPublish("", QUEUE_NAME, null, ObjectToBytes(model));
                    Console.WriteLine("发送消息为: " + model.ToString());
                }
            }
        }
        /// <summary>
        /// object对象序列化为byte[]
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] ObjectToBytes(object obj )
        {
            using(System.IO.MemoryStream ms=new System.IO.MemoryStream())
            {
                System.Runtime.Serialization.IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                return ms.GetBuffer();
            }
        }
        /// <summary>
        /// byte[]反序列化为指定的类型T
        /// </summary>
        /// <typeparam name="T">指定的类型</typeparam>
        /// <param name="bytes">byte数组</param>
        /// <returns></returns>
        public static object BytesToObject<T>(byte[] bytes )
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes))
            {
                System.Runtime.Serialization.IFormatter formatter = new BinaryFormatter();
                return (T)formatter.Deserialize(ms);
            }
        }
    }
}
