using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using todolistwork.Core.Entities;
using System.Collections;

namespace todolistwork.Core.Unit
{
    public static class UnitCore
    {
        public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1);

        public static string GetTimestamp(string time)
        {
            var t = DateTime.Parse(time);
            return ((t.Subtract(UnixEpoch)).TotalMilliseconds).ToString();
        }
        public static string GetTimestamp(DateTime time)
        {
            return ((time.Subtract(UnixEpoch)).TotalMilliseconds).ToString();
        }
        public static DateTime GetDateTime(string timestamp)
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(long.Parse(timestamp));
            return dateTimeOffset.LocalDateTime;
        }
      

        public static  string HashMd5(string input)
        {
            // Use input string to calculate MD5 hash
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("x2"));
            }
            return sb.ToString();

        }
        public static  string Random()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string randomString = new string(Enumerable.Repeat(chars, 6)
                                                      .Select(s => s[random.Next(s.Length)])
                                                      .ToArray());
            return randomString;

        }
        public static string ResultSerialize<T>(T data)
        {
            try {
                var settings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
                    StringEscapeHandling = StringEscapeHandling.EscapeNonAscii
                };
                string json = JsonConvert.SerializeObject(data, settings);
                return json;
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                return null;
            }
    

        }
        /*  public async Task<object> ListResultSerialize(object data)
          {
              var settings = new JsonSerializerSettings
              {
                  Formatting = Formatting.Indented,
                  ContractResolver = new CamelCasePropertyNamesContractResolver(),
                  NullValueHandling = NullValueHandling.Ignore,
                  DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
                  StringEscapeHandling = StringEscapeHandling.EscapeNonAscii
              };
              string json = JsonConvert.SerializeObject(data, settings);
           //   List<Dictionary<string, object>> list =  JArray.Parse(json).ToObject<List<Dictionary<string, object>>>();
              return json;

          }
          public async Task<string> ResultSerialize(object data)
          {
              var settings = new JsonSerializerSettings
              {
                  Formatting = Formatting.Indented,
                  ContractResolver = new CamelCasePropertyNamesContractResolver(),
                  NullValueHandling = NullValueHandling.Ignore,
                  DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
                  StringEscapeHandling = StringEscapeHandling.EscapeNonAscii
              };
              string json = JsonConvert.SerializeObject(data, settings);
              Console.WriteLine(json);
              Dictionary<string, object> jObject = JObject.Parse(json).ToObject<Dictionary<string, object>>();
              return json;

          }
  */


    }
}
