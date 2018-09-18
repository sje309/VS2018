using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thrift;
using Thrift.Protocol;
using Thrift.Transport;
using Thrift.Server;

namespace Thrift.Server
{
    class Program
    {
        private const int SERVERPORT = 8080;
        static void Main( string[] args )
        {
            /**设置服务端口*/
            TServerSocket serverTransport = new TServerSocket(SERVERPORT);
            /**设置传输协议工厂*/
            TBinaryProtocol.Factory factory = new TBinaryProtocol.Factory();
            /**关联处理器与服务的实现*/
            TProcessor processor = new HelloThrift.Interface.HelloService.Processor(new MyService());
            /**创建服务端对象*/
            TServer server = new TThreadPoolServer(processor, serverTransport);
        }
    }
}
