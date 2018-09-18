/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：TPLApp
*文件名称：ActionBlockClass
*创建人：  shuyizhi
*创建时间：2018/9/13 16:51:11
*文件描述: ActionBlock按照顺序
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace TPLApp
{
    public class ActionBlockClass
    {
        public static ActionBlock<int> actionBlock = new ActionBlock<int>(i =>
          {
              System.Threading.Thread.Sleep(1000);
              Console.WriteLine(i + "ThreadId: " + System.Threading.Thread.CurrentThread.ManagedThreadId +
                  " Execute Time: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
          });

        public static void TestSync()
        {
            for(int i = 0; i < 10; i++)
            {
                actionBlock.Post<int>(i);
            }
            Console.WriteLine("Post Finished");
        }
    }
}
