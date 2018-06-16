using NUnit.Framework;
using ESearch;
/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：ESearchTests1
*文件名称：ElasticSearchHelpTests
*创建人：  shuyizhi
*创建时间：2018/6/8 17:00:28
*文件描述: 
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace ESearch.Tests
{
    [TestFixture()]
    public class ElasticSearchHelpTests
    {
        /// <summary>
        /// 索引为db_student的连接对象
        /// </summary>
        public static ElasticClient clientStudent = new ElasticClient(Setting.ConnectionSettings.DefaultIndex("db_student"));
        /// <summary>
        /// 索引为bank的连接对象
        /// </summary>
        public static ElasticClient clientBank = new ElasticClient(Setting.ConnectionSettings.DefaultIndex("bank"));
        //public static ElasticClient clientAccount = new ElasticClient(Setting.ConnectionSettings);
        [Test()]
        public void InsertDocumentsTest()
        {
            var datas = new List<Student>()
            {
                new Student(){ Id=Guid.NewGuid().ToString(),Name="student11",DateTime=DateTime.Now, Description="student11student11"},
                new Student(){ Id=Guid.NewGuid().ToString(),Name="student22", DateTime=DateTime.Now,Description="student22student22"}
            };
            Assert.AreEqual(ElasticSearchHelp.InsertDocuments<Student>(clientStudent, datas), true);

        }
        [Test()]
        public void SearchDocumentsTest()
        {
            #region //普通简单查询
            //var searchRequest = new SearchRequest()
            //{
            //    //Query=new MatchQuery()
            //    //{
            //    //    Field="name",
            //    //    Query= "student22"
            //    //}

            //    //Query = new MatchAllQuery()
            //    //{

            //    //}

            //    ////DSL :{"size":20,"from":0,"query":{"bool":{"should":[{"match":{"address":"lane"}},{"match":{"address":"mill"}}]}},"sort":[{"account_number":{"order":"desc"}}]}
            //    //From = 0,
            //    //Size=20,
            //    //Sort=new List<ISort>()
            //    //{
            //    //    new SortField()
            //    //    {
            //    //        Field="account_number",
            //    //        Order=SortOrder.Descending
            //    //    }
            //    //},
            //    //},
            //    //Query=new MatchQuery()
            //    //{
            //    //    Field="address",
            //    //    Query="lane"
            //    //}
            //    //||
            //    //new MatchQuery()
            //    //{
            //    //    Field="address",
            //    //    Query="mill"
            //    //}
            //};

            #endregion

            #region //QueryContainer构建复合查询

            // DSL: {"from":0,"size":30,"query":{"bool":{"must":[{"bool":{"should":[{"term":{"age":{"value":"39"}}}]}},{"bool":{"should":[{"match":{"address":"Avenue"}},{"match":{"address":"Place"}}]}}]}},"sort":[{"account_number":{"order":"desc"}}]}
            var mustClauses = new List<QueryContainer>()
            {

            };

            mustClauses.Add(new TermQuery()
            {
                Field = "age",
                Value = 39
            });
            mustClauses.Add(new MatchQuery()
            {
                Field = "address",
                Query = "Avenue"
            }
            ||
            new MatchQuery()
            {
                Field = "address",
                Query = "Place"
            });

            var searchRequest = new SearchRequest<Account>()
            {
                From = 0,
                Size = 30,
                Query = new BoolQuery()
                {
                    Must = mustClauses
                },
                Sort = new List<ISort>()
                {
                    new SortField()
                    {
                        Field = "account_number",
                        Order = SortOrder.Descending
                    }
                }
            };


            //// DSL :{"query":{"bool":{"must":[{"bool":{"should":[{"match":{"address":"Avenue"}},{"match":{"address":"Place"}}]}},{"range":{"age":{"gte":20,"lte":30}}},{"range":{"balance":{"gte":2000,"lte":4000}}}]}},"sort":[{"account_number":{"order":"desc"}}]}
            //var mustClauses = new List<QueryContainer>();
            //mustClauses.Add(new MatchQuery()
            //{
            //    Field = "address",
            //    Query = "Avenue"
            //}
            //||
            //new MatchQuery()
            //{
            //    Field = "address",
            //    Query = "Place"
            //});

            //mustClauses.Add(new NumericRangeQuery()
            //{
            //    Field = "age",
            //    GreaterThanOrEqualTo = 20,
            //    LessThanOrEqualTo = 30
            //}
            //&&
            //new NumericRangeQuery()
            //{
            //    Field = "balance",
            //    GreaterThanOrEqualTo = 2000,
            //    LessThanOrEqualTo = 4000
            //});

            //var searchRequest = new SearchRequest<Account>()
            //{
            //    From = 0,
            //    Size = 20,
            //    Query = new BoolQuery()
            //    {
            //        Must = mustClauses,
            //    },
            //    Sort = new List<ISort>()
            //    {
            //        new SortField()
            //        {
            //            Field="account_number",
            //            Order=SortOrder.Descending
            //        }
            //    }
            //};

            #endregion

            #region //查询所有

            //var searchRequest = new SearchRequest<Account>()
            //{
            //    Query = new MatchAllQuery()
            //};

            #endregion


            var responseResult = ElasticSearchHelp.SearchDocuments<Account>(clientBank, searchRequest);
            if (null != responseResult && responseResult.IsValid)
            {
                Console.WriteLine("返回的总数: " + responseResult.Total);
                Console.WriteLine("========================================================================================================================================" +
                    "========================================================\n");
                foreach (var doc in responseResult.Documents)
                {
                    Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(doc));
                }
                Console.WriteLine("\n========================================================================================================================================" +
                    "========================================================");
            }
        }

        [Test()]
        public void UpdateDocumentByIdTest()
        {
            var doc = "{\"id\":\"00064e02-4947-4968-92d4-ef4b749daf21\",\"name\":\"束义志\",\"description\":\"student2student2\",\"dateTime\":\"2018-05-30T15:02:32.5847217+08:00\"}";
            string id = "00064e02-4947-4968-92d4-ef4b749daf21";

            Assert.AreEqual(ElasticSearchHelp.UpdateDocumentById<Student>(clientStudent, id, doc), true);

        }

        [Test()]
        public void DeleteDocumentByIdTest()
        {
            //string id = "f8dffa80-8872-4744-9e43-9dc5cf09661b";
            //Assert.AreEqual(ElasticSearchHelp.DeleteDocumentById<Student>(clientStudent, id), true);


            string id = "f8dffa80-8872-4744-9e43-9dc5cf09661b4";
            try
            {
                var result = ElasticSearchHelp.DeleteDocumentById<Student>(clientStudent, id);
                Console.WriteLine("result: " + result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [Test()]
        public void ImportByFileDataTest()
        {
            string filePath = @"D:\db_studentExport.json";
            try
            {
                int count = 0;
                var result = ElasticSearchHelp.ImportByFileData<Student>(clientStudent, filePath, ref count);
                Console.WriteLine("导入是否成功: " + result);
                Console.WriteLine("总共导入的条数: " + count);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [Test()]
        public void ExportFileBySearchTest()
        {
            string filePath = @"D:\db_studentTest.json";
            try
            {
                int count = 0;
                //查询所有
                var searchRequest = new SearchRequest<Account>()
                {
                    From = 0,
                    Size = 10000,
                    Query = new NumericRangeQuery()
                    {
                        Field = "age",
                        GreaterThanOrEqualTo = 20,
                        LessThanOrEqualTo = 30
                    }
                };

                Assert.AreEqual(ElasticSearchHelp.ExportFileBySearch<Account>(clientBank, filePath, ref count, searchRequest), true);
                Console.WriteLine("总共导出: " + count + "条数据");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [Test()]
        public void DeleteIndexTest()
        {
            string indexName = "twitter";

            Assert.AreEqual(ElasticSearchHelp.DeleteIndex(clientStudent, indexName), true);
        }

        [Test()]
        public void DeleteAllTypesByIndexTest()
        {
            string typeName = "student";
            string indexName = "db_student";
            Assert.AreEqual(ElasticSearchHelp.DeleteAllTypesByIndex<Student>(clientStudent, indexName, typeName), true);
        }
    }
}