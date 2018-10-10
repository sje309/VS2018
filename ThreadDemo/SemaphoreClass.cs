/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：ThreadDemo
*文件名称：Semaphore
*创建人：  shuyizhi
*创建时间：2018/10/9 9:21:29
*文件描述: 
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadDemo
{
    public class SemaphoreClass
    {
        static System.Threading.Semaphore semaphore = new System.Threading.Semaphore(5, 5);
        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(SemaphoreClass));
        public static void testFun(object obj)
        {
            semaphore.WaitOne();
            Console.WriteLine(obj.ToString() + "进洗手间: " + DateTime.Now.ToString());
            log.Info(obj.ToString() + "进洗手间: " + DateTime.Now.ToString());
            System.Threading.Thread.Sleep(2 * 1000);
            Console.WriteLine(obj.ToString() + "出洗手间: " + DateTime.Now.ToString());
            log.Info(obj.ToString() + "出洗手间: " + DateTime.Now.ToString());
            semaphore.Release();
        }
    }
}
