using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;

namespace WebEmpty
{
    public class Parser
    {
        private XmlDocument doc = new XmlDocument();
        public Parser(string rss_link)
        {
            doc.Load(@rss_link);
        }

        public static string filterTeks(string input)
        //Filter kata yg diawali '&' dan  diakhiri ';'
        {
            return Regex.Replace(input, "&.*?;", String.Empty);
        }

        public string parseHTML(string link)
        {

            HtmlWeb web = new HtmlWeb();
            var doc = web.Load(link);
            HtmlNodeCollection ourNode = doc.DocumentNode.SelectNodes("//div[@id = 'article-content']");
            string teks = "";
            if (ourNode != null)
            {
                foreach (HtmlNode node in ourNode)
                {
                    foreach (HtmlNode node2 in node.SelectNodes("//span/p"))
                    {
                        teks = teks + filterTeks(node2.InnerText);
                    }
                }
            }
            return teks;


        }

        public List<Tuple<string,string>> parseXML()
        {
           
            //doc.Load(@"https://www.tempo.co/rss/terkini");

            //doc.Load(@"http://rss.viva.co.id/get/all");

            //doc.Load(@"http://rss.detik.com/index.php/detikcom");
            //doc.Load(@"http://www.antaranews.com/rss/terkini"); 
            
            // Get elements
            List<Tuple<string, string>> titles = new List<Tuple<string,string>>();
            foreach (XmlNode node in doc.GetElementsByTagName("item"))
            {
                //Console.WriteLine("Count : {0}", node.ChildNodes);
                //Console.WriteLine("{0} Judul : {1}",i, node["title"].InnerText);
                titles.Add(new Tuple<string,string>(node["title"].InnerText,node["link"].InnerText));
                //i++;
                /* // Nulis semua identitas 
                foreach (XmlNode cnode in node.ChildNodes)
                {
                    Console.WriteLine("{0} : {1}", cnode.Name,cnode.InnerText);
                }*/
            }
            // Display the results
            return titles;
        }
    }
}