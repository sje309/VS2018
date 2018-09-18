/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：ThreadDemo
*文件名称：BaseThread
*创建人：  shuyizhi
*创建时间：2018/9/13 15:05:33
*文件描述: 线程基类
* 参考：https://blog.csdn.net/wangzhiyu1980/article/details/45094631
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadDemo
{
    public class BaseThread
    {
        public enum ThreadStatus
        {
            CREATE,
            RUNNING,
            STOPED
        };
        private int m_Tid;
        private bool m_IsAlive;
        private ThreadStatus m_Status;
        private Thread m_WorkingThread;

        private static void WorkFunction(object arg )
        {
            //try
            //{
            //    //System.Diagnostics.Trace.WriteLine("BaseThread::WorkFunction {");
            //    //((BaseThread)arg).Run();
            //    //System.Diagnostics.Trace.WriteLine("BaseThread::WorkFunction }");
            //}
        }
    }
}
