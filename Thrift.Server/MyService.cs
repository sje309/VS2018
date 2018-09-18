/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：Thrift.Server
*文件名称：MyService
*创建人：  shuyizhi
*创建时间：2018/9/18 17:14:09
*文件描述: 
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelloThrift.Interface;


namespace Thrift.Server
{
    public class MyService : HelloService.Iface
    {
        public bool HelloBoolean( bool para )
        {
            //throw new NotImplementedException();
            Console.WriteLine("客户端调用了HelloBoolean方法");
            return para;
        }

        public int HelloInt( int para )
        {
            //throw new NotImplementedException();
            Console.WriteLine("客户端调用了HelloInt方法");
            return para;
        }

        public string HelloNull()
        {
            //throw new NotImplementedException();
            Console.WriteLine("客户端调用了HelloNull方法");
            return null;
        }

        public string HelloString( string para )
        {
            //throw new NotImplementedException();
            Console.WriteLine("客户端调用了HelloString方法");
            return para;
        }

        public void HelloVoid()
        {
            //throw new NotImplementedException();
            Console.WriteLine("客户端调用了HelloVoid方法");
        }
    }
}
