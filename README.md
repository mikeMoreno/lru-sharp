# lru-sharp

An LRU (least recently used) cache inspired by Python's `lru_cache` [decorator](https://docs.python.org/3/library/functools.html#functools.lru_cache).

Simply add an `LruCache` attribute on to the property or method you want to cache.

```
[LruCache]
public static int Add(int x, int y)
{
    return x + y;
}
```

If the passed parameters have been previously cached, the method won't execute, and the cached value will be returned instead.

Like Python's `lru_cache`, the method's parameters must be hashable in order for the LRU cache to work.

---

The maximum number of items the cache can hold may be specified like so:

```
LruCacheConfiguration.MaxSize = 50;
```

If `MaxSize` is set to a number less than 1, the cache will no longer be an LRU cache and will have no maximum size.
