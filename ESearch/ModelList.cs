/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：ESearch
*文件名称：ModelList
*创建人：  shuyizhi
*创建时间：2018/5/25 10:56:49
*文件描述: 用于ES检索后接受的Model
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESearch
{
    public class ModelList
    {
        public long hits { get; set; }
        public int took { get; set; }
        public List<record> list { get; set; }
        public ModelList()
        {
            this.list = new List<record>();
        }
    }
}
