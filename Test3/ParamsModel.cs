/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：Test3
*文件名称：ParamsModel
*创建人：  shuyizhi
*创建时间：2018/5/23 16:16:48
*文件描述:
************************************************************************/

using System;

namespace Test3
{
    [Serializable]
    public class ParamsModel
    {
        public string key { get; set; }
        public string timestampstart { get; set; }
        public string timestampend { get; set; }

        public string[] phone_num { get; set; }

        public string[] protocol_type { get; set; }

        public string[] resource_type { get; set; }

        public string flow_type { get; set; }

        public long size { get; set; }
        public long from { get; set; }
    }
}