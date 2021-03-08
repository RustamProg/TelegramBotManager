using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonAPIParser
{
    // Main class for getting data from API
    internal class DataCollector
    {
        // Properties
        public string StreamReadData { get; private set; } // Data from stream property (string)
        public string FullApiUrl { get; private set; } // Full API URL string property

        // This method sets full api url from api url with parameters and returns string data from stream 
        public string SetAPIAndRead(string apiUrl, Dictionary<string, string> parameters)
        {
            // This block creating full api url
            FullApiUrl = apiUrl;

            foreach (var parameter in parameters)
            {
                FullApiUrl += parameter.Key + "=" + parameter.Value + "&";
            }

            // Creating HTTP request with User-Agent
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(FullApiUrl);
            request.UserAgent = ".NET Framework";

            // Trying to get response by api
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                // If response status code is not OK (= 200), returns error code with description
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return $"[ERROR] {response.StatusCode.ToString()} - {response.StatusDescription}";
                }

                // Creating string data using stream from HTTP API response
                Stream stream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(stream);

                StreamReadData = streamReader.ReadToEnd();
                response.Close();

                // Returning final data
                return StreamReadData;
            }
            // On got response exception return info about exception
            catch (Exception responseException)
            {
                return $"[ERROR] {responseException.StackTrace} - {responseException.Message}";
            }                       
        }

        // This method returns JObject with parsed from stream data
        public JObject GetJsonDeserializedData()
        {
            JObject jsonData = JObject.Parse(StreamReadData);
            return jsonData;
        }
    }
}
