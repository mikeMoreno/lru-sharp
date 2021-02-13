using LRUSharp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LRUSharp.Core.Caches
{
    public interface ICache
    {
        bool IsCached(object[] args);

        bool Add(object[] args, object value);

        CacheEntry Get(object[] args);

    }
}
