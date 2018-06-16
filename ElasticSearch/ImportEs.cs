/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：ElasticSearch
*文件名称：ImportEs
*创建人：  shuyizhi
*创建时间：2018/5/22 11:10:00
*文件描述: 
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearch
{
    public class ImportEs
    {
        public static string ElasticsearchMethod()
        {
            var node = new Uri("http://localhost:9200");
            var indexName = "msg";
        }
    }
}
