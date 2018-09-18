/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：ThreadDemo
*文件名称：TestThread
*创建人：  shuyizhi
*创建时间：2018/8/29 14:03:50
*文件描述: 
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadDemo
{
    public class TestThread
    {
        public static void ThreadMethod(object parameter )
        {
            Console.WriteLine("{0} 开始执行", System.Threading.Thread.CurrentThread.Name);
        }
        public static void ThreadMethodAbort(object parameter )
        {
            Console.WriteLine("我是:{0},我要终止了", Thread.CurrentThread.Name);
            /** 开始终止线程 */
            Thread.CurrentThread.Abort();
            for(int i = 0; i < 10; i++)
            {
                Console.WriteLine("我是:{0},我循环了{1}次", Thread.CurrentThread.Name, i);
            }
        }
    }
}
