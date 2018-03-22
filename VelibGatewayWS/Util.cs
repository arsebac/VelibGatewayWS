using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace VelibGatewayWS
{
    class Util
    {
        private String Url
        {
            get
            {
                return ConfigurationManager.AppSettings["apiRoute"];
            }
        }
        private String ApiKey
        {
            get
            {
                return ConfigurationManager.AppSettings["apiKey"];
            }
        }
        public String ApiRequest(String endPoint)
        {
            string url = this.Url + endPoint;
            string sep = endPoint.Contains("?") ? "&" : "?";
            string apiTokens = "apiKey=" + ConfigurationManager.AppSettings["apiKey"];
            WebRequest request = WebRequest.Create(url + sep + apiTokens);
            Console.WriteLine(url + sep + apiTokens);
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            response.Close();
            return responseFromServer;
        }
    }
}
