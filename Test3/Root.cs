/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：Test3
*文件名称：QueryParam
*创建人：  shuyizhi
*创建时间：2018/5/23 10:43:47
*文件描述: 
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test3
{
    public class Term
    {
        /// <summary>
        /// 合肥
        /// </summary>
        public string title { get; set; }
        public string protocol_type { get; set; }
    }

    public class MustItem
    {
        /// <summary>
        /// 
        /// </summary>
        public Term term { get; set; }
        public Bool _bool;
        public Range range;
    }
    public class Should
    {
        public Term term { get; set; }
    }
    public class Bool
    {
        /// <summary>
        /// 
        /// </summary>
        public List<MustItem> must { get; set; }
        public List<Should> should { get; set; }
    }

    public class Range
    {
        public Timestamp timestamp;
    }

    public class Query
    {
        /// <summary>
        /// 
        /// </summary>
        public Bool _bool { get; set; }
    }

    public class Timestamp
    {
        /// <summary>
        /// 
        /// </summary>
        public string order { get; set; }
        public string gte;
        public string lte;
    }

    public class Sort
    {
        /// <summary>
        /// 
        /// </summary>
        public Timestamp timestamp { get; set; }
    }

    [Serializable]
    public class Root
    {
        /// <summary>
        /// 
        /// </summary>
        public int size { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int from { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Query query { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Sort sort { get; set; }
    }

}
