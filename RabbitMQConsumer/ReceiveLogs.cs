/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：RabbitMQConsumer
*文件名称：ReceiveLogs
*创建人：  shuyizhi
*创建时间：2018/8/22 16:49:34
*文件描述: 
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQConsumer
{
    public class ReceiveLogs
    {
        public static void RecMsg()
        {
            var factory = new ConnectionFactory();
            using (var connection = factory.CreateConnection())
            {
                using(var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare("logs", "fanout", false, false, null);
                    var queueName = channel.QueueDeclare().QueueName;
                    channel.QueueBind(queueName, "logs", "", null);
                    Console.WriteLine("[*] Waiting for logs.");
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += ( model, ea ) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine("[X] {0}", message);
                    };
                    channel.BasicConsume(queueName, true, consumer);
                    Console.WriteLine("Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}
