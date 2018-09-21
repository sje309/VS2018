using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thrift;
using Thrift.Protocol;
using Thrift.Transport;
using Thrift.Server;
using HelloThrift.Interface;

namespace Thrift.Server
{
    class Program
    {
        private const int SERVERPORT = 8090;
        static void Main( string[] args )
        {
            try
            {
                //设置服务端口为8080
                TServerSocket serverTransport = new TServerSocket(SERVERPORT);
                //设置传输协议工厂
                TBinaryProtocol.Factory factory = new TBinaryProtocol.Factory();
                //关联处理器与服务的实现
                TProcessor processor = new HelloService.Processor(new MyService());
                //创建服务端对象
                TServer server = new TThreadPoolServer(processor, serverTransport, new TTransportFactory(), factory);
                //TServer server = new TSimpleServer(processor, serverTransport);
                Console.WriteLine("服务端正在监听" + SERVERPORT + "端口");
                server.Serve();
            }
            catch (TTransportException ex)//捕获异常信息
            {
                //打印异常信息
                Console.WriteLine(ex.Message);
            }
        }
    }
}
