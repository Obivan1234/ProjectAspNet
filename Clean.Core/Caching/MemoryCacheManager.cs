using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Core.Caching;
using System.Runtime.Caching;

namespace Clean.Core.Caching
{
    class MemoryCacheManager : ICacheManager
    {
        protected ObjectCache Cache
        {
            get => MemoryCache.Default;
        }

        public void Clear()
        {
            foreach (var item in Cache)
                Remove(item.Key);
        }

        public T Get<T>(string key)
        {
            return (T)Cache[key];
        }

        public bool IsSet(string key)
        {
            return Cache.Contains(key);
        }

        public void Remove(string key)
        {
            Cache.Remove(key);
        }

        public void Set(string key, object value, int cacheTime)
        {
            Cache.Add(key, value, DateTime.Now.AddMinutes(cacheTime));
        }
    }
}
