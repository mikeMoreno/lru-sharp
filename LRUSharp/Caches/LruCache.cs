using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Newtonsoft.Json;
using LRUSharp.Core.Caches;
using LRUSharp.Core.Models;

namespace LRUSharp.Caches
{
    public sealed class LruCache : Cache
    {
        private int MaxSize { get; }

        public LruCache(int maxSize = 4) : base()
        {
            MaxSize = maxSize;
        }

        public override bool Add(object[] args, object result)
        {
            string hash = GetHash(args);

            if (LastIndexCache.ContainsKey(hash))
            {
                return false;
            }

            if (ValueLists.Count >= MaxSize)
            {
                RemoveLeastRecentlyUsedEntry();
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

            if (lastIndex > 0)
            {
                MoveToFrontOfCache(entry);
            }
            else
            {
                Debug.WriteLine($"Hash was already the most recently used. Will not move: {entry.Hash}");
                Debug.WriteLine(
                    $"Current state:{Environment.NewLine}" +
                    $"Indices: {JsonConvert.SerializeObject(LastIndexCache)}{Environment.NewLine}" +
                    $"Cache: {JsonConvert.SerializeObject(ValueLists)}"
                );
            }

            Debug.WriteLine($"Returning existing entry: {entry.Hash}");

            return entry;
        }

        private void MoveToFrontOfCache(CacheEntry entry)
        {
            int lastIndex = LastIndexCache[entry.Hash];

            Debug.WriteLine($"Moving existing entry to front of cache: {entry.Hash}");

            ValueLists.RemoveAt(lastIndex);

            ValueLists.Insert(0, entry);

            UpdateIndices(entry.Hash);
        }

        private void RemoveLeastRecentlyUsedEntry()
        {
            var leastRecentlyUsedEntry = ValueLists[MaxSize - 1];

            Debug.WriteLine($"Removing least recently used entry: {leastRecentlyUsedEntry.Hash}");

            ValueLists.RemoveAt(MaxSize - 1);

            LastIndexCache.Remove(leastRecentlyUsedEntry.Hash);
        }
    }
}
