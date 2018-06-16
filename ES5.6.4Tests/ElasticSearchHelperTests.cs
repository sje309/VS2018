using NUnit.Framework;
using ES5._6._4;
/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：ES5._6._4Tests
*文件名称：ElasticSearchHelperTests
*创建人：  shuyizhi
*创建时间：2018/6/13 14:00:13
*文件描述: 
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace ES5._6._4.Tests
{
    [TestFixture()]
    public class ElasticSearchHelperTests
    {
        /// <summary>
        /// 
        /// </summary>
        public static ElasticClient clientStudent = new ElasticClient(Settings.ConnectionSettings.DefaultIndex("db_student"));
        /// <summary>
        /// 
        /// </summary>
        public static ElasticClient clientBank = new ElasticClient(Settings.ConnectionSettings.DefaultIndex("bank"));
        [Test()]
        public void CreateIndexByTypeTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void InsertDocumentsTest()
        {
            var datas = new List<Student>()
            {
                new Student(){ DateTime=DateTime.Now,Description="程序包控制器管理台",Id=Guid.NewGuid().ToString(), Name="NAME1"},
                new Student(){DateTime=DateTime.Now.Subtract(TimeSpan.FromHours(12)),Id=Guid.NewGuid().ToString(),Description=".Net Reflector Analyzer",Name="NAME2"}
            };
            Assert.AreEqual(ElasticSearchHelper.InsertDocuments(clientStudent, datas), true);
            //Assert.Fail();
        }

        [Test()]
        public void SearchDocumentsTest()
        {
            SearchRequest<Student> searchRequest = new SearchRequest<Student>()
            {
                Query = new MatchQuery()
                {
                    Field = "name",
                    Query = "风清扬"
                }
            };
            var searchResponse = ElasticSearchHelper.SearchDocuments<Student>(clientStudent, searchRequest);
            Console.WriteLine("=====================================================================================================\n");
            foreach (var hit in searchResponse.Hits)
            {
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(hit.Source));
                Console.WriteLine(ElasticSearchHelper.SerializeToJson(hit.Source));
            }
            Console.WriteLine("\n=====================================================================================================");

            //Assert.Fail();
        }

        [Test()]
        public void UpdateDocumentByIdTest()
        {
            string id = "0157efad-a199-42fe-8fb1-37778965f561";
            string jsonData = "{\"id\":\"0157efad-a199-42fe-8fb1-37778965f561\",\"name\":\"中科大\",\"description\":\"中国\",\"dateTime\":\"2018-06-08T17:28:23.5846743+08:00\"}";
            Assert.AreEqual(ElasticSearchHelper.UpdateDocumentById<Student>(clientStudent, id, jsonData), true);
            //Assert.Fail();
        }

        [Test()]
        public void UpdateDocumentBySearchTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void DeleteDocumentByIdTest()
        {
            string id = "42fe0543-a650-49f3-bfac-25d90d924bb8";
            Assert.AreEqual(ElasticSearchHelper.DeleteDocumentById<Student>(clientStudent, id), true);
            //Assert.Fail();
        }

        [Test()]
        public void DeleteDocumentByIdTest1()
        {
            string indexName = "db_student";
            string typeName = "student";
            string id = "d7e02a3e-92bc-41bc-83d5-6659f7450dc7";
            Assert.AreEqual(ElasticSearchHelper.DeleteDocumentById<Student>(clientStudent, indexName, typeName, id), true);
            //Assert.Fail();
        }

        [Test()]
        public void DeleteDocumentByQueryTest()
        {
            //查询所有
            IDeleteByQueryRequest<Student> deleteByQuery = new DeleteByQueryRequest<Student>()
            {
                Query = new MatchAllQuery()
                {

                }
            };
            long count = 0;
            Assert.AreEqual(ElasticSearchHelper.DeleteDocumentByQuery<Student>(clientStudent, deleteByQuery, ref count), true);
            Console.WriteLine("删除的条数: " + count);
            //Assert.Fail();
        }

        [Test()]
        public void DeleteIndexTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void ImportDocumentsByFileTest()
        {
            string filePath = @"D:\student20180613.json";
            int count = 0;
            string indexName = "db_student";
            string typeName = "student";
            Assert.AreEqual(ElasticSearchHelper.ImportDocumentsByFile<Student>(clientStudent, filePath, indexName, typeName, ref count), true);
            Console.WriteLine("导入的条数: " + count);
            //Assert.Fail();
        }

        [Test()]
        public void ExportDocumentsToFileTest()
        {
            string filePath = @"D:\student20180613.json";
            SearchRequest<Student> searchRequest = new SearchRequest<Student>()
            {
                Query = new MatchAllQuery() { }
            };
            int count = 0;
            Assert.AreEqual(ElasticSearchHelper.ExportDocumentsToFile<Student>(clientStudent, filePath, searchRequest, ref count), true);
            Console.WriteLine("导出的条数: " + count);
            //Assert.Fail();
        }

        [Test()]
        public void SearchPartFilesDocumentsTest()
        {
            //返回部分字段FluentAPI写法
            //var searchRespPart = clientBank.Search<Account>(f => f
            //     .Query(q => q.Match(m => m.Field(fd => fd.lastname).Query("Skinner")))
            //     .Source(s => s.Includes(u => u.Fields("firstname", "lastname", "email"))));

            //var searchRes = clientBank.Search<Account>(s => s
            //      .Query(q => q.Match(m => m.Field(f => f.lastname).Query("Skinner"))));


            // 对象初始化写法

            ISourceFilter sourceFilter = new SourceFilter();
            sourceFilter.Includes = ((Fields)new string[] { "firstname", "lastname", "email" });       //返回指定的firstname,lastname,email字段
            // 查询检索条件
            var searchRequest = new SearchRequest<Account>()
            {
                Query = new MatchQuery()
                {
                    Field = "lastname",
                    Query = "Skinner"
                },
                Source = new Union<bool, ISourceFilter>(sourceFilter)
            };

            var searchResponse = ElasticSearchHelper.SearchPartFilesDocuments<Account>(clientBank, searchRequest);
            // 序列化过滤为null的字段
            var jSetting = new Newtonsoft.Json.JsonSerializerSettings { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore };

            foreach (var hit in searchResponse.Hits)
            {
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(hit.Source, jSetting));
            }
           
        }
    }
}