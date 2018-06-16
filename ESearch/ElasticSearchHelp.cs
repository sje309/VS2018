/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：ESearch
*文件名称：ElasticSearchHelop
*创建人：  shuyizhi
*创建时间：2018/6/8 14:49:50
*文件描述: NEST ES C#客户端CRUD操作封装
* NEST 2.3.3
* Elasticsearch.Net 2.3.3
* 其中：DeleteAllTypesByIndex 根据查询的结果删除好像不支持，高版本支持
* 本实例中会报
* Invalid NEST response built from a unsuccessful low level call on DELETE: /db_student/student/_query
# Audit trail of this API call:
 - BadResponse: Node: http://localhost:9200/ Took: 00:00:00.5976587
# OriginalException: System.Net.WebException: 远程服务器返回错误: (404) 未找到。
   在 System.Net.HttpWebRequest.GetResponse()
   在 Elasticsearch.Net.HttpConnection.Request[TReturn](RequestData requestData) 位置 C:\Users\russ\source\elasticsearch-net-2.x\src\Elasticsearch.Net\Connection\HttpConnection.cs:行号 140
# Request:
<Request stream not captured or already read to completion by serializer. Set DisableDirectStreaming() on ConnectionSettings to force it to be set on the response.>
# Response:
<Response stream not captured or already read to completion by serializer. Set DisableDirectStreaming() on ConnectionSettings to force it to be set on the response.>
************************************************************************/

