using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonAPIParser
{
    /// <summary>
    /// Data presenter class
    /// </summary>
    public class DataPresenter
    {
        private APIManager _manager;
        private JObject _jsonData;
        /// <summary>
        /// All Json attributes got from API
        /// </summary>
        public List<string> AllJsonAttributes { get; private set; }
        public DataPresenter(string APIurl, Dictionary<string, string> apiParams)
        {
            _manager = new APIManager(APIurl, apiParams);
            _jsonData = _manager.GetDeserializedJson();
            Console.WriteLine(_jsonData["items"][0].ToString());
            foreach (var item in _jsonData["items"][0])
            {
                try
                {
                    AllJsonAttributes.Add("ihg"); // ошибка тут
                }
                catch (Exception){}               
            }
        }


    }
}
