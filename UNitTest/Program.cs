using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNitTest
{
    class Program
    {
        static void Main( string[] args )
        {
        }
        public bool IsValid(int opt )
        {
            if (opt > 100)
            {
                return true;
            }
            return false;
        }
        public int AddData(int a,int b )
        {
            return a + b;
        }
    }
}
