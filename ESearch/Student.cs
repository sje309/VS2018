/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：ESearch
*文件名称：Student
*创建人：  shuyizhi
*创建时间：2018/5/30 10:43:35
*文件描述: 
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;


namespace ESearch
{
    [ElasticsearchType(Name="student")]
    public class Student
    {
        [Nest.String(Index =FieldIndexOption.NotAnalyzed)]
        public string Id { get; set; }
        [Nest.String(Analyzer ="standard")]
        public string Name { get; set; }
        [Nest.String(Analyzer ="standard")]
        public string Description { get; set; }
        public DateTime DateTime { get; set; }

        public override string ToString()
        {
            return string.Format("Id:{0},Name:{1},Description:{2},DateTime:{3}", Id, Name, Description, DateTime);
        }
    }
}
