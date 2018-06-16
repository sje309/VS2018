/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：Test3
*文件名称：ESModel
*创建人：  shuyizhi
*创建时间：2018/5/22 14:41:57
*文件描述: 
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test3
{

    [Serializable]
    public class ESModel
    {
        public string ICCID { get; set; }
        public string content { get; set; }
        public string des_ip { get; set; }
        public int des_port { get; set; }
        public string domain_name { get; set; }
        public string domainname { get; set; }
        public string flow_type { get; set; }
        public string header { get; set; }
        public string iccid { get; set; }
        public string identity_id { get; set; }
        public string phone_num { get; set; }
        public string protocol_type { get; set; }
        public string resource_type { get; set; }
        public long size { get; set; }
        public string src_ip { get; set; }
        public int src_port { get; set; }
        
        public DateTime timestamp { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public string username { get; set; }
    }
}
