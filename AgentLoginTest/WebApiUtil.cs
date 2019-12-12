using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace AgentLoginTest
{
    public class WebApiUtil
    {
       
        public static bool HttpPost(string url, Dictionary<string, string> paramList, out string Msg)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            bool _flag = false;
            HttpWebRequest req = WebRequest.Create(new Uri(url)) as HttpWebRequest;
            req.Method = "POST";
            req.ContentType = "application/json";

            string jsonString = paramzToJson(paramList);

            byte[] formData = UTF8Encoding.UTF8.GetBytes(jsonString);
            req.ContentLength = formData.Length;

            using (Stream post = req.GetRequestStream())
            {
                post.Write(formData, 0, formData.Length);
                post.Flush();
            }

            using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
            {
                if (resp.StatusCode == HttpStatusCode.OK)
                {
                    _flag = true;
                }
                StreamReader reader = new StreamReader(resp.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
                Msg = reader.ReadToEnd();
            }
            return _flag;
        }

        private static string paramzToJson(IDictionary<string, string> paramList)
        {
            StringBuilder paramz = new StringBuilder();
            paramz.Append("{");
            foreach (KeyValuePair<string, string> param in paramList)
            {
                if (paramz.Length > 1)
                    paramz.Append(" ,");
                paramz.Append(String.Format("\"{0}\"", param.Key));
                paramz.Append(" : ");
                paramz.Append(String.Format("\"{0}\"", System.Web.HttpUtility.UrlEncode(param.Value)));
            }
            paramz.Append("}");
            return paramz.ToString();
        }

    }

}
