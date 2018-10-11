/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：Test3
*文件名称：HttpHelper
*创建人：  shuyizhi
*创建时间：2018/5/23 9:16:29
*文件描述:
************************************************************************/

using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace Test3
{
    public class HttpHelper
    {
        /// <summary>
        /// 获取HTTP Get方式响应内容
        /// </summary>
        /// <param name="url">远程地址</param>
        /// <returns>响应内容</returns>
        public static string GetHttpResponse( string url, bool isHotKey = false )
        {
            string jsonData = string.Empty;
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "GET";
                if (isHotKey)
                {
                    request.Timeout = 3 * 1000;
                }
                else
                {
                    request.Timeout = 30 * 1000;
                }

                request.KeepAlive = true;

                System.Net.ServicePointManager.DefaultConnectionLimit = 150;

                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader sRead = new StreamReader(stream, System.Text.Encoding.UTF8))
                    {
                        jsonData = sRead.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                //Utility.SysLog.WriteError("GetHttpResponse " + url, ex);
            }
            return jsonData;
        }

        /// <summary>
        /// 获取HTTP POST方式响应内容
        /// </summary>
        /// <param name="url">远程地址</param>
        /// <returns>响应内容</returns>
        public static string PostHttpResponse( string url, string parms )
        {
            string jsonData = string.Empty;
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Timeout = 30 * 1000;
                request.KeepAlive = true;

                byte[] byteParam = System.Text.Encoding.UTF8.GetBytes(parms);
                request.ContentLength = byteParam.Length;
                Stream writer = request.GetRequestStream();
                writer.Write(byteParam, 0, byteParam.Length);
                writer.Close();

                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader sRead = new StreamReader(stream, System.Text.Encoding.UTF8))
                    {
                        jsonData = sRead.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                //Utility.SysLog.WriteError("PostHttpResponse" + url, ex);
            }
            return jsonData;
        }

        /// <summary>
        /// 获取url主机部分
        /// 如 http://www.xxx.com/test/admin.aspx 返回 http://www.xxx.com/
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetWebHost( string url )
        {
            string host = string.Empty;
            Match mt = Regex.Match(url, @"http[s]*://[\S]*?/");
            if (mt != null && mt.Success)
                host = mt.Value;
            return host;
        }

        //public static string GetCurrentHost()
        //{
        //    string url = HttpContext.Current.Request.Url.ToString();
        //    return GetWebHost(url);
        //}

        #region Cookie处理

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        //public static void WriteCookie( string strName, string strValue )
        //{
        //    HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
        //    if (cookie == null)
        //    {
        //        cookie = new HttpCookie(strName);
        //    }
        //    cookie.Value = HttpUtility.UrlEncode(strValue, Encoding.UTF8);
        //    HttpContext.Current.Response.AppendCookie(cookie);

        //}

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="key">索引</param>
        /// <param name="strValue">值</param>
        //public static void WriteCookie( string strName, string key, string strValue )
        //{
        //    HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
        //    if (cookie == null)
        //    {
        //        cookie = new HttpCookie(strName);
        //    }
        //    cookie[key] = HttpUtility.UrlEncode(strValue, Encoding.UTF8);
        //    HttpContext.Current.Response.AppendCookie(cookie);

        //}
        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="strValue">过期时间(分钟)</param>
        //public static void WriteCookie( string strName, string strValue, int expires )
        //{
        //    HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
        //    if (cookie == null)
        //    {
        //        cookie = new HttpCookie(strName);
        //    }
        //    cookie.Value = HttpUtility.UrlEncode(strValue, Encoding.UTF8);
        //    cookie.Expires = DateTime.Now.AddMinutes(expires);
        //    HttpContext.Current.Response.AppendCookie(cookie);

        //}

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="strValue">过期时间(分钟)</param>
        /// <param name="strValue">cookie存在的域范围</param>
        //public static void WriteCookie(string strName, string strValue, int expires, string domain)
        //{
        //    HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
        //    if (cookie == null)
        //    {
        //        cookie = new HttpCookie(strName);
        //    }
        //    cookie.Value = HttpUtility.UrlEncode(strValue, Encoding.UTF8);
        //    cookie.Expires = DateTime.Now.AddMinutes(expires);
        //    cookie.Domain = domain;
        //    HttpContext.Current.Response.AppendCookie(cookie);

        //}
        /// <summary>
        ///写Cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="expires">过期时间（分钟）</param>
        /// <param name="path">虚拟路径</param>
        //public static void WriteCookie( string strName, string strValue, int expires, string path )
        //{
        //    HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
        //    if (cookie == null)
        //    {
        //        cookie = new HttpCookie(strName);
        //    }
        //    cookie.Value = HttpUtility.UrlEncode(strValue, Encoding.UTF8);
        //    cookie.Expires = DateTime.Now.AddMinutes(expires);
        //    cookie.Path = path;
        //    HttpContext.Current.Response.AppendCookie(cookie);

        //}

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        //public static string GetCookie( string strName )
        //{
        //    if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
        //    {
        //        //return  HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.Cookies[strName].Value.ToString());
        //        return HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[strName].Value.ToString(), Encoding.UTF8);
        //    }

        //    return "";
        //}
        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        //public static string GetCookie( string strName, string key )
        //{
        //    if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null && HttpContext.Current.Request.Cookies[strName][key] != null)
        //    {
        //        return HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[strName][key].ToString());
        //    }
        //    return "";
        //}

        ///// <summary>
        /// 删除Cookies
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="domain">域名</param>
        /// <returns></returns>
        //public static void DelCookie( string strName, string domain )
        //{
        //    HttpCookie cookie = new HttpCookie(strName);
        //    WriteCookie(strName, "", -60, domain);
        //}

        /// <summary>
        /// 删除Cookies
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns></returns>
        //public static void DelCookie( string strName )
        //{
        //    HttpCookie cookie = new HttpCookie(strName);
        //    WriteCookie(strName, "", -60);
        //}

        #endregion Cookie处理
    }
}