/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：RabbitMQ
*文件名称：Consume
*创建人：  shuyizhi
*创建时间：2018/6/27 16:10:40
*文件描述: RabbitMQ消费者
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Util;

namespace RabbitMQConsumer
{
    public class Consume
    {
        public static void ReceivedLog()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using(var connection = factory.CreateConnection())
            {
                using(var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("writeLog", false, false, false, null);
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += Consumer_Received;
                    channel.BasicConsume("writeLog", true, consumer);
                    Console.WriteLine("Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }

        private static void Consumer_Received( object sender, BasicDeliverEventArgs e )
        {
            //throw new NotImplementedException();
            var body = e.Body;
            var message = Encoding.UTF8.GetString(body);
            ExcuateWriteFile(message);
            Console.WriteLine("Receiver Received {0}", message);
        }
        public static void ExcuateWriteFile(string i )
        {
            using(System.IO.FileStream fs=new System.IO.FileStream(@"D:\\test.txt", System.IO.FileMode.Append))
            {
                using(System.IO.StreamWriter sw=new System.IO.StreamWriter(fs, Encoding.UTF8))
                {
                    sw.WriteLine(i);
                }
            }
        }
    }
}
