/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：ThreadDemo.FactoryMethod
*文件名称：IFactory
*创建标识：shuyizhi  2018/10/10 16:28:01
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
    /// 抽象工厂类，定义产品的接口
    /// </summary>
    public interface IFactory
    {
        ICoat CreateCoat();
    }
}
