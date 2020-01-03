using Abp.Domain.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyABP.Redis
{
    public interface IRedisManager<T> : IDomainService
    {
        IList<T> GetList(string key);

        void AddListItem(string key, T item);

        bool StringSet(RedisKey key, RedisValue value);

        void SetValue(string key, T item);
    }
}
