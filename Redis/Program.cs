﻿using System;
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
            CSRedis.SetKey();
            Console.WriteLine("Press any key to exit....");
            Console.ReadLine();
        }
    }
}
