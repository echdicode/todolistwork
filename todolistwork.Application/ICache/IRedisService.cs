using System;

namespace todolistwork.Application.ICache
{
    public interface IRedisService
    {
        T GetData<T>(string key);
      
        bool SetData<T>(string key, T value, DateTimeOffset expirationTime);
        object RemoveData(string key);
    }
}
