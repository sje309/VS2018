/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：RabbitMQ
*文件名称：Producer
*创建人：  shuyizhi
*创建时间：2018/6/27 16:01:45
*文件描述: RabbitMQ生产者
* 参考：https://www.cnblogs.com/ericli-ericli/p/5917018.html
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Util;

namespace RabbitMQ
{
    public class Producer
    {
        public static void WriteLog()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            using(var connection = factory.CreateConnection())
            {
                using(var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "writeLog", durable: false, exclusive: false, autoDelete: false, arguments: null);
                    for(int i = 0; i < 8000; i++)
                    {
                        string message = i.ToString();
                        var body = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish(exchange: "", routingKey: "writeLog", basicProperties: null, body: body);
                        Console.WriteLine("Program Sent {0}", message);
                    }
                }
            }
        }
    }
}
