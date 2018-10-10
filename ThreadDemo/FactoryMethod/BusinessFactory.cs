/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：ThreadDemo.FactoryMethod
*文件名称：BusinessFactory
*创建标识：shuyizhi  2018/10/10 16:51:33
*文件描述: 
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadDemo.FactoryMethod
{
    /// <summary>
    /// 具体工厂类
    /// </summary>
    public class BusinessFactory : IFactory
    {
        public ICoat CreateCoat()
        {
            return new BusinessCoat();
        }
    }
}
