using AspectInjector.Broker;
using System;

namespace LRUSharp
{
    [Injection(typeof(LruCacheAspect))]
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
    public sealed class LruCacheAttribute : Attribute
    {
    }
}