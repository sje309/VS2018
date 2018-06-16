using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class ReturnModel
    {
        /// <summary>
        /// 总数
        /// </summary>
        public long Total { get; set; }
        /// <summary>
        /// 查询消耗的时间
        /// </summary>
        public long Took { get; set; }
        /// <summary>
        /// 查询的数据集合
        /// </summary>
        public IReadOnlyCollection<Record> datas { get; set; }
    }
}