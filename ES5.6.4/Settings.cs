/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：ES5._6._4
*文件名称：Settings
*创建人：  shuyizhi
*创建时间：2018/6/12 13:43:36
*文件描述: ES 相关配置
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ES5._6._4
{
    public class Settings
    {
        public static Uri Node
        {
            //get { return new Uri("http://localhost:9200"); }

            get { return new Uri("http://192.168.56.101:9200"); }        //本地Ubuntu虚拟机
        }
        public static Nest.ConnectionSettings ConnectionSettings
        {
            get { return new Nest.ConnectionSettings(Node); }
        }
    }
}
