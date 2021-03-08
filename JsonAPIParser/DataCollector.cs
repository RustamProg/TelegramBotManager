using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JsonAPIParser
{
    public class DataCollector
    {
        public string StreamReadData { get; private set; }
        public string FullApiUrl { get; private set; }
        public string SetAPIAndRead(string apiUrl, Dictionary<string, string> parameters)
        {
            FullApiUrl = apiUrl;

            foreach (var parameter in parameters)
            {
                FullApiUrl += parameter.Key + "=" + parameter.Value + "&";
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(FullApiUrl);
            request.UserAgent = ".NET Framework";

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return $"[ERROR] {response.StatusCode.ToString()} - {response.StatusDescription}";
                }

                Stream stream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(stream);

                StreamReadData = streamReader.ReadToEnd();
                response.Close();

                return StreamReadData;
            }
            catch (Exception responseException)
            {
                return $"[ERROR] {responseException.StackTrace} - {responseException.Message}";
            }                       
        }
    }
}
