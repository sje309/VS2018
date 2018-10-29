/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：ThreadDemo
*文件名称：AsyncInvoker
*创建标识：shuyizhi  2018/10/15 14:02:58
*文件描述: 简洁的异步通知机制(委托)
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadDemo
{
    public class AsyncInvoker
    {
        /// <summary>
        /// 记录异步执行的结果
        /// </summary>
        private IList<string> output=new List<string>();

        public AsyncInvoker()
        {
           
        }
        
    }
}
