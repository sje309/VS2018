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
            #region //判断一个方法在指定的时间内是否执行完成
            //bool isTimeOut = true;  //默认超时

            ////try
            ////{
            ////    CallWithTimeOut(method, 6000, ref isTimeOut);
            ////}
            ////catch(Exception ex)
            ////{
            ////    Console.WriteLine(ex.Message);
            ////}


            //try
            //{
            //    CallWithTimeOut(method, 4000, ref isTimeOut);
            //}
            //catch (TimeoutException ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            //Console.WriteLine("isTimeOut: " + isTimeOut);
            //Console.WriteLine("main........");
            //Console.ReadLine();
            #endregion

            #region //启动一个进程

            System.Diagnostics.Process process = System.Diagnostics.Process.Start(
                @"E:\shuyizhi\舆情爬虫\新浪微博重点关注\zdgz_solar_more_process\zdgz_solar\DataProcess\bin\Debug\DataProcess.exe");
            process.Start();

            #endregion

        }

        static void method()
        {
            System.Threading.Thread.Sleep(5000);
            Console.WriteLine("方法执行完毕!");
        }
        /// <summary>
        /// 判断一个方法是否在指定的时间内完成，指定的时间后不影响主线程执行
        /// </summary>
        /// <param name="action"></param>
        /// <param name="timeout"></param>
        /// <param name="isTimeOut"></param>
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
