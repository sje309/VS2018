/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：ThreadDemo.Proxy
*文件名称：Friend
*创建标识：shuyizhi  2018/10/10 11:13:07
*文件描述: 代理角色
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadDemo.Proxy
{
    public class Friend : Person
    {
        /**引用真实主题实例*/
        RealBuyPerson realBuy;
        public override void BuyProduct()
        {
            Console.WriteLine("通过代理类访问真实实体对象的方法");
            if (null == realBuy)
            {
                realBuy = new RealBuyPerson();
            }
        }
        /// <summary>
        /// 代理角色执行的一些操作
        /// </summary>
        public void PreBuyProduct()
        {
            Console.WriteLine("我怕弄糊涂了，需要列一张清单，张三：要带相机，李四：要带Iphone...........");
        }

    }
}
