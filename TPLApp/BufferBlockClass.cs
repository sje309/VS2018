/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：TPLApp
*文件名称：BufferBlockClass
*创建人：  shuyizhi
*创建时间：2018/9/13 16:13:02
*文件描述: BufferBlock是TDF中最基础的Block
*参考：https://www.cnblogs.com/haoxinyue/archive/2013/03/01/2938959.html
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace TPLApp
{
    public class BufferBlockClass
    {
        public static BufferBlock<int> m_buffer = new BufferBlock<int>();
        
        public static void Producer()
        {
            while (true)
            {
                int item = GetItem();
                m_buffer.Post(item);
                if (item == 6)
                {
                    break;
                }
            }
        }
        public static void Consumer()
        {
            while (true)
            {
                int item = m_buffer.Receive<int>();
                Process(item);
                if (item == 6)
                {
                    break;
                }
            }
        }

        public static int GetItem()
        {
            int[] items = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            return items[new Random().Next(items.Length-1)];
        }

        public static void Process(int item )
        {
            Console.WriteLine("item: " + item);
        }
    }
}
