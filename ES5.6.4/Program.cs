using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;


namespace ES5._6._4
{
    class Program
    {
        public static ElasticClient client = new ElasticClient(Settings.ConnectionSettings.DefaultIndex("db_student"));
        public static ElasticClient clientBank = new ElasticClient(Settings.ConnectionSettings.DefaultIndex("bank"));

        static void Main( string[] args )
        {
            //DeleteByQuery();
            QueryByPartFile();
            Console.ReadLine();
        }
        private static void DeleteByQuery()
        {
            var deleteResponse =
                client.DeleteByQuery<Student>(f => f
                    .Query(q => q
                    .MatchAll()));

            var datas = new List<Student>()
            {
                new Student(){ Id=Guid.NewGuid().ToString(),Name="student0011",DateTime=DateTime.Now, Description="student11student11"},
                new Student(){ Id=Guid.NewGuid().ToString(),Name="student0022", DateTime=DateTime.Now,Description="student22student22"}
            };

            var test = client.BulkAll<Student>(datas, s => s
                  .Index("db_student").Type("student"));



            Console.WriteLine("删除的条数: " + deleteResponse.Deleted);
        }
        private static void QueryByPartFile()
        {    
            //返回部分字段
            var searchRespPart = clientBank.Search<Account>(f => f
                  .Query(q => q.Match(m => m.Field(fd => fd.lastname).Query("Skinner")))
                  .Source(s => s.Includes(u => u.Fields("firstname", "lastname", "email"))));

            var searchResPartSortFileds = clientBank.Search<Account>(f => f
                  .Query(q => q
                  .Match(m => m.Field(fd => fd.lastname).Query("Skinner")))
                  .StoredFields(new string[] { "firstname", "lastname", "email" }));

            var ts = clientBank.Search<Account>(f => f
                    .Query(q => q
                         .Match(m => m.Field(fd => fd.lastname).Query("Skinner")))
                    .DocValueFields(e => e
                         .Field(fd => fd.lastname).Field(fd => fd.firstname).Field(fd => fd.email))
                    );
                



            //var searchRes = clientBank.Search<Account>(s => s
            //      .Query(q => q.Match(m => m.Field(f => f.lastname).Query("Skinner"))));



            //{\"account_number\":260,\"balance\":2726,\"firstname\":\"Kari\",\"lastname\":\"Skinner\",\"age\":30,\"gender\":\"F\",\"address\":\"735 Losee Terrace\",\"employer\":\"Singavera\",\"email\":\"kariskinner@singavera.com\",\"city\":\"Rushford\",\"state\":\"WV\"}

            ISourceFilter sourceFilter = new SourceFilter();
            sourceFilter.Includes = ((Fields)new string[] { "firstname", "lastname", "email" });
            sourceFilter.Excludes = ((Fields)new string[] { "account_number", "balance" });


            var searchRequest = new SearchRequest<Account>()
            {
                Query = new MatchQuery()
                {
                    Field = "lastname",
                    Query = "Skinner"
                },
                Source = new Union<bool, ISourceFilter>(sourceFilter)
            };

            var testRes = clientBank.Search<Account>(searchRequest);

            // 序列化过滤为null的字段
            var jSetting = new Newtonsoft.Json.JsonSerializerSettings { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore };
            Console.WriteLine("==========================================FluentAPI=====================================================\n");
            foreach (var hit in searchRespPart.Hits)
            {
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(hit.Source, jSetting));
            }

            Console.WriteLine("\n==========================================对象初始化=====================================================\n");
            foreach(var hit in testRes.Hits)
            {
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(hit.Source, jSetting));
            }
            Console.WriteLine("\n==========================================StoredFields=====================================================\n");
            foreach (var hit in searchResPartSortFileds.Hits)
            {
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(hit.Source, jSetting));
            }

        }
    }
}
