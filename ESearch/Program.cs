using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESearch
{
    class Program
    {
        private static object _obj = new object();
        static void Main( string[] args )
        {
            #region //宽度ES接口测试

            //var protocol_types = new string[] { "HTTP", "HTTPS", "FTP", "SMTP", "POP3", "P2P" };
            //var flow_type = "";
            //var timestart = "2018-02-25 09:51:52";
            //var timeend = "2018-05-25 09:51:52";
            //var resource_types = new string[] { "HTML", "CSS", "图片", "语音", "视频", "FLASH" };
            //var key = "";
            //var phone_num = "";

            //search(key, phone_num, protocol_types, flow_type, timestart, timeend, resource_types);
            #endregion

            #region //Bank测试

            //TestBank.MatchAll();

            TestBank.Match();

            //TestBank.CreateIndex();

            //TestBank.InsertData();

            //TestBank.QueryData();

            //TestBank.DeleteDataById();

            //TestBank.QueryDataByCondition();

            //TestBank.UpdateDocumentById();
            //TestBank.UpdatePartById();

            //TestBank.QueryTekuanByCondition();

            //TestBank.AddBankDocument();

            #endregion

            Console.WriteLine("\n=================================================================================");
            Console.Write("press any key to exit...");
            Console.Read();


        }


        /// <summary>
        /// 宽度综合搜索
        /// </summary>
        /// <param name="key"></param>
        /// <param name="phone_num"></param>
        /// <param name="protocol_types"></param>
        /// <param name="flow_type"></param>
        /// <param name="timestart"></param>
        /// <param name="timeend"></param>
        /// <param name="resource_types"></param>
        /// DSL Query 语句
        /// 
        /// 
        /// <returns></returns>
        /// 
        /**
            {
  "size": 10,
  "from": 0,
  "query": {
    "bool": {
      "must": [
        {
          "bool": {
            "should": [
              {
                "match": {
                  "content": "美国"
                }
              },
              {
                "match": {
                  "title": "美国"
                }
              }
            ]
          }
        },
        {
          "bool": {
            "should": [
              {
                "term": {
                  "phone_num": "13162976153"
                }
              }
            ]
          }
        },
        {
          "bool": {
            "should": [
              {
                "term": {
                  "protocol_type": "HTTP"
                }
              },
              {
                "term": {
                  "protocol_type": "HTTPS"
                }
              },
              {
                "term": {
                  "protocol_type": "FTP"
                }
              },
              {
                "term": {
                  "protocol_type": "SMTP"
                }
              },
              {
                "term": {
                  "protocol_type": "POP3"
                }
              },
              {
                "term": {
                  "protocol_type": "P2P"
                }
              }
            ]
          }
        },
        {
          "bool": {
            "should": [
              {
                "term": {
                  "resource_type": "HTML"
                }
              },
              {
                "term": {
                  "resource_type": "CSS"
                }
              },
              {
                "term": {
                  "resource_type": "图片"
                }
              },
              {
                "term": {
                  "resource_type": "语音"
                }
              },
              {
                "term": {
                  "resource_type": "视频"
                }
              },
              {
                "term": {
                  "resource_type": "FLASH"
                }
              }
            ]
          }
        },
        {
          "bool": {
            "should": [
                {
                "term": {
                  "flow_type": "上行"
                }
              }
            ]
          }
        },
        {
          "range": {
            "timestamp": {
              "gte": "2018-02-25 09:51:52",
              "lte": "2018-05-25 09:51:52"
            }
          }
        }
      ]
    }
  },
  "sort": {
    "timestamp": {
      "order": "desc"
    }
  }
} 
        */
        public static string search( string key, string phone_num, string[] protocol_types, string flow_type, string timestart, string timeend, string[] resource_types )
        {
            string result = string.Empty;
            //单节点
            //var settings = new ConnectionSettings(new Uri("http://192.168.131.16:9200"))
            //    .DefaultIndex("tekuan");

            //集群
            var nodes = new Node[]
            {
                new Node(new Uri("http://192.168.131.16:9200")),
                new Node(new Uri("http://192.168.131.15:9200")),
                new Node(new Uri("http://192.168.131.13:9200")),
                new Node(new Uri("http://192.168.131.12:9200"))
            };

            var connectionPool = new StaticConnectionPool(nodes);
            var settings = new ConnectionSettings(connectionPool)
                .DefaultIndex("tekuan");

            var client = new ElasticClient(settings);
            var searchResponse = client.Search<record>
                (
                    s => s
                    .Size(10)
                    .From(0)
                    .Query(q => q
                        .Bool(b => b
                        .Must(
                            m => m.Bool(
                                    l => l
                                    .Should(d => d
                                    .Match(c => c
                                    .Field(f => f.title).Query(key))
                                    ||
                                    d.Match(c => c
                                    .Field(f => f.content).Query(key))
                                )
                            )
                            &&
                            //phone_num
                            m.Bool(l => l
                            .Should(d => d
                            .Term(t => t.Field(f => f.phone_num).Value(phone_num))))
                            &&
                            //resource_type
                            m.Bool(l => l
                            .Should(d =>
                            //terms多值
                            d.Terms(t => t.Field(f => f.resource_type).Terms(resource_types))
                            //term单值
                            //d.Term(t=>t.Field(f=>f.resource_type).Value("HTML"))
                            //||
                            //d.Term(t=>t.Field(f=>f.resource_type).Value("CSS"))
                            //||
                            //d.Term(t=>t.Field(f=>f.resource_type).Value("图片"))
                            //||
                            //d.Term(t => t.Field(f => f.resource_type).Value("语音"))
                            //||
                            //d.Term(t => t.Field(f => f.resource_type).Value("视频"))
                            //||
                            //d.Term(t => t.Field(f => f.resource_type).Value("FLASH"))
                            ))
                            &&
                            //protocol_type
                            m.Bool(l => l
                            .Should(d =>
                            //terms多值
                            d.Terms(t => t.Field(f => f.protocol_type).Terms(protocol_types))

                            //term单值
                            //d.Term(t=>t.Field(f=>f.protocol_type).Value("HTTP"))
                            //||
                            //d.Term(t => t.Field(f => f.protocol_type).Value("HTTPS"))
                            //||
                            //d.Term(t => t.Field(f => f.protocol_type).Value("FTP"))
                            //||
                            //d.Term(t => t.Field(f => f.protocol_type).Value("SMTP"))
                            //||
                            //d.Term(t => t.Field(f => f.protocol_type).Value("POP3"))
                            //||
                            //d.Term(t => t.Field(f => f.protocol_type).Value("P2P"))

                            ))
                            &&
                            //flow_type
                            m.Bool(l => l
                            .Should(d => d.Term(t => t.Field(f => f.flow_type).Value(flow_type))
                            ))
                            &&
                            //timestamp范围
                            m.DateRange(r => r
                            .Field("timestamp")
                            .GreaterThanOrEquals(DateMath.FromString(timestart))
                            .LessThanOrEquals(DateMath.FromString(timeend)))

                        )))
                        //timestamp时间排序
                        .Sort(c => c
                        .Field("timestamp", SortOrder.Descending)));



            if (null != searchResponse)
            {
                Console.WriteLine("返回的总条数: " + searchResponse.Total);
                Console.WriteLine("\n================================================================================================");
                var documents = searchResponse.Documents;
                //System.IO.FileStream fs = new System.IO.FileStream(@"D:\tekuan.json", System.IO.FileMode.OpenOrCreate);
                //StringBuilder sb = new StringBuilder();
                foreach (var doc in documents)
                {
                    //sb.Append(Newtonsoft.Json.JsonConvert.SerializeObject(doc));                  
                    Console.WriteLine("title: " + doc.title + ",\t\ttimestamp: " + doc.timestamp + ",\t\t phone_num: " + doc.phone_num);


                }
                #region //将查询结果写入到本地ES中
                //IEnumerable<record> enumerator = searchResponse.Documents;
                //foreach (var en in enumerator)
                //{
                //    TestBank.clientTekuan.Index<record>(en);
                //}

                ////System.IO.File.AppendAllText(@"D:\tekuan.json", Newtonsoft.Json.JsonConvert.SerializeObject(documents));
                //Console.WriteLine("写入成功!");
                #endregion

            }
            Console.ReadLine();
            return result;
        }
    }
}
