using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Core.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);

        bool IsSet(string key);

        void Remove(string key);

        void Set(string key, object value, int cacheTime);

        void Clear();
    }
}
