using LRUSharp.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LRUSharp.Core.Caches
{
    public abstract class Cache : ICache
    {
        protected Dictionary<string, int> LastIndexCache { get; }

        protected List<CacheEntry> ValueLists { get; set; }

        public Cache()
        {
            LastIndexCache = new Dictionary<string, int>();
            ValueLists = new List<CacheEntry>();
        }

        public bool IsCached(object[] args)
        {
            string hash = GetHash(args);

            return LastIndexCache.ContainsKey(hash);
        }

        public virtual bool Add(object[] args, object value)
        {
            throw new NotImplementedException();
        }

        public virtual CacheEntry Get(object[] args)
        {
            throw new NotImplementedException();
        }

        protected void AddToFrontOfCache(string hash, object value)
        {
            var newEntry = new CacheEntry()
            {
                Hash = hash,
                Value = value
            };

            Debug.WriteLine($"Adding new entry to front of cache: {hash}");

            LastIndexCache.Add(hash, 0);

            ValueLists.Insert(0, newEntry);

            UpdateIndices(hash);
        }

        protected void UpdateIndices(string lastAddedHash)
        {
            Debug.WriteLine($"Updating indices. Most recently used entry: {lastAddedHash}");

            int index = 0;

            foreach (var entry in ValueLists)
            {
                LastIndexCache[entry.Hash] = index;

                index++;
            }

            Debug.WriteLine(
                $"Indices updated. Current state:{Environment.NewLine}" +
                $"Indices: {JsonConvert.SerializeObject(LastIndexCache)}{Environment.NewLine}" +
                $"Cache: {JsonConvert.SerializeObject(ValueLists)}"
            );
        }

        protected string GetHash(object[] args)
        {
            if (args == null)
            {
                return null;
            }

            var hashBuilder = new StringBuilder();

            foreach (var arg in args)
            {
                if (arg == null)
                {
                    hashBuilder.Append("<null>");
                }
                else
                {
                    hashBuilder.Append(arg.GetHashCode().ToString());
                }
            }

            return CreateMD5(hashBuilder.ToString());
        }

        /// <summary>
        /// Calculates an MD5 hash from a string.
        /// https://stackoverflow.com/questions/11454004/calculate-a-md5-hash-from-a-string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                return sb.ToString();
            }
        }
    }
}
