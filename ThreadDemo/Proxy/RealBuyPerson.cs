/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：ThreadDemo.Proxy
*文件名称：RealBuyPerson
*创建标识：shuyizhi  2018/10/10 11:11:50
*文件描述: 真实主题角色
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadDemo.Proxy
{
    public class RealBuyPerson : Person
    {
        public override void BuyProduct()
        {
            Console.WriteLine("帮我购买一个Iphone和一台苹果电脑");
        }
    }
}
