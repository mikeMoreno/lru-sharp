using LRUSharp.Tests.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LRUSharp.Tests.EndToEndTests
{
    public class FunctionsWithCachingTests
    {
        [Fact]
        public void MethodExecuted()
        {
            var functionsWithCaching = new FunctionsWithCaching();

            functionsWithCaching.Add(1, 2);

            Assert.Equal(1, functionsWithCaching.MethodExecutions);
        }

        [Fact]
        public void MethodExecutedTwice()
        {
            var functionsWithCaching = new FunctionsWithCaching();

            functionsWithCaching.Add(1, 2);

            Assert.Equal(1, functionsWithCaching.MethodExecutions);



            functionsWithCaching.Add(1, 3);

            Assert.Equal(2, functionsWithCaching.MethodExecutions);
        }

        [Fact]
        public void MethodExecutedOnlyOnceWhenCached()
        {
            var functionsWithCaching = new FunctionsWithCaching();

            Assert.Equal(3, functionsWithCaching.Add(1, 2));

            Assert.Equal(1, functionsWithCaching.MethodExecutions);


            Assert.Equal(3, functionsWithCaching.Add(1, 2));

            Assert.Equal(1, functionsWithCaching.MethodExecutions);
        }

        [Fact]
        public void DefaultMaxSizeIsTenTestRetention()
        {
            var functionsWithCaching = new FunctionsWithCaching();

            functionsWithCaching.Add(1, 1);
            functionsWithCaching.Add(1, 2);
            functionsWithCaching.Add(1, 3);
            functionsWithCaching.Add(1, 4);
            functionsWithCaching.Add(1, 5);
            functionsWithCaching.Add(1, 6);
            functionsWithCaching.Add(1, 7);
            functionsWithCaching.Add(1, 8);
            functionsWithCaching.Add(1, 9);
            functionsWithCaching.Add(1, 10);
            functionsWithCaching.Add(1, 1);

            Assert.Equal(10, functionsWithCaching.MethodExecutions);
        }

        [Fact]
        public void DefaultMaxSizeIsTenTestEjection()
        {
            var functionsWithCaching = new FunctionsWithCaching();

            functionsWithCaching.Add(1, 1);
            functionsWithCaching.Add(1, 2);
            functionsWithCaching.Add(1, 3);
            functionsWithCaching.Add(1, 4);
            functionsWithCaching.Add(1, 5);
            functionsWithCaching.Add(1, 6);
            functionsWithCaching.Add(1, 7);
            functionsWithCaching.Add(1, 8);
            functionsWithCaching.Add(1, 9);
            functionsWithCaching.Add(1, 10);
            functionsWithCaching.Add(1, 11);
            functionsWithCaching.Add(1, 1);

            Assert.Equal(12, functionsWithCaching.MethodExecutions);
        }

        [Fact]
        public void CustomMaxSizeWorksSmallerSize()
        {
            LruCacheConfiguration.MaxSize = 2;

            var functionsWithCaching = new FunctionsWithCaching();

            functionsWithCaching.Add(1, 1);
            functionsWithCaching.Add(1, 2);
            functionsWithCaching.Add(1, 3);
            functionsWithCaching.Add(1, 1);


            Assert.Equal(4, functionsWithCaching.MethodExecutions);
        }

        [Fact]
        public void CustomMaxSizeWorksLargerSize()
        {
            LruCacheConfiguration.MaxSize = 5;

            var functionsWithCaching = new FunctionsWithCaching();

            functionsWithCaching.Add(1, 1);
            functionsWithCaching.Add(1, 2);
            functionsWithCaching.Add(1, 3);
            functionsWithCaching.Add(1, 4);
            functionsWithCaching.Add(1, 5);
            functionsWithCaching.Add(1, 1);

            Assert.Equal(5, functionsWithCaching.MethodExecutions);
        }
    }
}
