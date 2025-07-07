using ExchangeRate.dto;
using ExchangeRate.model;
using ExchangeRate.repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.middleware
{
    internal class CheckJSONFilesForCurrencies
    {
        public void CheckFileForCurrencies(string path)
        {
            if (!File.Exists(path))
            {
                using var steam = File.Create(path);
                steam.Close();
            }
        }

        public void SaveJSONCurrency(ExchangeRateAllCurrencies list, string path)
        {
            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new();
                serializer.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                serializer.NullValueHandling = NullValueHandling.Ignore;
                serializer.TypeNameHandling = TypeNameHandling.Auto;
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, list);
                file.Close();
            }
        }

        public ExchangeRateAllCurrencies LoadJSONCurrency(string path)
        {
            string jsonString = File.ReadAllText(path);
            ExchangeRateAllCurrencies loadingList = new();

            if(jsonString != null && jsonString.Length != 0)
            {
                loadingList = JsonConvert.DeserializeObject<ExchangeRateAllCurrencies>(jsonString, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    NullValueHandling = NullValueHandling.Ignore,
                })!;
            }

            return loadingList;
        }
    }
}
