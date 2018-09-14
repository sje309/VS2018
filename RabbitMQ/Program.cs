using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace RabbitMQ
{
    class Program
    {
        private static Object obj = new Object();
        static void Main( string[] args )
        {
            //new Thread(Write).Start();
            //new Thread(Write).Start();
            //new Thread(Write).Start();
            //new Thread(Write).Start();

            //new Thread(Producer.WriteLog).Start();
            //new Thread(Producer.WriteLog).Start();
            //new Thread(Producer.WriteLog).Start();
            //new Thread(Producer.WriteLog).Start();

            new Thread(SendObject.SendObjMsg).Start();

            new Thread(() =>
            {
                SendObject.SendObjMsg();
            }).Start();

            Console.ReadLine();
            



        }
        public static void WriteLog(int i )
        {
            lock (obj)
            {
                using (FileStream fileStream = new FileStream(@"D:\\test.txt", FileMode.Append))
                {
                    using (StreamWriter sw = new StreamWriter(fileStream, Encoding.UTF8))
                    {
                        sw.WriteLine(i);
                    }
                }
            }
           
        }
        public static void Write()
        {
            for(int i = 0; i < 10000; i++)
            {
                WriteLog(i);
            }
        }
    }
}
