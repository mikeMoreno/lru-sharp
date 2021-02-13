using System;
using System.Collections.Generic;
using System.Text;

namespace LRUSharp.Core.Models
{
    public sealed class CacheEntry
    {
        public string Hash { get; set; }
        public object Value { get; set; }
    }
}
