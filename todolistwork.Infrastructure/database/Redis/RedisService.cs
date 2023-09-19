using Newtonsoft.Json;
using StackExchange.Redis;
using todolistwork.Application.ICache;
using Microsoft.Extensions.Configuration;
using todolistwork.Core.Unit;
using todolistwork.Core.Entities;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;

namespace todolistwork.Infrastructure.database.Redis
{
    public class RedisService : IRedisService
    {
        private static ConnectionMultiplexer _redis = null;

        private static ConnectionMultiplexer[] _redisPools = new ConnectionMultiplexer[4];

        private readonly IConfiguration configuration;
        private static volatile int _currentRedis = 0;
        private  void ConfigureRedis()
        {
            var address = "127.0.0.1:6379";
            ConfigurationOptions option = new ConfigurationOptions
            {
              //  AbortOnConnectFail = true,
                EndPoints = { address },
              //  Password = configuration["SecretKeys:ApiKey"]
            };
            for (int i = 0; i < _redisPools.Length; i++)
            {
                _redisPools[i] = ConnectionMultiplexer.Connect(option);
            }
            _redis = _redisPools[_redisPools.Length - 1];
        }
        public RedisService(IConfiguration configuration)
        {
            this.configuration = configuration;
            ConfigureRedis();
        }
        public static IDatabase GetRedis(int db = -1)
        {
            int cR = Interlocked.Increment(ref _currentRedis);
            if (cR < 0)
            {
                cR = 0;
                Interlocked.Exchange(ref _currentRedis, 0);
            }
            var redis = _redisPools[cR % _redisPools.Length];
            return redis.GetDatabase(db);
        }
        public async Task <IReadOnlyList<T>> GetAllDataHash<T>(string key)
        {
            {
                var hashEntries = GetRedis().HashGetAll(key);

                var results = hashEntries.Select(hashEntry =>
                {
                    var value = hashEntry.Value.ToString();

                    // Chuyển đổi chuỗi JSON thành đối tượng Result
                    var result = JsonConvert.DeserializeObject<T>(value);

                    return result;
                }).ToList();

                return results.AsReadOnly();
            }

        }
        public async Task<T?> GetDataByIdHash<T>(string key,string id)
        {
            {

                var value = GetRedis().HashGet(key, id);
                if (value.IsNull)
                {
                    return default;
                }
                var result = JsonConvert.DeserializeObject<T>(value);
                return result;
            }

        }
        public async Task<bool> SetDataHash<T>(string key,string id,  T value)
        {
            {
                string data = await ResultSerialize(value);
                bool iSet =  GetRedis().HashSet(key, id, data);
                return iSet;
            }

        }
        public async Task SetAllDataHash<T>(string key, IReadOnlyList<T> list)
        {
            {
                HashEntry[] hashEntries = list
                    .Select((item, index) => new HashEntry(index.ToString(), item.ToString()))
                    .ToArray();

                GetRedis().HashSet(key, hashEntries);
                
            }

        }
        public async Task<bool> DeleteDataHash(string key, string id)
        {
            {
                bool iSet = GetRedis().HashDelete(key, id);
                return iSet;
            }

        }
        public T GetData<T>(string key)
        {
            var value = GetRedis().StringGet(key);
            if (!string.IsNullOrEmpty(value))
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            return default;
        }
        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            TimeSpan expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
            var isSet = GetRedis().StringSet(key, JsonConvert.SerializeObject(value), expiryTime);
            return isSet;
        }
        public object RemoveData(string key)
        {
            bool _isKeyExist = GetRedis().KeyExists(key);
            if (_isKeyExist == true)
            {
                return GetRedis().KeyDelete(key);
            }
            return false;
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
    }
}
