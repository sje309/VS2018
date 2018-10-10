/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：ThreadDemo
*文件名称：AutoResetEventClass
*创建人：  shuyizhi
*创建时间：2018/10/9 9:48:31
*文件描述: 
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadDemo
{
    public class AutoResetEventClass
    {
        public static System.Threading.AutoResetEvent autoResetEvent = new System.Threading.AutoResetEvent(false);
        /// <summary>
        /// 记录日志
        /// </summary>
        private static log4net.ILog log = log4net.LogManager.GetLogger(type: typeof(AutoResetEventClass));
        public static void Sqrt()
        {
            autoResetEvent.WaitOne();
            Console.WriteLine(DateTime.Now.ToShortTimeString() + "线程一执行");
            log.Info(DateTime.Now.ToShortTimeString() + "线程一执行");
            System.Threading.Thread.Sleep(500);
        }
    }
}
