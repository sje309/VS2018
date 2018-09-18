/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：RabbitMQConsumer
*文件名称：BookModel
*创建人：  shuyizhi
*创建时间：2018/8/21 17:03:41
*文件描述: 
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQConsumer
{
    [Serializable]
    public class BookModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public DateTime PublishTime { get; set; }

        public override string ToString()
        {
            //return base.ToString();
            string res = "bookModel[Id: " + Id + ",Name: " + Name + ",Author: " + Author + ",Price: "
                + Price + ",PublishTime: " + PublishTime + "]";
            return res;
        }

    }
}
