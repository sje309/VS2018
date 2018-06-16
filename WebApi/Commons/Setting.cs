/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：ESearch
*文件名称：Setting
*创建人：  shuyizhi
*创建时间：2018/5/29 15:08:09
*文件描述: 索引为Bank设置
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Commons
{
    public static class Setting
    {
        public static Uri Node
        {
            get { return new Uri("http://localhost:9200"); }
        }
        public static Nest.ConnectionSettings ConnectionSettings
        {
            get { return new Nest.ConnectionSettings(Node); }
        }
    }
}
