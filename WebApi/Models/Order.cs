﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderName { get; set; }
        public DateTime OrderTime { get; set; }
    }
}