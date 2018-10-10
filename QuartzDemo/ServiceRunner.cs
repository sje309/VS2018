/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：QuartzDemo
*文件名称：ServiceRunner
*创建人：  shuyizhi
*创建时间：2018/10/8 17:04:29
*文件描述: 
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using Topshelf;

namespace QuartzDemo
{
    public sealed class ServiceRunner:ServiceControl,ServiceSuspend
    {
        private readonly IScheduler scheduler;

        public ServiceRunner()
        {
            scheduler = (IScheduler)StdSchedulerFactory.GetDefaultScheduler();
        }

        public bool Continue( HostControl hostControl )
        {
            //throw new NotImplementedException();
            scheduler.ResumeAll();
            return true;
        }

        public bool Pause( HostControl hostControl )
        {
            //throw new NotImplementedException();
            scheduler.PauseAll();
            return true;
        }

        public bool Start( HostControl hostControl )
        {
            //throw new NotImplementedException();
            scheduler.Start();
            return true;
        }

        public bool Stop( HostControl hostControl )
        {
            //throw new NotImplementedException();
            scheduler.Shutdown(false);
            return true;
        }
    }
}
