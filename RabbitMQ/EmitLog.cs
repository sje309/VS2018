/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：RabbitMQ
*文件名称：EmitLog
*创建人：  shuyizhi
*创建时间：2018/8/22 15:34:04
*文件描述: 
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace RabbitMQ
{
    public class EmitLog
    {
        public static void SendMsg()
        {
            var factory = new ConnectionFactory();
            factory.HostName = "localhost";
            using(var connection = factory.CreateConnection())
            {
                using(var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare("logs", "fanout", false, false, null);
                    var message = "info:Hello World!";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish("logs", "", null, body);
                    Console.WriteLine("[X] Send '" + message + "'");
                }
            }
            Console.WriteLine("Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
