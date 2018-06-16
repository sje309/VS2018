using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    [Serializable]
    public class Department
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Level { get; set; }
        public string Description { get; set; }
    }
}