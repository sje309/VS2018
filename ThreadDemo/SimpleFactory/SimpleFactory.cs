/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：ThreadDemo.SimpleFactory
*文件名称：SimpleFactory
*创建标识：shuyizhi  2018/10/10 15:15:35
*文件描述: 
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadDemo.SimpleFactory
{
    /// <summary>
    /// 简单工厂模式中的核心部分：工厂类
    /// </summary>
    public class SimpleFactory
    {
        public ICat CreateCoat(string styleName )
        {
            switch (styleName.Trim().ToLower())
            {
                case "business":
                    return new BusinessCoat();
                case "fashion":
                    return new FashionCoat();
                default:
                    throw new Exception("还没有你要的那种衣服");
            }
        }
    }
}
