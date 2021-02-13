using LRUSharp.Caches;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LRUSharp.Tests
{
    public class LruMechanismTests
    {
        [Fact]
        public void MissingHashReturnsNull()
        {
            var lruCache = new LruCache();

            var args = new object[] { 1, 1 };

            var entry = lruCache.Get(args);

            Assert.Null(entry);
        }

        [Fact]
        public void FoundHashReturnsNotNull()
        {
            var lruCache = new LruCache();

            var args = new object[] { 1, 1 };

            lruCache.Add(args, 1 + 1);

            var entry = lruCache.Get(args);

            Assert.NotNull(entry);
            Assert.Equal(2, entry.Value);
        }

        [Fact]
        public void IsCachedFindsAddedItem()
        {
            var lruCache = new LruCache();

            var args = new object[] { 1, 1 };

            lruCache.Add(args, 1 + 1);

            Assert.True(lruCache.IsCached(args));
        }

        [Fact]
        public void IsCachedDoesNotFindMissingItem()
        {
            var lruCache = new LruCache();

            var args = new object[] { 1, 1 };

            lruCache.Add(args, 1 + 1);

            var missingArgs = new object[] { 1, 2 };

            Assert.False(lruCache.IsCached(missingArgs));
        }

        [Fact]
        public void SameHashDoesNotAffectCacheEjection()
        {
            const int MaxSize = 4;

            var lruCache = new LruCache();

            var args = new object[] { 1, 1 };

            bool firstAdd = lruCache.Add(args, 1 + 1);

            Assert.True(firstAdd);

            for (int i = 0; i < MaxSize + 1; i++)
            {
                bool subsequentAdd = lruCache.Add(args, 1 + 1);

                Assert.False(subsequentAdd);
            }

            var entry = lruCache.Get(args);

            Assert.NotNull(entry);
            Assert.Equal(2, entry.Value);
        }

        [Fact]
        public void LeastRecentlyUsedItemKeptWhenMaxNotExceeded()
        {
            var lruCache = new LruCache();

            var args1 = new object[] { 1, 1 };
            var args2 = new object[] { 1, 2 };
            var args3 = new object[] { 1, 3 };
            var args4 = new object[] { 1, 4 };

            lruCache.Add(args1, 1 + 1);
            lruCache.Add(args2, 1 + 2);
            lruCache.Add(args3, 1 + 3);
            lruCache.Add(args4, 1 + 4);

            Assert.NotNull(lruCache.Get(args1));
        }

        [Fact]
        public void LeastRecentlyUsedItemEjectedWhenMaxExceeded()
        {
            var lruCache = new LruCache();

            var args1 = new object[] { 1, 1 };
            var args2 = new object[] { 1, 2 };
            var args3 = new object[] { 1, 3 };
            var args4 = new object[] { 1, 4 };
            var args5 = new object[] { 1, 5 };

            lruCache.Add(args1, 1 + 1);
            lruCache.Add(args2, 1 + 2);
            lruCache.Add(args3, 1 + 3);
            lruCache.Add(args4, 1 + 4);
            lruCache.Add(args5, 1 + 5);

            Assert.Null(lruCache.Get(args1));
        }

        [Fact]
        public void MoveArgs1ToLeastRecentlyUsedAndSeeEjectedAscending()
        {
            var lruCache = new LruCache();

            var args1 = new object[] { 1, 1 };
            var args2 = new object[] { 1, 2 };
            var args3 = new object[] { 1, 3 };
            var args4 = new object[] { 1, 4 };
            var args5 = new object[] { 1, 5 };

            lruCache.Add(args1, 1 + 1);
            lruCache.Add(args2, 1 + 2);
            lruCache.Add(args3, 1 + 3);
            lruCache.Add(args4, 1 + 4);

            _ = lruCache.Get(args2);
            _ = lruCache.Get(args3);
            _ = lruCache.Get(args4);

            lruCache.Add(args5, 1 + 5);

            Assert.Null(lruCache.Get(args1));
        }

        [Fact]
        public void MoveArgs1ToLeastRecentlyUsedAndSeeEjectedDescending()
        {
            var lruCache = new LruCache();

            var args1 = new object[] { 1, 1 };
            var args2 = new object[] { 1, 2 };
            var args3 = new object[] { 1, 3 };
            var args4 = new object[] { 1, 4 };
            var args5 = new object[] { 1, 5 };

            lruCache.Add(args1, 1 + 1);
            lruCache.Add(args2, 1 + 2);
            lruCache.Add(args3, 1 + 3);
            lruCache.Add(args4, 1 + 4);

            _ = lruCache.Get(args4);
            _ = lruCache.Get(args3);
            _ = lruCache.Get(args2);

            lruCache.Add(args5, 1 + 5);

            Assert.Null(lruCache.Get(args1));
        }

        [Fact]
        public void MoveArgs2ToLeastRecentlyUsedAndSeeEjectedAscending()
        {
            var lruCache = new LruCache();

            var args1 = new object[] { 1, 1 };
            var args2 = new object[] { 1, 2 };
            var args3 = new object[] { 1, 3 };
            var args4 = new object[] { 1, 4 };
            var args5 = new object[] { 1, 5 };

            lruCache.Add(args1, 1 + 1);
            lruCache.Add(args2, 1 + 2);
            lruCache.Add(args3, 1 + 3);
            lruCache.Add(args4, 1 + 4);

            _ = lruCache.Get(args1);
            _ = lruCache.Get(args3);
            _ = lruCache.Get(args4);

            lruCache.Add(args5, 1 + 5);

            Assert.Null(lruCache.Get(args2));
        }

        [Fact]
        public void MoveArgs2ToLeastRecentlyUsedAndSeeEjectedDescending()
        {
            var lruCache = new LruCache();

            var args1 = new object[] { 1, 1 };
            var args2 = new object[] { 1, 2 };
            var args3 = new object[] { 1, 3 };
            var args4 = new object[] { 1, 4 };
            var args5 = new object[] { 1, 5 };

            lruCache.Add(args1, 1 + 1);
            lruCache.Add(args2, 1 + 2);
            lruCache.Add(args3, 1 + 3);
            lruCache.Add(args4, 1 + 4);

            _ = lruCache.Get(args4);
            _ = lruCache.Get(args3);
            _ = lruCache.Get(args1);

            lruCache.Add(args5, 1 + 5);

            Assert.Null(lruCache.Get(args2));
        }

        [Fact]
        public void MoveArgs3ToLeastRecentlyUsedAndSeeEjectedAscending()
        {
            var lruCache = new LruCache();

            var args1 = new object[] { 1, 1 };
            var args2 = new object[] { 1, 2 };
            var args3 = new object[] { 1, 3 };
            var args4 = new object[] { 1, 4 };
            var args5 = new object[] { 1, 5 };

            lruCache.Add(args1, 1 + 1);
            lruCache.Add(args2, 1 + 2);
            lruCache.Add(args3, 1 + 3);
            lruCache.Add(args4, 1 + 4);

            _ = lruCache.Get(args1);
            _ = lruCache.Get(args2);
            _ = lruCache.Get(args4);

            lruCache.Add(args5, 1 + 5);

            Assert.Null(lruCache.Get(args3));
        }

        [Fact]
        public void MoveArgs3ToLeastRecentlyUsedAndSeeEjectedDescending()
        {
            var lruCache = new LruCache();

            var args1 = new object[] { 1, 1 };
            var args2 = new object[] { 1, 2 };
            var args3 = new object[] { 1, 3 };
            var args4 = new object[] { 1, 4 };
            var args5 = new object[] { 1, 5 };

            lruCache.Add(args1, 1 + 1);
            lruCache.Add(args2, 1 + 2);
            lruCache.Add(args3, 1 + 3);
            lruCache.Add(args4, 1 + 4);

            _ = lruCache.Get(args4);
            _ = lruCache.Get(args2);
            _ = lruCache.Get(args1);

            lruCache.Add(args5, 1 + 5);

            Assert.Null(lruCache.Get(args3));
        }

        [Fact]
        public void MoveArgs4ToLeastRecentlyUsedAndSeeEjectedAscending()
        {
            var lruCache = new LruCache();

            var args1 = new object[] { 1, 1 };
            var args2 = new object[] { 1, 2 };
            var args3 = new object[] { 1, 3 };
            var args4 = new object[] { 1, 4 };
            var args5 = new object[] { 1, 5 };

            lruCache.Add(args1, 1 + 1);
            lruCache.Add(args2, 1 + 2);
            lruCache.Add(args3, 1 + 3);
            lruCache.Add(args4, 1 + 4);

            _ = lruCache.Get(args1);
            _ = lruCache.Get(args2);
            _ = lruCache.Get(args3);

            lruCache.Add(args5, 1 + 5);

            Assert.Null(lruCache.Get(args4));
        }

        [Fact]
        public void MoveArgs4ToLeastRecentlyUsedAndSeeEjectedDescending()
        {
            var lruCache = new LruCache();

            var args1 = new object[] { 1, 1 };
            var args2 = new object[] { 1, 2 };
            var args3 = new object[] { 1, 3 };
            var args4 = new object[] { 1, 4 };
            var args5 = new object[] { 1, 5 };

            lruCache.Add(args1, 1 + 1);
            lruCache.Add(args2, 1 + 2);
            lruCache.Add(args3, 1 + 3);
            lruCache.Add(args4, 1 + 4);

            _ = lruCache.Get(args3);
            _ = lruCache.Get(args2);
            _ = lruCache.Get(args1);

            lruCache.Add(args5, 1 + 5);

            Assert.Null(lruCache.Get(args4));
        }
    }
}
