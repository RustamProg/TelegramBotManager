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
            DataPresenter dataPresenter = new DataPresenter("https://api.hh.ru/vacancies/?", apiParams);
            Console.WriteLine(dataPresenter.AllJsonAttributes);
        }
    }
}
