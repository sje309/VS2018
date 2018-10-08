using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuartzDemo
{
    class Program
    {
        static void Main( string[] args )
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
            //Topshelf.HostFactory.Run(x =>
            //{
            //    x.UseLog4Net();
            //    x.ser
            //});

        }
    }
}
