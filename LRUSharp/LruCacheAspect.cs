using AspectInjector.Broker;
using LRUSharp.Caches;
using LRUSharp.Core.Caches;
using System;
using System.Collections.Generic;
using System.Text;

namespace LRUSharp
{
    [Aspect(Scope.PerInstance, Factory = typeof(LruCacheConfiguration))]
    public sealed class LruCacheAspect
    {
        ICache lruCache { get; }

        public LruCacheAspect(ICache cache)
        {
            lruCache = cache;
        }

        [Advice(Kind.Around, Targets = Target.Method)]
        public object BeforeMethodExecution([Argument(Source.Target)] Func<object[], object> methodDelegate, [Argument(Source.Arguments)] object[] args)
        {
            if (lruCache.IsCached(args))
            {
                var entry = lruCache.Get(args);

                return entry.Value;
            }

            return methodDelegate(args);
        }

        [Advice(Kind.After, Targets = Target.Method)]
        public void AfterMethodExecution([Argument(Source.Arguments)] object[] args, [Argument(Source.ReturnValue)] object returnValue)
        {
            if (!lruCache.IsCached(args))
            {
                lruCache.Add(args, returnValue);
            }
        }
    }
}
