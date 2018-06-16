/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：ESearch
*文件名称：TestBank
*创建人：  shuyizhi
*创建时间：2018/5/29 15:11:43
*文件描述: 测试索引为Bank
* 参考：https://www.cnblogs.com/xing901022/p/4967796.html
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NLog;

namespace ESearch
{
    public class TestBank
    {
        public static ElasticClient client = new ElasticClient(Setting.ConnectionSettings);
        public static ElasticClient clientStudent = new ElasticClient(Setting.ConnectionSettings.DefaultIndex("db_student"));
        public static ElasticClient clientTekuan = new ElasticClient(Setting.ConnectionSettings.DefaultIndex("tekuan"));
        /// <summary>
        /// 查询所有
        /// </summary>
        public static void MatchAll()
        {

            //var node = new Uri("http://localhost:9200");
            //var settings = new ConnectionSettings(node);
            //var client = new ElasticClient(settings);

            //var responseResult = client.Search<bank>(
            //    c => c
            //    .Query(q =>
            //    q.MatchAll()
            //    )
            //    );


            //var responseResult = client.Search<bank>(s => s.Index("bank"));


            var responseResult = client.Search<Account>(
                    c => c
                    .Query(q => q
                    .Match(m => m
                    .Field(f => f.address).Query("mill")
                    )));

            if (null != responseResult)
            {
                Console.WriteLine("获取的总条数： " + responseResult.Total);
                Console.WriteLine("\n=================================================================================");
                var docs = responseResult.Documents;
                if (null != docs)
                {
                    foreach (var doc in docs)
                    {
                        Console.WriteLine("firstname: " + doc.firstname + "\t lastname: " + doc.lastname);
                    }
                }
            }
        }

