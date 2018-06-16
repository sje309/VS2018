using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class OrderController : ApiController
    {
       [HttpGet]
       public IHttpActionResult Get()
        {
            return Ok<string>("Success");
        }
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            return Ok<string>("Success" + id);
        }
        [HttpPost]
        public HttpResponseMessage PostData(int id )
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        [HttpPost]
        public HttpResponseMessage SaveData(Order order)
        {
            //return Request.CreateResponse();
            HttpResponseMessage message = new HttpResponseMessage();
            string s = Newtonsoft.Json.JsonConvert.SerializeObject(order);
            var content = new StringContent(s, System.Text.Encoding.UTF8, "appliction/json");
            message.Content = content;
            message.StatusCode = HttpStatusCode.OK;
            return message;

        }
        [HttpPut]
        public HttpResponseMessage Put(int id,Order order)
        {
            //return Ok();
            HttpResponseMessage message = new HttpResponseMessage();
            string s = Newtonsoft.Json.JsonConvert.SerializeObject(order);
            var content = new StringContent(s, System.Text.Encoding.UTF8, "appliction/json");
            message.Content = content;
            message.StatusCode = HttpStatusCode.OK;
            return message;
        }
        [HttpDelete]
        public IHttpActionResult DeleteById(int id )
        {
            return Ok();
        }
    }
}
