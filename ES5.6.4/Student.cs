/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：ES5._6._4
*文件名称：Student
*创建人：  shuyizhi
*创建时间：2018/6/12 13:46:33
*文件描述: 
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Nest.JsonNetSerializer;
namespace ES5._6._4
{
    [ElasticsearchType(Name = "student")]
    public class Student
    {
        public string Id { get; set; }
       
        public string Name { get; set; }
       
        public string Description { get; set; }
        public DateTime DateTime { get; set; }

        public override string ToString()
        {
            return string.Format("Id:{0},Name:{1},Description:{2},DateTime:{3}", Id, Name, Description, DateTime);
        }
    }
}
