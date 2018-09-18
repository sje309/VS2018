using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TPLApp
{
    class Program
    {
        private static AutoResetEvent autoResetEvent = new AutoResetEvent(false);
        const int cycleNum = 5;
        static void Main( string[] args )
        {
            #region //BufferBlock

            //var p = Task.Factory.StartNew(BufferBlockClass.Producer);
            //var c = Task.Factory.StartNew(BufferBlockClass.Consumer);
            //Task.WaitAll(p, c);

            #endregion
            #region //ActionBlock
            //ActionBlockClass.TestSync();
            #endregion
            #region //测试await async
            //Console.WriteLine("Main线程,线程Id: {0}", Thread.CurrentThread.ManagedThreadId);
            //TestAsync();
            #endregion
            #region //ManualResetEvent
            //MultiThreadSynergicWithManualResetEvent();
            #endregion
            #region //测试WaitHandle
            //TestWaitHandle();
            #endregion
            #region //测试Semaphore
            //TestSemaphore();

            Thread td = new Thread(new ThreadStart(sqrt));
            td.Name = "线程一";
            td.Start();
            autoResetEvent.Set();
            #endregion
            
            //Console.Write("press any key to exit ......");
            Console.ReadKey();
        }

        public static void sqrt()
        {
            autoResetEvent.WaitOne();
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " 限制一执行");
            Thread.Sleep(500);
        }

        /// <summary>
        /// 测试Semaphore,应用场景:如果有多个线程运行，
        /// 每次控制3个线程一起跑
        /// </summary>
        private static void TestSemaphore()
        {
            Semaphore semaphore = new Semaphore(0, 3);
            for(int i = 0; i < 8; i++)
            {
                ThreadPool.QueueUserWorkItem(ar =>
                {
                    semaphore.WaitOne();
                    Console.WriteLine("\t第: " + ((int)ar).ToString() + "个开始运行. ");
                }, i);
            }
            ThreadPool.QueueUserWorkItem(ar =>
            {
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine("第" + (i + 1).ToString() + "批开始执行");
                    semaphore.Release(3);
                    Thread.Sleep(5 * 1000);
                }
            });
        }

        private static void TestWaitHandle()
        {
            WaitHandle[] handles = new WaitHandle[]
            {
                new AutoResetEvent(false),
                new AutoResetEvent(false),
                new AutoResetEvent(false),
                new AutoResetEvent(false),
                new AutoResetEvent(false),
                new AutoResetEvent(false),
                new AutoResetEvent(false),
                new AutoResetEvent(false),
                new AutoResetEvent(false)
            };
            for(var i = 0; i < handles.Length; i++)
            {
                ThreadPool.QueueUserWorkItem(ar =>
                {
                    int index = (int)ar;
                    Thread.Sleep(1000);
                    Console.WriteLine("任务:" + index + "开始执行");
                    (handles[index] as AutoResetEvent).Set();
                }, i);
            }
            ThreadPool.QueueUserWorkItem(ar =>
            {
                WaitHandle.WaitAll(handles);
                Console.WriteLine("所有任务都已执行完成，不在等待");
            });

        }
        private void ThreadMethod()
        {
            while (!autoResetEvent.WaitOne(TimeSpan.FromSeconds(2)))
            {
                Console.WriteLine("Continue");
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            Console.WriteLine("Thread got signal");
        }

        private static void MultiThreadSynergicWithManualResetEvent()
        {
            ManualResetEvent mre = new ManualResetEvent(false);
            Thread thread1 = new Thread(() =>
              {
                  mre.WaitOne();
                  mre.Reset();
                  Console.WriteLine("thread1 work");
                  mre.Set();
                  Thread.Sleep(1000);
              });
            thread1.Start();

            Thread thread2 = new Thread(() =>
              {
                  mre.WaitOne();
                  Console.WriteLine("thread2 work");
                  Thread.Sleep(1000);
              });
            thread2.Start();
            mre.Set();
        }

        static async Task TestAsync()
        {
            Console.WriteLine("调用GetReturnResult()之前,线程Id:{0}。当前时间:{1}",
                Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            var name = GetReturnResult();
            Console.WriteLine("调用GetReturnResult()之后,线程Id:{0}。当前时间:{1}",
                Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            Console.WriteLine("得到GetReturnResult()方法的结果: {0}。当前时间: {1}", await name, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));

        }
        static async Task<string> GetReturnResult()
        {
            Console.WriteLine("执行Task.Run之前，线程Id: {0}", Thread.CurrentThread.ManagedThreadId);
            return await Task.Run(() =>
            {
                Thread.Sleep(3 * 1000);
                Console.WriteLine("GetReturnResult()方法里面的线程Id: {0}", Thread.CurrentThread.ManagedThreadId);
                return "我是返回值";
            });
        }
    }
}
