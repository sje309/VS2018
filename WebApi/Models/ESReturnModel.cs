using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class ESReturnModel
    {
        public long Total { get; set; }
        public List<Model> Hits { get; set; }
    }

    public class Model
    {
        public string _index { get; set; }
        public string _type { get; set; }
        public string _id { get; set; }
        public double _score { get; set; }
        public Record _source { get; set; }
    }
}