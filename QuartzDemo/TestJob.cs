/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：QuartzDemo
*文件名称：TestJob
*创建人：  shuyizhi
*创建时间：2018/10/8 16:59:06
*文件描述: 
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Quartz;

namespace QuartzDemo
{
    public sealed class TestJob:IJob
    {
        private readonly ILog log = LogManager.GetLogger(typeof(TestJob));
        public Task Execute(IJobExecutionContext context )
        {
            log.InfoFormat("TestJob测试");
            return null;
        }
    }
}
