using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonAPIParser
{
    public class DataPresenter
    {
        private APIManager _manager;
        private JObject _jsonData;
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
                    AllJsonAttributes.Add(item); // ошибка тут
                }
                catch (Exception){}               
            }
        }


    }
}
