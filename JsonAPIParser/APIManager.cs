using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonAPIParser
{
    // Main class to interact with different APIs
    internal class APIManager
    {
        private string _requestURL;
        private Dictionary<string, string> _apiParams;

        private DataCollector _dataCollector;
        /// <summary>
        /// Manager class to provide access to API data
        /// </summary>
        /// <param name="APIurl">URL to api (must have backslash and ? at the end, e.g. www.api.com/?)</param>
        /// <param name="apiParams">Dictinary of parameters: <param name, value></param>
        public APIManager(string APIurl, Dictionary<string, string> apiParams)
        {
            _dataCollector = new DataCollector(APIurl, apiParams);
            _requestURL = APIurl;
            _apiParams = apiParams;
        }

        public string GetDataInStringFormat()
        {                        
            return _dataCollector.SetAPIAndRead(_requestURL, _apiParams);
        }

        public string GetFullApiUrl()
        {
            return _dataCollector.FullApiUrl;
        }

        public JObject GetDeserializedJson()
        {
            return _dataCollector.GetJsonDeserializedData(); ;
        }
    }
}
