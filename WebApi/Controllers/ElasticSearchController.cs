using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Nest;
using Newtonsoft.Json;
using WebApi.Commons;
using WebApi.Models;
using System.Web.Mvc;

/// <summary>
/// ES查询提供接口
/// </summary>

namespace WebApi.Controllers
{
    public class ElasticSearchController : ApiController
    {
        public static ElasticClient client = new ElasticClient(Setting.ConnectionSettings);
        public static ElasticClient clientStudent = new ElasticClient(Setting.ConnectionSettings.DefaultIndex("db_student"));
        public static ElasticClient clientTekuan = new ElasticClient(Setting.ConnectionSettings.DefaultIndex("tekuan"));


        // api/elasticsearch
        public HttpResponseMessage GetAll()
        {
            var responseResult = clientTekuan
                .Search<Record>(s => s
                  .Size(10)
                  .Query(q => q
                  .MatchAll()));

            ReturnModel model = new ReturnModel();
            if (null != responseResult)
            {
                model.datas = responseResult.Documents;
                model.Took = responseResult.Took;
                model.Total = responseResult.Total;              
            }
            HttpResponseMessage message = new HttpResponseMessage();
            var content=JsonConvert.SerializeObject(model);
            message.Content = new StringContent(content, System.Text.Encoding.UTF8, "application/json");
            return message;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.ActionName(name:"GetByCount")]
        public HttpResponseMessage Get(int limit,int offset )
        {
            if (limit == 0 || limit > 10000)
            {
                throw new HttpResponseException(HttpStatusCode.NotImplemented);
            }
            var responseResult = clientTekuan
                                .Search<Record>(s => s
                                .Size(limit)
                                .From(offset)
                                .Query(q => q
                                .MatchAll()));
            ReturnModel model = new ReturnModel();
            if (null != responseResult)
            {
                model.datas = responseResult.Documents;
                model.Took = responseResult.Took;
                model.Total = responseResult.Total;
            }
            HttpResponseMessage message = new HttpResponseMessage();
            var content = JsonConvert.SerializeObject(model);
            message.Content = new StringContent(content, System.Text.Encoding.UTF8, "application/json");
            return message;

        }
       
    }
}
