using LRUSharp.Caches;
using LRUSharp.Core.Caches;
using System;
using System.Collections.Generic;
using System.Text;

namespace LRUSharp
{
    public static class LruCacheConfiguration
    {
        public static int MaxSize { get; set; } = 10;

        public static object GetInstance(Type aspectType)
        {
            if (aspectType == typeof(LruCacheAspect))
            {
                ICache cache;

                if (MaxSize > 0)
                {
                    cache = new LruCache(MaxSize);
                }
                else
                {
                    cache = new BoundlessCache();
                }

                return new LruCacheAspect(cache);
            }

            throw new ArgumentException($"Unknown aspect type '{aspectType.FullName}'");
        }
    }
}
