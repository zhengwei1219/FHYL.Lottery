using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace OCAPIDemo
{
    public class api
    {
        public static string HttpGet(string url, Encoding enc)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 10000;//设置10秒超时
            request.Proxy = null;
            request.Method = "GET";
            request.ContentType = "application/x-www-from-urlencoded";
            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), enc);
            StringBuilder sb = new StringBuilder();
            sb.Append(reader.ReadToEnd());
            reader.Close();
            reader.Dispose();
            response.Close();
            return sb.ToString();
        }
    }
}
