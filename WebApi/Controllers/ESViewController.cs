using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace WebApi.Controllers
{
    public class ESViewController : Controller
    {
        // GET: ESView
        public ActionResult Index()
        {
            return View();
        }

        // GET: ESView/Details/5
        public ActionResult Details( string esid )
        {
            Models.Model model = new Models.Model();
            if (!string.IsNullOrEmpty(esid))
            {
                //编辑

                var responseResult = WebApi.Controllers.ElasticSearchController.clientTekuan.Search<Models.Record>
                  (s => s
                 .Query(q => q
                 .Term(t => t
                 .Field("_id")
                 .Value(esid)
                 )));

               

                if (null != responseResult)
                {                 
                    foreach (var hit in responseResult.Hits)
                    {
                        model._id = hit.Id;
                        model._index = hit.Index;
                        model._score = (double)hit.Score;
                        model._source = hit.Source;
                    }
                }
                ViewBag.Messsage = model;
                ViewBag.username = model._source.username;
                ViewBag.identity_id = model._source.identity_id;
                ViewBag.phone_num = model._source.phone_num;
                ViewBag.timestamp = model._source.timestamp.ToString("yyyy-MM-dd hh:mm:ss");
                ViewBag.src_ip = model._source.src_ip;
                ViewBag.des_ip = model._source.des_ip;
                ViewBag.src_port = model._source.src_port;
                ViewBag.des_port = model._source.des_port;
                ViewBag.protocol_type = model._source.protocol_type;
                ViewBag.header = model._source.header;
                ViewBag.url = model._source.url;
                ViewBag.size = model._source.size;
                ViewBag.title = model._source.title.ToString();
                ViewBag.content = model._source.content.Trim();
                ViewBag.ICCID = model._source.iccid;
                ViewBag.flow_type = model._source.flow_type;

                // ViewBag.Message = "Hello" + esid;
            }
            //else
            //{
            //    //添加
            //}

            //return View();
            return View(model);
        }

        // GET: ESView/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ESView/Create
        [System.Web.Mvc.HttpPost]
        public ActionResult Create( FormCollection collection )
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ESView/Edit/5
        public ActionResult Edit( int id )
        {
            return View();
        }

        // POST: ESView/Edit/5
        [System.Web.Mvc.HttpPost]
        public ActionResult Edit( int id, FormCollection collection )
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ESView/Delete/5
        public ActionResult Delete( int id )
        {
            return View();
        }

        // POST: ESView/Delete/5
        [System.Web.Mvc.HttpPost]
        public ActionResult Delete( int id, FormCollection collection )
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public JsonResult GetDepartment( int limit, int offset, string departName, string status )
        {
            var lstRes = new List<WebApi.Models.Department>();
            for (int i = 0; i < 50; i++)
            {
                var model = new WebApi.Models.Department();
                model.ID = Guid.NewGuid().ToString();
                model.Name = "销售部" + (i + 1).ToString();
                model.Level = (i + 1).ToString();
                model.Description = "暂无描述信息";
                lstRes.Add(model);
            }
            var total = lstRes.Count;
            var rows = lstRes.Skip(offset).Take(limit).ToList();
            return Json(new { total, rows }, JsonRequestBehavior.AllowGet);
        }
        [System.Web.Mvc.HttpPost]
        public JsonResult GetESData( int limit, int offset, string key )
        {
            if (limit == 0 || limit > 10000)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.NotImplemented);
            }
            var responseResult = WebApi.Controllers.ElasticSearchController.clientTekuan
                .Search<WebApi.Models.Record>(s => s
                    .Size(limit)
                    .From(offset)
                    .Query(q => q
                        .Match(m => m
                        .Field(f => f.title).Query(key))
                        ||
                        q.Match(m => m
                        .Field(f => f.content).Query(key)))
                        );
            if (null != responseResult)
            {
                //var total = responseResult.Total;
                //var rows = responseResult.Documents.ToList();
                //return Json(new { total, rows }, JsonRequestBehavior.AllowGet);

                WebApi.Models.ESReturnModel eSReturnModel = new Models.ESReturnModel();
                eSReturnModel.Total = responseResult.Total;

                List<Models.Model> models = new List<Models.Model>();

                foreach (var hit in responseResult.Hits)
                {
                    Models.Model model = new Models.Model();
                    model._id = hit.Id;
                    model._index = hit.Index;
                    model._score = (double)hit.Score;
                    model._source = hit.Source;

                    models.Add(model);
                }
                eSReturnModel.Hits = models;

                var total = eSReturnModel.Total;
                var rows = eSReturnModel.Hits.ToList();

                return Json(new { total, rows });

            }
            return null;
        }

        public JsonResult GetDataByESID( string esid )
        {
            if (!string.IsNullOrEmpty(esid))
            {
                Models.Model model = new Models.Model();
                var responseResult = WebApi.Controllers.ElasticSearchController.clientTekuan.Search<Models.Record>
                   (s => s
                  .Query(q => q
                  .Term(t => t
                  .Field("_id")
                  .Value(esid)
                  )));

                if (null != responseResult)
                {
                    foreach (var hit in responseResult.Hits)
                    {
                        model._id = hit.Id;
                        model._index = hit.Index;
                        model._score = (double)hit.Score;
                        model._source = hit.Source;
                    }
                    return Json(new { model }, JsonRequestBehavior.AllowGet);
                }

            }
            return null;
        }

    }
}
