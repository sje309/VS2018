/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：ThreadDemo.SimpleFactory
*文件名称：BusinessCoat
*创建标识：shuyizhi  2018/10/10 15:13:02
*文件描述: 具体产品类
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadDemo.SimpleFactory
{
    public class BusinessCoat : ICat
    {
        public void GetYourCoat()
        {
            Console.WriteLine("商务上衣");
        }
    }
    public class FashionCoat:ICat
    {
        public void GetYourCoat()
        {
            Console.WriteLine("时尚上衣");
        }
    }
}
