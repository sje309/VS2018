using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadDemo
{
    class Program
    {
        static void Main( string[] args )
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

            String ip = getRandomIp();
            Console.WriteLine("ip: " + ip);

            #endregion



            Console.ReadKey();
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
