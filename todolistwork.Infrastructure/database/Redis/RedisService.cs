using Newtonsoft.Json;
using StackExchange.Redis;
using todolistwork.Application.ICache;
using System;
using Microsoft.Extensions.Configuration;

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
            var address = configuration["SecretKeys:ApiKey"];
            ConfigurationOptions option = new ConfigurationOptions
            {
                AbortOnConnectFail = true,
                EndPoints = { address },
                Password = configuration["SecretKeys:ApiKey"]
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
    }
}
