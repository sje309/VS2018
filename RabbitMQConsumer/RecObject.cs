/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：RabbitMQConsumer
*文件名称：RecObject
*创建人：  shuyizhi
*创建时间：2018/8/21 16:44:35
*文件描述: 接受RabbitMQ对象消息
************************************************************************/


using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;
using System.Runtime.Serialization.Formatters.Binary;

namespace RabbitMQConsumer
{
    public class RecObject
    {
        private const string QUEUE_NAME = "C Sharp QUEUE";

        public static void RecObjectMsg()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = "localhost";
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(QUEUE_NAME, false, false, false, null);
                    //QueueingBasicConsumer consumer = new QueueingBasicConsumer(channel);
                    EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
                    //consumer.Received += Consumer_Received;
                    consumer.Received += Consumer_Received1;
                    channel.BasicConsume(QUEUE_NAME, true, consumer);                                           
                }
            }
        }

        private static void Consumer_Received1( object sender, BasicDeliverEventArgs e )
        {
            //throw new NotImplementedException();
            var body = e.Body;
            BookModel model = BytesToObject(body) as BookModel;
            Console.WriteLine(model.ToString());
        }

        private static void Consumer_Received( object sender, BasicDeliverEventArgs e )
        {
            //throw new NotImplementedException();
            var body = e.Body;
            BookModel model = BytesToObject(body) as BookModel;
            Console.WriteLine(model.ToString());


        }
        /// <summary>
        /// object对象序列化为byte[]
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] ObjectToBytes( object obj )
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
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
        public static object BytesToObject( byte[] bytes )
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes))
            {
                System.Runtime.Serialization.IFormatter formatter = new BinaryFormatter();
                return formatter.Deserialize(ms);
            }
        }
    }
}
