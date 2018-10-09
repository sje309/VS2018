using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis
{
    class Program
    {
        static void Main(string[] args)
        {
            #region //string测试
            CSRedis.SetKey();
            #endregion

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


            #endregion


            Console.WriteLine("Press any key to exit....");
            Console.ReadLine();
        }
    }
}