        public static void Match()
        {
            //同时包含mill和lane的文档
            //var responseResult = client.Search<Bank>(
            //    c => c
            //    .Query(q => q
            //    .Bool(b => b
            //    .Must(m => m
            //    .Match(t => t
            //    .Field(f => f.address).Query("mill"))
            //    &&
            //    m.Match(t => t
            //    .Field(f => f.address).Query("lane"))
            //    ))));

            //查询包含mill或者lane的文档
            //var responseResult = client.Search<Bank>(
            //    c => c
            //    .Query(q => q
            //    .Bool(b => b
            //    .Should(s =>
            //    s.Match(t => t
            //    .Field(f => f.address).Query("mill"))
            //    ||
            //    s.Match(t => t.Field(f => f.address).Query("lane"))
            //    ))));

            //排除包含mill和lane的文档
            //var responseResult = client.Search<Bank>(
            //        c => c
            //        .Query(q => q
            //        .Bool(b => b
            //        .MustNot(n => n
            //        .Match(m => m
            //        .Field(f => f.address).Query("mill"))
            //        ||
            //        n.Match(m => m
            //        .Field(f => f.address).Query("lane"))
            //        ))));

            //var responseResult = client.Search<Bank>(
            //      c => c
            //      .Query(q => q
            //                .Bool(b => b
            //                        .Must(m => m
            //                        .Match(t => t
            //                        .Field("age").Query("40")))
            //                        .MustNot(mn => mn
            //                            .Match(m => m
            //                            .Field("state").Query("ID")
            //                            )))));

            //查询在2000-3000范围内的所有文档
            //DSL:{"query":{"bool":{"must":[{"match_all":{}}],"filter":{"range":{"balance":{"gte":20000,"lte":30000}}}}}}
            //var responseResult = client.Search<Bank>(
            //    c => c
            //    .Query(q => q
            //        .Bool(b => b
            //          .Must(m => m
            //            .MatchAll())
            //            .Filter(f => f
            //                 .Range(r => r
            //                  .Field(d => d.balance)
            //                     .GreaterThanOrEquals(20000).LessThanOrEquals(30000)
            //    )))));

            //查询address 中包含lane，年龄在20~30岁之间的所有文档
            //DSL :{"query":{"bool":{"must":[{"match":{"address":"lane"}},{"range":{"age":{"gte":20,"lte":30}}}]}}}

            //var responseResult = client.Search<Bank>(
            //        s => s
            //        .Query(q => q
            //        .Bool(b => b
            //        .Must(m => m
            //        .Match(mt => mt
            //        .Field(f => f.address)
            //        .Query("lane"))
            //        &&
            //        m.Range(r => r
            //        .Field(f => f.age).GreaterThanOrEquals(20).LessThanOrEquals(30))
            //        )))
            //        .Sort(c => c
            //            .Field("account_number", SortOrder.Descending)));

            //同时包含mill和lane的文档
            //DSL:{"query":{"match_phrase":{"address":"mill lane"}}}
            //var responseResult = client.Search<Bank>(
            //        s => s
            //        .Query(q => q
            //        .MatchPhrase(mp => mp
            //        .Field(f => f.address)
            //        .Query("mill lane")
            //        )));

            //address包含mill 或 lane的文档
            //DSL:{"query":{"bool":{"should":[{"match":{"address":"lane"}},{"match":{"address":"mill"}}]}},"sort":[{"account_number":{"order":"desc"}}]}
            
            //lambda表达式写法
            //var responseResult = client.Search<Bank>(
            //        s => s
            //        .Query(q => q
            //        .Bool(b => b
            //        .Should(sh => sh
            //        .Match(m => m.Field(f => f.address).Query("lane"))
            //        ||
            //        sh.Match(m => m.Field(f => f.address).Query("mill"))
            //        )))
            //        .Sort(c => c
            //        .Field("account_number", SortOrder.Descending)));

            //对象写法
            var searchRequest = new SearchRequest()
            {
                Sort = new List<ISort>()
                {
                    new SortField(){ Field="account_number", Order=SortOrder.Descending }
                },
                Size = 10,
                From = 0,
                Query = new MatchQuery() { Field = "address", Query = "lane" }
                ||
                new MatchQuery() { Field = "address", Query = "mill" }
            };

            var responseResult = client.Search<Account>(searchRequest);

            if (null != responseResult)
            {
                Console.WriteLine("获取的总条数： " + responseResult.Total);
                Console.WriteLine("\n==========================================================================================");
                var docs = responseResult.Documents;

                //string res = Newtonsoft.Json.JsonConvert.SerializeObject(docs);
                //Console.WriteLine(res);

                if (null != docs)
                {
                    foreach (var doc in docs)
                    {
                        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(doc));
                        //Console.WriteLine("firstname: " + doc.firstname + "\t lastname: " + doc.lastname + "\t balance: " + doc.balance);
                    }
                }
            }
        }
        /// <summary>
        /// 创建索引
        /// </summary>
        public static void CreateIndex()
        {
            var description = new CreateIndexDescriptor("db_student")
                .Settings(s => s.NumberOfShards(5).NumberOfReplicas(1))
                .Mappings(ms => ms
                .Map<Student>(m => m.AutoMap()));

            var index = client.CreateIndex(description);
            Console.WriteLine(index.IsValid);
        }
        /// <summary>
        /// 插入模型为student的数据
        /// </summary>
        public static void InsertData()
        {
            var datas = new List<Student>()
            {
                new Student(){ Id=Guid.NewGuid().ToString(),Name="student1",DateTime=new DateTime(2018,05,23,15,23,45), Description="student1student1" },
                new Student(){ Id=Guid.NewGuid().ToString(),Name="student2", DateTime=DateTime.Now,Description="student2student2" }
            };
            //Nest.BulkResponse response= (Nest.BulkResponse)client.IndexMany<Student>(datas);

            foreach (var data in datas)
            {
                clientStudent.Index<Student>(data);
            }
        }
        /// <summary>
        /// 查询所有
        /// </summary>
        public static void QueryData()
        {
            var responseResult = clientStudent.Search<Student>(s => s
              .Query(q => q
                  .MatchAll()));
            if (null != responseResult)
            {
                Console.WriteLine("获取的总数: " + responseResult.Total);
                IEnumerable<Student> students = responseResult.Documents;
                Console.WriteLine("\n==========================================================================================");
                foreach (var student in students)
                {
                    Console.WriteLine(student.ToString());
                }

            }
        }

        public static void QueryDataByCondition()
        {
            var responseResult = clientStudent.Search<Student>(s => s.Query(q => q.Match(m => m.Field("name").Query("student1"))));

            if (null != responseResult)
            {
                Console.WriteLine("获取的总数: " + responseResult.Total);
                IEnumerable<Student> students = responseResult.Documents;
                Console.WriteLine("\n==========================================================================================");
                foreach (var student in students)
                {
                    Console.WriteLine(student.ToString());
                }

            }

            var searchRequest = new SearchRequest<Student>()
            {
                Query = new MatchQuery()
                {
                    Field = "name",
                    Query = "student1"
                }
            };
            var searchResult = clientStudent.Search<Student>(searchRequest);
            if (null != searchResult)
            {
                Console.WriteLine("==================================对象初始化方式===========================================");
                Console.WriteLine("获取的总数：" + searchResult.Total);
                foreach (var stu in searchResult.Documents)
                {
                    Console.WriteLine(stu.ToString());
                }

            }
        }

