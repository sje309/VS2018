/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：ESearch
*文件名称：ESReturnModel
*创建人：  shuyizhi
*创建时间：2018/6/4 9:55:13
*文件描述: ES 返回数据模型

* {
        "_index": "tekuan",
        "_type": "record",
        "_id": "AWO0-wEspqYNltpyqQNZ",
        "_score": 5.570099,
        "_source": {
          "iCCID": "1625256787646692",
          "content": "德國什文福野生動物園山貓魯費斯2013年結紮，因不必再覓偶變得快樂無比，發福至48公斤，比同類重13公斤。《太陽報》詳全文：【怪奇PoP】山貓結紮後 快樂肥13公斤",
          "des_ip": "175.232.125.44",
          "des_port": 2348,
          "domain_name": "www.appledaily.com.tw",
          "flow_type": "下行",
          "header": "HTTP/1.1",
          "identity_id": "510902198211016183",
          "phone_num": "13761069826",
          "protocol_type": "HTTP",
          "resource_type": "HTML",
          "size": 82,
          "src_ip": "207.138.252.138",
          "src_port": 3528,
          "timestamp": "2018-05-25T07:00:00",
          "title": "【怪奇PoP】山貓結紮後 快樂肥13公斤",
          "url": "http://www.appledaily.com.tw/appledaily/article/international/20180525/38023974//",
          "username": "谭小英"
        }
      }
 
* 
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESearch
{
    public class ESReturnModel
    {
        public long Total { get; set; }
        public List<ReturnModel> Hits { get; set; }
    }

    public class ReturnModel
    {
        public string _index { get; set; }
        public string _type { get; set; }
        public string _id { get; set; }
        public double _score { get; set; }
        public record _source { get; set; }
    }
}
