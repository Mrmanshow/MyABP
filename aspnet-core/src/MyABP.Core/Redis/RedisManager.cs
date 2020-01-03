using Abp.Domain.Services;
using ServiceStack.Model;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using StackExchange.Redis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MyABP.Redis
{
    public class RedisManager<T>: DomainService, IRedisManager<T>
    {
        //商业版
        private readonly IRedisClientsManager _redisClientsManager;
        //社区版本
        private readonly IConnectionMultiplexer _connectionMultiplexer;


        public RedisManager(IRedisClientsManager redisClientsManager,
            IConnectionMultiplexer connectionMultiplexer)
        {
            this._redisClientsManager = redisClientsManager;
            this._connectionMultiplexer = connectionMultiplexer;
        }

        public IList<T> GetList(string key)
        {
            using (IRedisClient redis = _redisClientsManager.GetClient())
            {
                var redisTodos = redis.As<T>();
                IHasNamed<IRedisList<T>> lists = redisTodos.Lists;

                IRedisList<T> list = lists[key];
                IList<T> result = list.GetRange(0, -1);
                return result;
            }
        }

        public void AddListItem(string key, T item)
        {
            using (IRedisClient redis = _redisClientsManager.GetClient())
            {
                var redisTodos = redis.As<T>();
                IHasNamed<IRedisList<T>> lists = redisTodos.Lists;

                IRedisList<T> list = lists[key];
                list.Push(item);
            }
        }


        public void SetValue(string key, T item)
        {
            using (IRedisClient redis = _redisClientsManager.GetClient())
            {
                var redisTodos = redis.As<T>();
                redisTodos.SetValue(key, item);
            }
        }


        public bool StringSet(RedisKey key, RedisValue value)
        {
            IDatabase redis = _connectionMultiplexer.GetDatabase();
            return redis.StringSet(key, value);
        }
    }
}
