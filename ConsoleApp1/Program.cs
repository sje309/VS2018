using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApp1
{
    /// <summary>
    /// C# 判断一个方法在指定的时间内是否执行完成
    /// </summary>
    class Program
    {
        static void Main( string[] args )
        {
            bool isTimeOut = true;  //默认超时

            //try
            //{
            //    CallWithTimeOut(method, 6000, ref isTimeOut);
            //}
            //catch(Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}


            try
            {
                CallWithTimeOut(method, 4000, ref isTimeOut);
            }
            catch (TimeoutException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("isTimeOut: " + isTimeOut);
            Console.WriteLine("main........");         
            Console.ReadLine();
        }

        static void method()
        {
            System.Threading.Thread.Sleep(5000);
            Console.WriteLine("方法执行完毕!");
        }
        
        static void CallWithTimeOut( Action action, int timeout, ref bool isTimeOut )
        {
            Thread thread = null;
            Action MyAction = () =>
            {
                thread = Thread.CurrentThread;
                action();
            };
            IAsyncResult result = MyAction.BeginInvoke(null, null);
            if (result.AsyncWaitHandle.WaitOne(timeout))
            {
                MyAction.EndInvoke(result);
                isTimeOut = false;
            }
            else
            {
                //终止当前线程
                thread.Abort();
                isTimeOut = true;
                throw new TimeoutException("调用超时!");
            }
        }
    }
}
