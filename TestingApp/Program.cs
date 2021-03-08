using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonAPIParser;
using Newtonsoft.Json.Linq;

namespace TestingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> apiParams = new Dictionary<string, string>()
            {
                {"experience", "between1And3" },
            };
            APIManager manager = new APIManager("https://api.hh.ru/vacancies/?", apiParams);
            Console.WriteLine(manager.GetDataInStringFormat());
            Console.WriteLine(manager.GetFullApiUrl());
            JObject jd = manager.GetDeserializedJson();
            Console.WriteLine(jd["items"][0]["name"].ToString());
            Console.ReadKey();
        }
    }
}
