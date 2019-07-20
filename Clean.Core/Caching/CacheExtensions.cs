using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Core.Caching
{
    public static class CacheExtensions
    {
        public static readonly object _loackObj = new object();

        public static T Get<T>(this ICacheManager cacheManager, string key, Func<T> aquire)
        {
            return Get<T>(cacheManager, key, 60, aquire);
        }

        private static T Get<T>(this ICacheManager cacheManager, string key, int cacheTime, Func<T> aquire)
        {
            if(cacheManager.IsSet(key))
            {
                return cacheManager.Get<T>(key);
            }

            lock (_loackObj)
            {
                if (cacheManager.IsSet(key))
                {
                    return cacheManager.Get<T>(key);
                }

                var result = aquire();

                cacheManager.Set(key, result, cacheTime);

                return result;
            }
        }
    }
}
