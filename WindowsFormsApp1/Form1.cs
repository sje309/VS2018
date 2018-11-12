using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.webBrowser1.ScriptErrorsSuppressed = true;
        }

        private void button1_Click( object sender, EventArgs e )
        {
            webBrowser1.Navigate(this.textBox1.Text.Trim());
        }

        private void webBrowser1_DocumentCompleted( object sender, WebBrowserDocumentCompletedEventArgs e )
        {
            try
            {
                WebBrowser wb = (WebBrowser)sender;
                if (wb.ReadyState != WebBrowserReadyState.Complete)
                {
                    return;
                }
                if (e.Url != wb.Url)
                {
                    return;
                }
                if (wb.Url.ToString() == textBox1.Text.Trim())
                {
                    wb.DocumentCompleted -= new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// 获取店铺
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click( object sender, EventArgs e )
        {
            try
            {
                HtmlDocument document = webBrowser1.Document;
                if (document != null)
                {
                    //获取页面所有的div
                    HtmlElementCollection divs = document.GetElementsByTagName("div");
                    if (null != divs && divs.Count > 0)
                    {
                        string storeNum = string.Empty;
                        string storeLocation = string.Empty;
                        string storeTime = string.Empty;
                        string follows = string.Empty;
                       
                        foreach(HtmlElement el in divs)
                        {
                            //店铺信息div css
                            string cssName = "store-info-header util-clearfix";
                            if (el.GetAttribute("className").Equals(cssName, StringComparison.OrdinalIgnoreCase))
                            {
                                NSoup.Nodes.Document doc = NSoup.NSoupClient.Parse(el.InnerHtml);
                                storeNum = doc.Select(".store-number").Text;
                                storeLocation = doc.Select(".store-location").Text;
                                storeTime = doc.Select(".store-time em").Text;
                                follows = doc.Select(".positive-feedback a").Text;

                                MessageBox.Show(storeNum+"\n"+storeLocation+"\n"+storeTime+"\n"+follows);
                            }
                            //产品列表信息
                            string prodListCss = "ui-box-body";
                            if (el.GetAttribute("className").Equals(prodListCss, StringComparison.OrdinalIgnoreCase))
                            {
                                NSoup.Nodes.Document doc = NSoup.NSoupClient.Parse(el.InnerHtml);
                                NSoup.Select.Elements elements = doc.Select(".item");
                                foreach(var element in elements)
                                {
                                    string prodName = element.Select(".detail h3 a").Attr("title");
                                    string prodPrice = element.Select(".cost b").Text;
                                    string prodOrders = element.Select(".recent-order").Text;
                                    string prodImg = element.Select(".pic img").Attr("src");
                                    string prodUrl = element.Select(".pic a").Attr("href");
                                    Console.WriteLine("=====================产品信息=========================");
                                    Console.WriteLine("产品名称: " + prodName);
                                    Console.WriteLine("产品价格: " + prodPrice);
                                    Console.WriteLine("产品订单数: " + prodOrders);
                                    Console.WriteLine("产品图片: " + prodImg);
                                    Console.WriteLine("产品详情页: " + prodUrl);
                                }
                            }
                        }
                        webBrowser1.Navigate("http://www.baidu.com");
                       
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
