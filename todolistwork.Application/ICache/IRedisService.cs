using System;

namespace todolistwork.Application.ICache
{
    public interface IRedisService
    {
        Task<IReadOnlyList<T>> GetAllDataHash<T>(string key);
        Task<T> GetDataByIdHash<T>(string key, string id);
        Task<bool> SetDataHash<T>(string key, string id, T value);
        Task<bool> DeleteDataHash (string key, string id);
        Task SetAllDataHash<T>(string key, IReadOnlyList<T> list);
        T GetData<T>(string key);
      
        bool SetData<T>(string key, T value, DateTimeOffset expirationTime);
        object RemoveData(string key);
    }
}
