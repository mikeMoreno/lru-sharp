using LRUSharp.Core.Caches;
using LRUSharp.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LRUSharp.Caches
{
    public sealed class BoundlessCache : Cache
    {
        public override bool Add(object[] args, object result)
        {
            string hash = GetHash(args);

            if (LastIndexCache.ContainsKey(hash))
            {
                return false;
            }

            Debug.WriteLine($"Adding args to front of cache: {JsonConvert.SerializeObject(args)}: {hash}");

            AddToFrontOfCache(hash, result);

            return true;
        }

        public override CacheEntry Get(object[] args)
        {
            string hash = GetHash(args);

            Debug.WriteLine($"Getting hash: {hash}");

            if (!LastIndexCache.ContainsKey(hash))
            {
                return null;
            }

            int lastIndex = LastIndexCache[hash];

            var entry = ValueLists[lastIndex];

            Debug.WriteLine($"Returning existing entry: {entry.Hash}");

            return entry;
        }
    }
}
