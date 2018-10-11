using System;

namespace Redis
{
    internal class Program
    {
        private static void Main( string[] args )
        {
            #region //string测试

            CSRedis.SetKey();

            #endregion //string测试

            #region //hash测试

            //string key = "hashkey";
            //string filed = "test";
            //CSRedis.GetHashTest(key, filed);

            //HashClass.TestHGETAndHSET();
            //HashClass.TestHMGetAndHMSet();
            //HashClass.TestHExistsAndHDel();

            //HashClass.TestObject();

            //HashClass.TestDelObject();

            //HashClass.TestHSetObject();

            #endregion //hash测试

            Console.WriteLine("Press any key to exit....");
            Console.ReadLine();
        }
    }
}