using ExchangeRate.dto;
using ExchangeRate.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.middleware
{
    internal class CheckJSONFilesForUser
    {
        public void CheckFileForUser(string path)
        {
            if (!File.Exists(path))
            {
                using var steam = File.Create(path);
                steam.Close();
            }
        }

        public void SaveJSONUser(List<User> list, string path)
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

        public List<User> LoadJSONUser(string path)
        {
            string jsonString = File.ReadAllText(path);
            List<User> loadingList = new();

            if (jsonString != null && jsonString.Length != 0)
            {
                loadingList = JsonConvert.DeserializeObject<List<User>>(jsonString, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    NullValueHandling = NullValueHandling.Ignore,
                })!;
            }

            return loadingList;
        }
    }
}
