﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ThreadDemo.Builder;

namespace ThreadDemo
{
    class Program
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(Program));
        static int cnt = 10;
        /// <summary>
        /// 线程信号量，控制线程池中线程与Main线程通讯
        /// </summary>
        private static AutoResetEvent autoResetEvent = new AutoResetEvent(false);

        #region 定义队列
        /// <summary>
        /// 定义队列，存在并发风险
        /// </summary>
        public static  Queue<Product> productQueue=new Queue<Product>();
        /// <summary>
        /// 定义堆栈，存在并发风险
        /// </summary>
        public static Stack<Product> productStack=new Stack<Product>();
        /// <summary>
        /// .Net 4.0后，线程安全的先进先出集合
        /// </summary>
        public static ConcurrentQueue<Product> productConcurrentQueue =
            new ConcurrentQueue<Product>();
        /// <summary>
        /// .Net 4.0后，线程安全的先进后出节后
        /// </summary>
        public static ConcurrentStack<Product> productConcurrentStack=new ConcurrentStack<Product>();

        #endregion

        static void Main(string[] args)
        {
            #region //创建线程

            //System.Threading.Thread thread = new System.Threading.Thread(TestThread.ThreadMethod);
            //thread.Name = "子线程";
            //thread.Start("shuyizhi");

            #endregion

            #region //终止线程

            //Thread thread = new Thread(TestThread.ThreadMethodAbort);
            //thread.Name = "小A";
            //thread.Start();

            #endregion

            #region //CPU过高测试

            //new Thread(A).Start();
            //new Thread(B).Start();

            #endregion

            #region //随机生成ip地址

            //String ip = getRandomIp();
            //Console.WriteLine("ip: " + ip);

            #endregion

            #region //Test Semaphore

            //for(int i = 0; i < 9; i++)
            //{
            //    Thread td = new Thread(new ParameterizedThreadStart(SemaphoreClass.testFun));
            //    td.Name = string.Format("编号{0}", i.ToString());
            //    td.Start(td.Name);
            //}

            #endregion

            #region //Test AutoResetEvent

            //Thread td = new Thread(new ThreadStart(AutoResetEventClass.Sqrt));
            //td.Name = "线程一";
            //td.Start();
            //AutoResetEventClass.autoResetEvent.Set();

            #endregion

            #region //简单线程池

            //ThreadPool.SetMinThreads(1, 1);
            //ThreadPool.SetMaxThreads(5, 5);
            //for(int i = 1; i <= 10; i++)
            //{
            //    //ThreadPool.QueueUserWorkItem(new WaitCallback(testFun), i);
            //    ThreadPool.QueueUserWorkItem(new WaitCallback(testFunByResetEvent), i);
            //}
            //Console.WriteLine("主线程执行!");
            //Console.WriteLine("主线程结束!");
            //autoResetEvent.WaitOne();
            //Console.WriteLine("线程池终止!");

            #endregion

            #region //IOC 控制反转\依赖注入

            /**1、构造函数注入*/
            //DIP.Water water = new DIP.RiverWater();
            //DIP.Fish fish = new DIP.Fish(water);
            //fish.Live();

            /**2、setter方式注入*/
            //DIP.Water water = new DIP.LakeWater();
            //DIP.Fish fish = new DIP.Fish();
            //fish.setWater(water);
            //fish.Live();

            /**3、接口注入*/
            //DIP.Water water = new DIP.WellWater();
            //DIP.Fish fish = new DIP.Fish();
            //fish.setWater(water);
            //fish.Live();

            #endregion

            #region //工厂模式测试

            ///**从配置文件中获取工厂参数名称*/
            //string factoryName = System.Configuration.ConfigurationManager.AppSettings["factoryName"];
            //if (!string.IsNullOrWhiteSpace(factoryName))
            //{
            //    System.Reflection.Assembly assembly = System.Reflection.Assembly.GetAssembly(typeof(FactoryMethod.IFactory));
            //    FactoryMethod.IFactory factory = (FactoryMethod.IFactory)assembly.CreateInstance("ThreadDemo.FactoryMethod." + factoryName);
            //    if (null != factory)
            //    {
            //        FactoryMethod.ICoat coat = factory.CreateCoat();
            //        coat.ShowCoat();
            //    }              
            //}

            #endregion

            #region //建造者模式(Build Pattern)

            //VehicleBuilder builder1 = new ScooterBuilder();
            //VehicleBuilder builder2 = new CarBuilder();
            //VehicleBuilder builder3 = new CarBuilder();

            //Builder.Shop shop=new Shop();

            //shop.Construct(builder1);
            //builder1.Vehicle.show();

            //shop.Construct(builder2);
            //builder2.Vehicle.show();

            //shop.Construct(builder3);
            //builder3.Vehicle.show();

            #endregion

            #region //线程安全队列、堆栈和线程非安全队列、堆栈
            ///**普通入队列操作，存在线程安全*/
            //Task t1 = new TaskFactory().StartNew(EnterQueue);
            //Task t2 = new Task(() => EnterQueue());
            //t2.Start();
            //Task t3 = Task.Factory.StartNew(() => EnterQueue());
            //Task t4 = Task.Run(() => EnterQueue());
            //Task.WaitAll(t1, t2, t3, t4);

            ///**普通入栈操作，存在线程安全*/
            //Task t5 = new TaskFactory().StartNew(EnterStack);
            //Task t6 = new Task(EnterStack);
            //t6.Start();
            //Task t7 = Task.Factory.StartNew(EnterStack);
            //Task t8 = Task.Run(() => EnterStack());
            //Task.WaitAll(t5, t6, t7, t8);

            ///**线程安全入队列*/
            //Task t55 = new Task(EnterConcurrentQueue);
            //Task t66 = new Task(() => { EnterConcurrentQueue(); });
            //t66.Start();
            //Task t77 = Task.Run(() => { EnterConcurrentQueue(); });
            //Task t88 = Task.Factory.StartNew(() => { EnterConcurrentQueue(); });
            //Task.WaitAll(t55, t66, t77, t88);

            ///**线程安全入栈*/
            //Task t11 = Task.Factory.StartNew(EnterConcurrentStack);
            //Task t22 = Task.Factory.StartNew(EnterConcurrentStack);
            //Task t33 = Task.Factory.StartNew(EnterConcurrentStack);
            //Task t44 = Task.Factory.StartNew(EnterConcurrentStack);
            //Task.WaitAll(t11, t22, t33, t44);

            //Console.WriteLine("productQueue队列中共有元素: " + productQueue.Count + "个,实际应该40000个，存在线程安全！");
            //Console.WriteLine("productStack堆栈中共有元素: " + productStack.Count + "个，实际应该40000个，存在线程安全！");

            //Console.WriteLine("productConcurrentQueue中共有元素: " + productConcurrentQueue.Count + "个，实际应该40000个，线程安全！");
            //Console.WriteLine("productConcurrentStack中共有元素: " + productConcurrentStack.Count + "个，实际应该有40000个，线程安全！");
            #endregion

            Console.ReadKey();
        }
        /// <summary>
        /// 入队列
        /// </summary>
        public static void EnterQueue()
        {
            for (int i = 0; i < 10001; i++)
            {
                Product model = new Product
                {
                    Category = "Category" + i,
                    Name = "ProductName" + i,
                    SellPrice = i
                };
                productQueue.Enqueue(model);
            }
        }
        /// <summary>
        /// 入栈
        /// </summary>
        public static void EnterStack()
        {
            for (int i = 0; i < 10001; i++)
            {
                Product model = new Product
                {
                    Category = "Category" + i,
                    Name = "ProductName" + i,
                    SellPrice = i
                };
                productStack.Push(model);
            }
        }
        /// <summary>
        /// 入队列，线程安全集合
        /// </summary>
        public static void EnterConcurrentQueue()
        {
            for(int i = 0; i < 10001; i++)
            {
                Product model = new Product
                {
                    Category = "Category" + i,
                    Name = "ProductName" + i,
                    SellPrice = i
                };
                productConcurrentQueue.Enqueue(model);
            }
        }
        /// <summary>
        /// 入栈，线程安全集合
        /// </summary>
        public static void EnterConcurrentStack()
        {
            for(int i = 0; i < 10001; i++)
            {
                Product model = new Product
                {
                    Category = "Category" + i,
                    Name = "ProductName" + i,
                    SellPrice = i
                };

                productConcurrentStack.Push(model);
            }
        }

        public static void testFun(object obj )
        {
            Console.WriteLine(string.Format("{0}:第{1}个线程池", DateTime.Now.ToString(), obj.ToString()));
            //log.Info(string.Format("{0}:第{1}个线程池", DateTime.Now.ToString(), obj.ToString()));
            Thread.Sleep(5 * 1000);
        }
        public static void testFunByResetEvent(object obj )
        {
            cnt -= 1;
            Console.WriteLine(string.Format("{0}:第{1}个线程池", DateTime.Now.ToString(), obj.ToString()));
            Thread.Sleep(5 * 1000);
            if (cnt == 0)
            {
                autoResetEvent.Set();
            }
        }
        private static void A(object state )
        {
            while (true)
            {
                Thread.Sleep(1 * 1000);
            }
        }
        private static void B(object state )
        {
            while (true)
            {
                double d = new Random().NextDouble() * new Random().NextDouble();
            }
        }

        public static String getRandomIp()
        {
            //ip范围

            int[,] range =
            {
                {607649792,608174079},//[图片]36.56.0.0-[图片]36.63.255.255
                {1038614528,1039007743},//[图片]61.232.0.0-[图片]61.237.255.255
                {1783627776,1784676351},//[图片]106.80.0.0-[图片]106.95.255.255
                {2035023872,2035154943},//[图片]121.76.0.0-[图片]121.77.255.255
                {2078801920,2079064063},//[图片]123.232.0.0-[图片]123.235.255.255
                {-1950089216,-1948778497},//[图片]139.196.0.0-[图片]139.215.255.255
                {-1425539072,-1425014785},//[图片]171.8.0.0-[图片]171.15.255.255
                {-1236271104,-1235419137},//[图片]182.80.0.0-[图片]182.92.255.255
                {-770113536,-768606209},//[图片]210.25.0.0-[图片]210.47.255.255
                {-569376768,-564133889} //[图片]222.16.0.0-[图片]222.95.255.255 };
            };

            Random rdint = new Random();
            int index = rdint.Next(10);
            //String ip = num2ip(range[index][0] + new Random().Next(range[index][1] - range[index][0]));
            String ip = num2ip(range[index, 0] + new Random().Next(range[index, 1] - range[index, 0]));
            return ip;
        }

        public static String num2ip( int ip )
        {
            int[] b = new int[4];
            String x = "";

            b[0] = (int)((ip >> 24) & 0xff);
            b[1] = (int)((ip >> 16) & 0xff);
            b[2] = (int)((ip >> 8) & 0xff);
            b[3] = (int)(ip & 0xff);
            x = Convert.ToString(b[0]) + "." + Convert.ToString(b[1]) + "." + Convert.ToString(b[2]) + "." + Convert.ToString(b[3]);

            return x;
        }

    }
}
