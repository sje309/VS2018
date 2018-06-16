/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：ES5._6._4
*文件名称：ElasticSearchHelper
*创建人：  shuyizhi
*创建时间：2018/6/12 13:38:44
*文件描述: ES5.6.4版本的常见操作
* 基于ElasticSearch5.6.4常见操作
* NEST 6.0
* Nest.JsonNetSerializer 6.0
* Elasticsearch.Net    6.0
************************************************************************/

using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace ES5._6._4
{
    public class ElasticSearchHelper
    {
        /// <summary>
        /// 创建指定类型的索引
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="indexName">索引名</param>
        /// <returns>bool</returns>
        public static bool CreateIndexByType<T>( ElasticClient client, string indexName ) where T : class
        {
            if (string.IsNullOrEmpty(indexName))
            {
                throw new ArgumentNullException("参数错误:indexName为空");
            }
            var description = new CreateIndexDescriptor(indexName)
                     .Settings(s => s.NumberOfShards(5).NumberOfReplicas(1))            //设置分片数和副本数
                     .Mappings(ms => ms
                     .Map<T>(m => m.AutoMap()));

            var createResponse = client.CreateIndex(description);
            return createResponse.Acknowledged;
        }

        /// <summary>
        /// 批量插入指定的数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="client">client对象</param>
        /// <param name="datas">数据</param>
        /// <returns>bool</returns>
        public static bool InsertDocuments<T>( ElasticClient client, IEnumerable<T> datas ) where T : class
        {
            if (null == datas)
            {
                throw new ArgumentNullException("参数错误:datas为null");
            }
            IBulkResponse bulkResponse = client.IndexMany<T>(datas);
            return bulkResponse.IsValid;
        }

        /// <summary>
        /// 根据指定的条件检索数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="client">client对象</param>
        /// <param name="searchRequest">搜索请求条件</param>
        /// <returns>ISearchResponse<T></returns>
        public static ISearchResponse<T> SearchDocuments<T>( ElasticClient client, SearchRequest<T> searchRequest ) where T : class
        {
            if (null == searchRequest)
            {
                throw new ArgumentNullException("参数错误:查询条件searchRequest为null");
            }
            ISearchResponse<T> searchResponse = client.Search<T>(searchRequest);
            return searchResponse;
        }
        /// <summary>
        /// 根据指定的条件返回指定字段的数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="client">client对象</param>
        /// <param name="searchRequest">搜索请求条件</param>
        /// <returns>ISearchResponse</returns>
        public static ISearchResponse<T> SearchPartFilesDocuments<T>( ElasticClient client, SearchRequest<T> searchRequest ) where T : class
        {
            if (null == searchRequest)
            {
                throw new ArgumentNullException("参数错误:查询条件searchRequest为null");
            }
            ISearchResponse<T> searchResponse = client.Search<T>(searchRequest);
            return searchResponse;
        }

        /// <summary>
        /// 根据指定的id和json数据更新文档
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client"></param>
        /// <param name="id"></param>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        public static bool UpdateDocumentById<T>( ElasticClient client, string id, string jsonData ) where T : class
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(jsonData))
            {
                throw new ArgumentNullException("参数错误:id,jsonData为null");
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
            if (searchResponse.Documents.Count() == 0)
            {
                throw new Exception("根据id:" + id + "查询的数据不存在");
            }

            DocumentPath<T> document = new DocumentPath<T>(id);
            var updateResponse = client.Update<T>(document, f => f
               .Doc(Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonData)));
            return updateResponse.IsValid;
        }

        public static bool UpdateDocumentBySearch<T>( ElasticClient client, string id, string jsonData ) where T : class
        {
            //var updateResponse = client.UpdateByQuery<T>(f => f
            //     .Query(q => q
            //     .Term(t => t
            //     .Field(new Field("name")).Value("student1111"))),);

            IUpdateByQueryRequest updateByQuery = new UpdateByQueryRequest<T>("db_student", "student");
            updateByQuery.Query = new TermQuery()
            {
                Field = "name",
                Value = "student1111"
            };


            return false;
        }
        public static bool UpdatePartialDocument<T>( ElasticClient client, string indexName, string typeName, string id, string fileName, string fileValue ) where T : class, new()
        {
            IUpdateRequest<T, T> updateRequest = new UpdateRequest<T, T>(indexName, typeName, id);

            return false;
            //updateRequest.Script


            //client.Update<T>()
        }
        /// <summary>
        /// 根据指定的index\type\id删除文档数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="client">client对象</param>
        /// <param name="indexName">索引名</param>
        /// <param name="typeName">类型名</param>
        /// <param name="id">id</param>
        /// <returns>bool</returns>
        public static bool DeleteDocumentById<T>( ElasticClient client, string indexName, string typeName, string id ) where T : class
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("参数错误: id为null");
            }
            if (string.IsNullOrEmpty(indexName))
            {
                throw new ArgumentNullException("参数错误: indexName为null");
            }
            if (string.IsNullOrEmpty(typeName))
            {
                throw new ArgumentNullException("参数错误: typeName为null");
            }
            var existsResponse = client.DocumentExists(new DocumentExistsRequest<T>(indexName, typeName, id));      //根据indexName、typeName和id判断该文档是否存在
            if (!existsResponse.Exists)
            {
                throw new Exception("参数错误: " + id + "数据不存在");
            }
            DocumentPath<T> documentPath = new DocumentPath<T>(id);
            IDeleteResponse deleteResponse = client.Delete<T>(documentPath);
            return deleteResponse.IsValid;
        }

        /// <summary>
        /// 根据指定的id删除数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="client">client对象</param>
        /// <param name="id">id</param>
        /// <returns>bool</returns>
        public static bool DeleteDocumentById<T>( ElasticClient client, string id ) where T : class
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("参数错误:id为null");
            }
            //根据指定的id查找
            var searchRequest = new SearchRequest<T>()
            {
                Query = new TermQuery()
                {
                    Field = "_id",
                    Value = id
                }
            };
            var searchResponse = SearchDocuments<T>(client, searchRequest);
            if (searchResponse.Documents.Count() <= 0)
            {
                throw new Exception("参数错误: " + id + "数据不存在");
            }
            DocumentPath<T> documentPath = new DocumentPath<T>(id);
            IDeleteResponse deleteResponse = client.Delete(documentPath);
            return deleteResponse.IsValid;
        }
        /// <summary>
        /// 根据指定的查询条件删除数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="client">client对象</param>
        /// <param name="deleteByQueryRequest">删除请求条件</param>
        /// <param name="count">删除的条数</param>
        /// <returns>bool</returns>
        public static bool DeleteDocumentByQuery<T>( ElasticClient client, IDeleteByQueryRequest<T> deleteByQueryRequest,
            ref long count ) where T : class
        {
            IDeleteByQueryResponse deleteByQueryResponse = client.DeleteByQuery(deleteByQueryRequest);
            count = deleteByQueryResponse.Deleted;
            return deleteByQueryResponse.IsValid;
        }
        /// <summary>
        /// 删除指定的索引
        /// </summary>
        /// <param name="client">client对象</param>
        /// <param name="indexName">索引名</param>
        /// <returns>bool</returns>
        public static bool DeleteIndex( ElasticClient client, string indexName )
        {
            if (string.IsNullOrEmpty(indexName))
            {
                throw new ArgumentNullException("参数错误: " + indexName + "为null");
            }
            IExistsResponse existsResponse = client.IndexExists(indexName);
            if (!existsResponse.Exists)
            {
                throw new ArgumentNullException("参数错误： " + indexName + "索引不存在");
            }
            IDeleteIndexResponse deleteIndexResponse = client.DeleteIndex(indexName);
            return deleteIndexResponse.Acknowledged;
        }
        /// <summary>
        /// 批量导入指定type和index数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="client">client对象</param>
        /// <param name="filePath">导入的文件路径</param>
        /// <param name="indexName">index名称</param>
        /// <param name="typeName">type名称</param>
        /// <param name="count">导入的条数</param>
        /// <returns>bool</returns>
        public static bool ImportDocumentsByFile<T>( ElasticClient client, string filePath, string indexName, string typeName, ref int count ) where T : class
        {
            bool result = false;
            if (!string.IsNullOrEmpty(filePath))
            {
                if (File.Exists(filePath))
                {
                    string strLine = "";
                    count = 0;
                    using (StreamReader reader = new StreamReader(filePath, System.Text.Encoding.UTF8))
                    {
                        while ((strLine = reader.ReadLine()) != null)
                        {
                            T t = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(strLine);
                            var indexResponse = client.Index(t, s => s.Index(indexName).Type(typeName));
                            result = indexResponse.IsValid;
                            count++;
                        }
                    }
                }
                else
                {
                    throw new ArgumentNullException("参数错误: " + filePath + "指定的文件不存在");
                }
            }
            else
            {
                throw new ArgumentNullException("参数错误: " + filePath + "为null");
            }
            return result;
        }
        /// <summary>
        /// 根据查询结果导出
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="client">client对象</param>
        /// <param name="filePath">到导出的文件路径</param>
        /// <param name="searchRequest">搜索请求条件</param>
        /// <param name="count">导出的条数</param>
        /// <returns>bool</returns>
        public static bool ExportDocumentsToFile<T>( ElasticClient client, string filePath, SearchRequest<T> searchRequest, ref int count ) where T : class
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("参数错误: " + filePath + "为null");
            }
            var searchResponse = SearchDocuments<T>(client, searchRequest);
            count = 0;
            if (searchResponse.IsValid && searchResponse.Hits.Count() > 0)
            {
                var hits = searchResponse.Hits;
                using (StreamWriter writer = new StreamWriter(filePath, true, System.Text.Encoding.UTF8))
                {
                    foreach (var hit in hits)
                    {
                        writer.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(hit.Source));
                        count++;
                    }
                }
            }
            else
            {
                throw new Exception("错误: " + searchRequest + "查询不到结果");
            }
            return count > 0;
        }
        /// <summary>
        /// /Date(1417104000000)/时间格式序列化为yyyy-MM-dd HH:mm:ss形式
        /// </summary>
        /// <param name="data">序列化数据</param>
        /// <param name="DateTimeFormats">时间格式</param>
        /// <returns></returns>
        public static string SerializeToJson( object data, string DateTimeFormats = "yyyy-MM-dd HH:mm:ss" )
        {
            var timeConvert = new Newtonsoft.Json.Converters.IsoDateTimeConverter() { DateTimeFormat = DateTimeFormats };
            return Newtonsoft.Json.JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented, timeConvert);
        }
    }
}