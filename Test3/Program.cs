using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test3
{
    internal class Program
    {
        private static void Main( string[] args )
        {
            //testES();

            //string result= testParams(20, 0, "合肥",
            //     new string[] { "HTTP", "HTTPS", "SMTP", "POP3" },
            //     "上行",
            //     new string[] { "CSS", "FLASH", "图片", "语音" },
            //     "2018-02-23 16:14:12",
            //     "2018-05-23 16:14:12",
            //     new string[] { "15000825521" });

            var str = "{\"key\":\"\",\"timestampstart\":\"2018-02-24 17:20:08\",\"timestampend\":\"2018-05-24 17:20:08\",\"phone_num\":[\"\"],\"protocol_type\":[\"HTTP\",\"HTTPS\",\"FTP\",\"SMTP\",\"POP3\",\"P2P\"],\"flow_type\":\"\",\"resource_type\":[\"HTML\",\"CSS\",\"图片\",\"语音\",\"视频\",\"FLASH\"],\"size\":10,\"from\":0}";

            //var str = "{\"key\":\"合肥\",\"timestampstart\":\"2018-02-23 16:14:12\",\"timestampend\":\"2018-05-23 16:14:12\",\"phone_num\":[],\"protocol_type\":[\"HTTP\",\"HTTPS\",\"FTP\",\"SMTP\",\"POP3\",\"P2P\"],\"flow_type\":\"\",\"resource_type\":[\"HTML\",\"CSS\",\"图片\",\"语音\",\"视频\",\"FLASH\"],\"size\":10,\"from\":0}";

            //var str = "{\"key\":\"合肥\",\"timestampstart\":\"2018-02-23 16:14:12\",\"timestampend\":\"2018-05-23 16:14:12\",\"phone_num\":[],\"protocol_type\":[\"HTTP\",\"HTTPS\",\"FTP\",\"SMTP\",\"POP3\",\"P2P\"],\"flow_type\":\"上行\",\"resource_type\":[\"HTML\",\"CSS\",\"图片\",\"语音\",\"视频\",\"FLASH\"],\"size\":10,\"from\":0}";

            //var str = "{\"key\":\"合肥\",\"timestampstart\":\"2018-02-23 16:14:12\",\"timestampend\":\"2018-05-23 16:14:12\",\"phone_num\":[],\"protocol_type\":[],\"flow_type\":\"上行\",\"resource_type\":[\"HTML\",\"CSS\",\"图片\",\"语音\",\"视频\",\"FLASH\"],\"size\":10,\"from\":0}";

            //var str = "{\"key\":\"合肥\",\"timestampstart\":\"2018-02-23 16:14:12\",\"timestampend\":\"2018-05-23 16:14:12\",\"phone_num\":[],\"protocol_type\":[],\"flow_type\":\"上行\",\"resource_type\":[\"HTML\"],\"size\":10,\"from\":0}";

            //var str = "{\"key\":\"\",\"timestampstart\":\"2018-02-23 16:14:12\",\"timestampend\":\"2018-05-23 16:14:12\",\"phone_num\":[],\"protocol_type\":[],\"flow_type\":\"上行\",\"resource_type\":[\"HTML\"],\"size\":10,\"from\":0}";

            var param = Newtonsoft.Json.JsonConvert.DeserializeObject<ParamsModel>(str);

            string result = testParams(param.size, param.from, param.key, param.protocol_type, param.flow_type, param.resource_type, param.timestampstart, param.timestampend, param.phone_num);

            Console.WriteLine(result);
            Console.Read();
        }

        public static void testES()
        {
            var nodes = new Uri[]
            {
                 new Uri("http://192.168.131.12:9200"),
                 new Uri("http://192.168.131.13:9200"),
                 new Uri("http://192.168.131.15:9200"),
                 new Uri("http://192.168.131.16:9200")
            };

            //var connectionPool = new SniffingConnectionPool(new Uri[] { new Uri("http://192.168.131.12:9200") });
            var connectionPool = new StaticConnectionPool(nodes);
            var settings = new ConnectionSettings(connectionPool).DefaultFieldNameInferrer(( name ) => name);

            var client = new ElasticClient(settings);

            //var manyStrings = Nest.Indices.Index("tekuan");

            //ISearchRequest manyStringRequest = new SearchDescriptor<ESModel>().Index(manyStrings);

            //var searchResponse = client.Search<ESModel>(s => s.Query(q => q.MatchAll()));

            var search1 = client.Search<ESModel>(s => s
      .From(0)
      .Size(100)
      .Query(q => q
           .Match(m => m
              .Field(f => f.phone_num)
              .Query("17721145375")
           )
      ).
       Query(
                q => q.
                DateRange(r => r
                .Field(f => f.timestamp).GreaterThanOrEquals(new DateTime(2018, 3, 17, 0, 0, 0)).LessThanOrEquals(new DateTime(2018, 5, 18, 16, 45, 15))))
            .Sort(l => l.Descending(asc => asc.phone_num)));

            var search2 = client.Search<ESModel>(s => s
     .From(0)
     .Size(100)
     .Query(q => q
          .Match(m => m
             .Field(f => f.phone_num)
             .Query("17721145375")
          )
     ).
      Query(
               q => q.
               DateRange(r => r
               .Field(f => f.timestamp).GreaterThanOrEquals(new DateTime(2018, 3, 17, 0, 0, 0)).LessThanOrEquals(new DateTime(2018, 5, 18, 16, 45, 15)))));

            var search3 = client.Search<ESModel>(s => s
              .From(0)
              .Size(100)
              .Query(q =>
              q.Bool(b =>
              b.Should(
                  o => o.Match(
                     m => m.Field(
                         f => f.title)
                  .Query("合肥")
                  ),
                  n => n.Match(
                      l => l.Field(
                          g => g.phone_num)
                          .Query("15000825521"))))));

            Root root = new Root();
            root.from = 0;
            root.size = 10;
            Sort sort = new Sort();

            Timestamp timestampsort = new Timestamp();
            timestampsort.order = "desc";

            sort.timestamp = timestampsort;

            root.sort = sort;

            Query query = new Query();

            Bool _bool = new Bool();

            Should should = new Should();

            Term termprotocal1 = new Term();
            termprotocal1.protocol_type = "POP3";
            Term termprotocal2 = new Term();
            termprotocal2.protocol_type = "P2P";
            Term termprotocal3 = new Term();
            termprotocal3.protocol_type = "FTP";

            List<Term> terms = new List<Term>()
            {
                termprotocal1,
                termprotocal2,
                termprotocal3
            };
            //should.term = terms;

            Term termtitle = new Term();
            termtitle.title = "合肥";
            MustItem itemtitle = new MustItem();
            itemtitle.term = termtitle;

            List<MustItem> musts = new List<MustItem>();
            musts.Add(itemtitle);

            //_bool.must = musts;

            List<Should> shoulds = new List<Should>();
            Should should1 = new Should();
            should1.term = termprotocal1;

            Should should2 = new Should();
            should2.term = termprotocal2;

            Should should3 = new Should();
            should3.term = termprotocal3;

            shoulds.Add(should1);
            shoulds.Add(should2);
            shoulds.Add(should3);

            _bool.should = shoulds;

            MustItem item = new MustItem();
            Range range = new Range();

            Timestamp timestamprange = new Timestamp();
            timestamprange.gte = "2017-04-01 00:00:00";
            timestamprange.lte = "2018-05-01 00:00:00";

            range.timestamp = timestamprange;
            item.range = range;
            _bool.must = new List<MustItem>()
            {
                item,
                itemtitle
            };

            query._bool = _bool;

            root.query = query;
            var jSetting = new Newtonsoft.Json.JsonSerializerSettings { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore };
            string str = Newtonsoft.Json.JsonConvert.SerializeObject(root, jSetting);
            str = str.Replace("_bool", "bool");

            //var param = "{ \"query\":{ \"bool\":{ \"should\":[ {\"match\":{\"title\":\"合肥\"}}, {\"match\":{\"phone_num\":\"15000825521\"}} ] } }, \"sort\": { \"timestamp\":{ \"order\":\"desc\" } } }";
            var param = "{\"size\":20,\"from\":0,\"query\":{\"bool\":{\"must\":[{\"term\":{\"title\":\"合肥\"}},{\"bool\":{\"should\":[{\"term\":{\"protocol_type\":\"POP3\"}},{\"term\":{\"protocol_type\":\"P2P\"}},{\"term\":{\"protocol_type\":\"FTP\"}},{\"term\":{\"resource_type\":\"CSS\"}},{\"term\":{\"flow_type\":\"下行\"}}]}},{\"range\":{\"timestamp\":{\"gte\":\"2017-04-01 00:00:00\",\"lte\":\"2018-05-01 00:00:00\"}}}]}},\"sort\":{\"timestamp\":{\"order\":\"desc\"}}}";

            //StringBuilder sb = new StringBuilder();
            //sb.Append("")

            var search4 = HttpHelper.PostHttpResponse("http://192.168.131.12:9200/tekuan/_search", param);

            //foreach (var item in search1.Hits)
            //{
            //    Console.WriteLine(item.Source.phone_num + "," + item.Source.timestamp);
            //}
            //Console.WriteLine("\n\n========================================================\n\n");
            //foreach (var item in search2.Hits)
            //{
            //    Console.WriteLine(item.Source.phone_num + "," + item.Source.timestamp);
            //}

            Console.WriteLine("\n\n========================================================\n\n");

            //foreach (var item in search3.Hits)
            //{
            //    Console.WriteLine(item.Source.phone_num + ",\t" + item.Source.timestamp + ",\t" + item.Source.protocol_type);
            //}

            //Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(search1.Hits.ToList()));
        }

        public static string testParams( long size, long from, string title, string[] protocol_type, string flow_type, string[] resource_type, string timestart, string timend, string[] phone_num )
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{"
             + "\"size\": " + size + ","
             + "\"from\": " + from + ",");

            sb.Append(""
             + "\"query\": {"
             + "\"bool\": {"
             + "\"must\": [");

            sb.Append("{"
              + "\"bool\": {"
              + "\"should\": [");

            if (!string.IsNullOrEmpty(title))
            {
                sb.Append("{"
                      + "\"match\": {"
                    + "\"content\": \"" + title.Trim() + "\""
                    + "}"
                    + "},"
                    + "{"
                    + " \"match\": {"
                    + "\"title\": \"" + title.Trim() + "\""
                    + "}"
                    + "}");
            }
            sb.Append("]"
                + "}"
                + "},");

            sb.Append("{"
             + "\"bool\": {"
             + "\"should\": [");

            if (null != phone_num && phone_num.Length > 0)
            {
                foreach (var phone in phone_num)
                {
                    sb.Append("{"
                      + " \"term\": {"
                      + "\"phone_num\": \"" + phone.Trim() + "\""
                      + "}"
                      + "}");
                }
            }
            sb.Append("]"
                + "}"
               + "},");

            sb.Append(""
            + "{"
            + "\"bool\": {"
            + "\"should\": [");
            if (null != protocol_type && protocol_type.Length > 0)
            {
                for (int i = 0; i < protocol_type.Length; i++)
                {
                    if (i == protocol_type.Length - 1)
                    {
                        sb.Append("{"
                              + "\"term\": {"
                              + "\"protocol_type\": \"" + protocol_type[i] + "\""
                              + "}"
                              + "}");
                    }
                    else
                    {
                        sb.Append("{"
                                + "\"term\": {"
                                + "\"protocol_type\": \"" + protocol_type[i] + "\""
                                + "}"
                                + "},");
                    }
                }
            }
            sb.Append("]"
                  + "}"
                  + "},");

            sb.Append(""
          + " {"
          + "\"bool\": {"
            + "\"should\": [");

            if (null != resource_type && resource_type.Length > 0)
            {
                for (int i = 0; i < resource_type.Length; i++)
                {
                    if (i == resource_type.Length - 1)
                    {
                        sb.Append("{"
                             + "\"term\": {"
                             + "\"resource_type\": \"" + resource_type[i] + "\""
                             + "}"
                             + "}"
                                );
                    }
                    else
                    {
                        sb.Append("{"
                             + "\"term\": {"
                             + "\"resource_type\": \"" + resource_type[i] + "\""
                             + "}"
                             + "},"
                                );
                    }
                }
            }

            sb.Append("]"
                  + "}"
                  + "},");

            sb.Append(""
                + "{"
                + "\"bool\": {"
                + "\"should\": [");

            if (!string.IsNullOrEmpty(flow_type))
            {
                sb.Append("{"
                   + "\"term\": {"
                   + "\"flow_type\": \"" + flow_type.Trim() + "\""
                   + "}"
                   + "}");
            }

            sb.Append("]"
                  + "}"
                  + "},");

            if (!string.IsNullOrEmpty(timestart) && !string.IsNullOrEmpty(timend))
            {
                sb.Append(""
                  + "{"
                  + "\"range\": {"
                  + "\"timestamp\": {"
                  + "\"gte\": \"" + timestart.Trim() + "\","
                  + "\"lte\": \"" + timend.Trim() + "\""
                  + "}"
                  + "}"
                  + "}");
            }

            sb.Append("]"
             + "}"
            + "},");

            sb.Append(""
              + "\"sort\": {"
              + "\"timestamp\": {"
              + "\"order\": \"desc\""
              + "}"
              + "}"
              + "}");

            //            sb.Append("{"
            //                     + "\"size\": " + size + ","
            //                     + "\"from\": " + from + ","
            //                     + "\"query\": {"
            //                     + "\"bool\": {"
            //                     + "\"must\": [");

            //            if (!string.IsNullOrEmpty(title))
            //            {
            //                sb.Append(""
            //                  + "{"
            //                  + "\"term\": {"
            //                  + "\"title\": \"" + title.Trim() + "\""
            //                  + "}"
            //                  + "},");
            //            }
            //            if (null != phone_num && phone_num.Length > 0)
            //            {
            //                foreach (var phone in phone_num)
            //                {
            //                    {
            //                        sb.Append(""
            //                          + "{"
            //                          + "\"term\": {"
            //                          + "\"phone_num\": \"" + phone.Trim() + "\""
            //                          + "}"
            //                          + "},");
            //                    }
            //                }
            //            }

            //            sb.Append("{"
            //                     + " \"bool\": {"
            //                     + "\"should\": [");

            //                StringBuilder ptype = new StringBuilder();
            //            if (null != protocol_type && protocol_type.Length > 0)
            //            {
            //                foreach (var type in protocol_type)
            //                {
            //                    ptype.Append(""
            //                    + " {"
            //                    + "\"term\": {"
            //                    + "\"protocol_type\": \"" + type.Trim() + "\""
            //                    + "}"
            //                    + "},");
            //                }
            //                //sb.Append(ptype.ToString().Substring(0, ptype.ToString().Length - 1));
            //            }
            //            StringBuilder rtype = new StringBuilder();

            //            if (null != resource_type && resource_type.Length > 0)
            //            {
            //                foreach (var resource in resource_type)
            //                {
            //                    rtype.Append("{"
            //                             + "\"term\": {"
            //                             + "\"resource_type\": \"" + resource.Trim() + "\""
            //                             + "}"
            //                          + "},");
            //                }

            //                //sb.Append(rtype.ToString().Substring(0,rtype.ToString().Length - 1));
            //            }
            //            sb.Append(string.Join(",",ptype.ToString().Substring(0, ptype.ToString().Length - 1), rtype.ToString().Substring(0, rtype.ToString().Length - 1)));

            //            if (!string.IsNullOrEmpty(flow_type))
            //            {
            //                sb.Append(",{"
            //                          + "\"term\": {"
            //                          + "\"flow_type\": \"" + flow_type.Trim() + "\""
            //                          + "}"
            //                          + "}");
            //            }

            //            sb.Append(""
            //                + "]"
            //                + "}"
            //                + "},"
            //           + "{");

            //            if (!string.IsNullOrEmpty(timend) && !string.IsNullOrEmpty(timestart))
            //            {
            //                sb.Append(""
            //                    + "\"range\": {"
            //                    + "\"timestamp\": {"
            //                    + "\"gte\": \"" + timestart.Trim() + "\","
            //                    + "\"lte\": \"" + timend.Trim() + "\""
            //                    + "}"
            //                    + "}");
            //            }

            //            sb.Append(""
            //        + "}"
            //      + "]"
            //    + "}"
            //  + "},"
            //  + "\"sort\": {"
            //    + "\"timestamp\": {"
            //      + "\"order\": \"desc\""
            //    + "}"
            //  + "}"
            //+ "}");

            string result = HttpHelper.PostHttpResponse("http://192.168.131.12:9200/tekuan/_search", sb.ToString());
            return result;
        }
    }
}