        public static void QueryTekuanByCondition()
        {
            ESReturnModel returnModel = new ESReturnModel();
            var searchResult = clientTekuan.Search<record>(s => s
               .Query(q => q
                   .Match(m => m
                   .Field(f => f.title).Query("合肥")
                   )));

            if (null != searchResult)
            {
                IEnumerable<IHit<record>> hits = searchResult.Hits;
                List<ReturnModel> records = new List<ReturnModel>();
                foreach (var hit in hits)
                {
                    ReturnModel model = new ReturnModel();
                    model._id = hit.Id;
                    model._index = hit.Index;
                    model._score = hit.Score;
                    model._source = hit.Source;
                    model._type = hit.Type;

                    records.Add(model);

                }
                returnModel.Hits = records;
                returnModel.Total = searchResult.Total;
            }

            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(returnModel));
        }

        /// <summary>
        /// 删除单条数据
        /// 删除Id 为7540795-fe9a-4a4c-bb95-84b723f00788的数据(文档)
        /// </summary>
        public static void DeleteDataById()
        {
            var id = "67540795-fe9a-4a4c-bb95-84b723f00788";
            DocumentPath<Student> documentPath = new DocumentPath<Student>(id);
            var del = clientStudent.Delete(documentPath);
            if (del.IsValid)
            {
                Console.WriteLine("删除成功!");
            }
            else
            {
                Console.WriteLine("删除失败!");
            }
        }
        /// <summary>
        /// 批量删除
        /// http://www.cnblogs.com/xing901022/p/4963542.html
        /// </summary>
        public static void DeleteDataByRange()
        {

        }

        /**
            Kibana控制台
            POST db_student/student/67233b15-d66c-40c2-ac40-74c8e91b3ce1
            {
                "name": "67233b15-d66c-40c2-ac40-74c8e91b3ce167233b15-d66c-40c2-ac40-74c8e91b3ce1",
                "description": 67233b15-d66c-40c2-ac40-74c8e91b3ce167233b15-d66c-40c2-ac40-74c8e91b3ce1",
                "dateTime": "2018-05-30T15:23:45"
            }
        */
        /**
            RESTClient测试
            POST http://localhost:9200/db_student/student/67233b15-d66c-40c2-ac40-74c8e91b3ce1
            {
                "name": "67233b15-d66c-40c2-ac40-74c8e91b3ce167233b15-d66c-40c2-ac40-74c8e91b3ce1",
                "description": 67233b15-d66c-40c2-ac40-74c8e91b3ce167233b15-d66c-40c2-ac40-74c8e91b3ce1",
                "dateTime": "2018-05-30T15:23:45"
            }
        */

        /// <summary>
        /// 根据Id更新整个文档
        /// </summary>
        public static void UpdateDocumentById()
        {
            var id = "67233b15-d66c-40c2-ac40-74c8e91b3ce1";
            DocumentPath<Student> documentPath = new DocumentPath<Student>(id);

            var response = clientStudent.Update(documentPath, f => f.Doc(new Student()
            {
                DateTime = DateTime.Now,
                Description = "67233b15-d66c-40c2-ac40-74c8e91b3ce167233b15-d66c-40c2-ac40-74c8e91b3ce1",
                Name = "程序包管理器控制台"
            }));

            if (response.IsValid)
            {
                Console.WriteLine("更新doc成功!");
            }
            else
            {
                Console.WriteLine("更新doc失败!");
            }
        }
        /// <summary>
        /// 根据Id更新部分数据
        /// </summary>
        public static void UpdatePartById()
        {
            var id = "67233b15-d66c-40c2-ac40-74c8e91b3ce1";
            DocumentPath<Student> documentPath = new DocumentPath<Student>(id);
            IUpdateRequest<Student, object> request = new UpdateRequest<Student, object>(documentPath)
            {
                Doc = new
                {
                    Name = "shuyizhi"
                }
            };
            var result = clientStudent.Update(request);
            if (null != result)
            {
                if (result.IsValid)
                {
                    Console.WriteLine("更新成功!");
                }
                else
                {
                    Console.WriteLine("更新成功!");
                }
            }
        }

        public static void AddBankDocument()
        {
            try
            {
                string str = "{\"account_number\":30,\"balance\":50000,\"firstname\":\"song\",\"lastname\":\"li\",\"age\":35,\"gender\":\"M\",\"address\":\"安徽省巢湖市巢湖学院\",\"employer\":\"songli\",\"email\":\"songli@microsoft.com\",\"city\":\"巢湖\",\"state\":\"安徽\"}";

                Account bank = Newtonsoft.Json.JsonConvert.DeserializeObject<Account>(str);
                if (null != bank)
                {
                    var createIndex = client.Index(bank, f => f.Index("bank").Type("account"));     //需要制定indx和type名称
                    if (null != createIndex)
                    {
                        if (createIndex.IsValid)
                        {
                            Console.WriteLine("插入index为bank的document成功!");
                        }
                        else
                        {
                            Console.WriteLine("插入index为bank的document成功!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("插入文档错误: " + ex.Message);
            }

        }
    }
}