using Nest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ESearch
{
    public class ElasticSearchHelp
    {
        ///// <summary>
        ///// client连接
        ///// </summary>
        //public static ElasticClient client = new ElasticClient(Setting.ConnectionSettings);

        ///// <summary>
        ///// 根据指定类型创建索引
        ///// </summary>
        ///// <typeparam name="T">类型</typeparam>
        ///// <param name="indexName">索引名</param>
        ///// <returns></returns>
        //public static bool CreateIndex<T>( string indexName ) where T : class
        //{
        //    if (string.IsNullOrEmpty(indexName))
        //    {
        //        throw new ArgumentNullException("indexName为空");
        //    }
        //    var description = new CreateIndexDescriptor(indexName)
        //             .Settings(s => s.NumberOfShards(5).NumberOfReplicas(1))            //设置分片数和副本数
        //             .Mappings(ms => ms
        //             .Map<T>(m => m.AutoMap()));

        //    var createResponse = client.CreateIndex(description);
        //    return createResponse.Acknowledged;
        //}

        /// <summary>
        /// 批量插入指定的数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="client">client对象</param>
        /// <param name="datas">数据</param>
        /// <returns></returns>
        public static bool InsertDocuments<T>( ElasticClient client, IEnumerable<T> datas ) where T : class
        {
            if (null == datas || null == client)
            {
                throw new ArgumentNullException("参数错误，client或datas为null");
            }
            IBulkResponse bulkResponse = client.IndexMany<T>(datas);

            return bulkResponse.IsValid;
        }

        /// <summary>
        /// 根据指定的查询条件检索数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="client">client对象</param>
        /// <param name="searchRequest">搜索请求条件</param>
        /// <returns></returns>
        public static ISearchResponse<T> SearchDocuments<T>( ElasticClient client, SearchRequest<T> searchRequest ) where T : class
        {
            if (null == client || null == searchRequest)
            {
                throw new ArgumentNullException("参数错误，client或searchRequest为null");
            }
            var searchResponse = client.Search<T>(searchRequest);
            return searchResponse;
        }

        /// <summary>
        /// 根据指定的id和json数据更新文档
        /// </summary>
        /// <typeparam name="T">类型名称</typeparam>
        /// <param name="client">client对象</param>
        /// <param name="id">id</param>
        /// <param name="jsonData">要更新的数据</param>
        /// <returns></returns>
        public static bool UpdateDocumentById<T>( ElasticClient client, string id, string jsonData ) where T : class
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("参数错误,id为null");
            }
            //根据id查询一下
            var searchRequest = new SearchRequest<T>()
            {
                Query = new TermQuery()
                {
                    Field = "_id",
                    Value = id
                }
            };
            var searchResponse = SearchDocuments<T>(client, searchRequest);
            if (searchResponse.Documents.Count() > 0)
            {
                DocumentPath<T> document = new DocumentPath<T>(id);
                T t = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonData);

                var updateResponse = client.Update(document, f => f
                   .Doc(t));

                return updateResponse.IsValid;
            }
            else
            {
                throw new Exception("id：" + id + "数据不存在");
            }
        }

        //public static bool UpdateDocumentByQuery<T>( ElasticClient client ) where T : class
        //{
        //    var updateByQuery = new UpdateByQueryRequest("db_student", "student");

        //    client.UpdateByQuery<Student>("db_student", "student", f => f
        //          .Query(q => q
        //          .Match(m => m
        //          .Field(d => d.Name).Query("student1111"))));
        //    )

        //    return false;
        //}

        /// <summary>
        /// 根据Id删除对应的文档
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="client">client对象</param>
        /// <param name="id">id</param>
        /// <returns></returns>
        public static bool DeleteDocumentById<T>( ElasticClient client, string id ) where T : class
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("参数错误,id为null");
            }
            //根据id查询
            var searchRequest = new SearchRequest<T>()
            {
                Query = new TermQuery()
                {
                    Field = "_id",
                    Value = id
                }
            };
            var searchResponse = SearchDocuments<T>(client, searchRequest);
            if (searchResponse.Documents.Count() > 0)
            {
                DocumentPath<T> documentPath = new DocumentPath<T>(id);
                var delResponse = client.Delete<T>(documentPath);
                return delResponse.IsValid;
            }
            else
            {
                throw new Exception("id：" + id + "数据不存在");
            }
        }

        /// <summary>
        /// 删除索引，慎用！
        /// </summary>
        /// <param name="client">client对象</param>
        /// <param name="indexName">索引名称</param>
        /// <returns></returns>
        public static bool DeleteIndex( ElasticClient client, string indexName )
        {
            //判断一下索引是否存在
            var isExistResponse = client.IndexExists(indexName);
            if (isExistResponse.Exists)
            {
                var delResponse = client.DeleteIndex(indexName);
                return delResponse.IsValid;
            }
            else
            {
                throw new Exception("索引名: " + indexName + "不存在");
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client"></param>
        /// <param name="indexName"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static bool DeleteAllTypesByIndex<T>( ElasticClient client, string indexName, string typeName ) where T : class
        {
            var deleQueryRequest = new DeleteByQueryRequest<T>(indexName, typeName);     
            var mustClauses = new List<QueryContainer>();
            mustClauses.Add(new MatchAllQuery());

            deleQueryRequest.Query = new MatchAllQuery();
            //var delReponse = client.DeleteByQuery(deleQueryRequest);
            //return delReponse.IsValid;

            var delResponse = client.DeleteByQuery<T>(indexName, typeName,
                    f => f
                    .Query(q => q
                    .MatchAll()));
            return delResponse.IsValid;

        }

        /// <summary>
        /// 批量导入指定的JSON文件
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="client">client</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="count">导入的条数</param>
        /// <returns></returns>
        public static bool ImportByFileData<T>( ElasticClient client, string filePath, ref int count ) where T : class
        {
            bool result = false;
            if (!string.IsNullOrEmpty(filePath))
            {
                if (File.Exists(filePath))
                {
                    using (StreamReader reader = new StreamReader(filePath, System.Text.Encoding.UTF8))
                    {
                        string strLine = "";
                        count = 0;
                        while ((strLine = reader.ReadLine()) != null)
                        {
                            var indexResponse = client.Index<T>(Newtonsoft.Json.JsonConvert.DeserializeObject<T>(strLine));
                            result = indexResponse.Created;
                            count++;
                        }
                    }
                }
                else
                {
                    throw new FileNotFoundException("文件:" + filePath + "不存在!");
                }
            }
            else
            {
                throw new ArgumentNullException("参数错误: filePath为空!");
            }
            return result;
        }

        /// <summary>
        /// 导出满足指定搜索条件的数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="client">client对象</param>
        /// <param name="filePath">导出的文件路径</param>
        /// <param name="count">导出的文档数</param>
        /// <param name="searchRequest">搜索条件</param>
        /// <returns></returns>
        public static bool ExportFileBySearch<T>( ElasticClient client, string filePath, ref int count, SearchRequest<T> searchRequest ) where T : class
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                //if (File.Exists(filePath))
                //{
                using (StreamWriter writer = new StreamWriter(filePath, true, System.Text.Encoding.UTF8))
                {
                    var searchResponse = SearchDocuments<T>(client, searchRequest);
                    if (searchResponse.IsValid && searchResponse.Documents.Count() > 0)
                    {
                        count = 0;
                        foreach (var doc in searchResponse.Documents)
                        {
                            writer.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(doc));
                            count++;
                        }
                    }
                }
                //}
                //else
                //{
                //    throw new FileNotFoundException("文件:" + filePath + "不存在");
                //}
            }
            else
            {
                throw new ArgumentNullException("参数错误: filePath为空");
            }
            return count > 0;
        }
    }
}