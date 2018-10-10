/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：ThreadDemo.Proxy
*文件名称：Proxy
*创建标识：shuyizhi  2018/10/10 11:08:11
*文件描述: 
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadDemo.Proxy
{
    public class Proxy:ISubject
    {
        RealSubject real;
        public void Request()
        {
            if (null == real)
            {
                real = new RealSubject();
            }
            real.Request();
        }
    }
}
