using LRUSharp.Tests.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LRUSharp.Tests.EndToEndTests
{
    public class FunctionsWithoutCachingTests
    {
        [Fact]
        public void MethodExecuted()
        {
            var functionsWithoutCaching = new FunctionsWithoutCaching();

            functionsWithoutCaching.Add(1, 2);

            Assert.Equal(1, functionsWithoutCaching.MethodExecutions);
        }

        [Fact]
        public void MethodExecutedTwice()
        {
            var functionsWithoutCaching = new FunctionsWithoutCaching();

            functionsWithoutCaching.Add(1, 2);

            Assert.Equal(1, functionsWithoutCaching.MethodExecutions);



            functionsWithoutCaching.Add(1, 3);

            Assert.Equal(2, functionsWithoutCaching.MethodExecutions);
        }

        [Fact]
        public void MethodExecutedTwiceWhenNotCached()
        {
            var functionsWithoutCaching = new FunctionsWithoutCaching();

            Assert.Equal(3, functionsWithoutCaching.Add(1, 2));
            
            Assert.Equal(1, functionsWithoutCaching.MethodExecutions);


            Assert.Equal(3, functionsWithoutCaching.Add(1, 2));

            Assert.Equal(2, functionsWithoutCaching.MethodExecutions);
        }
    }
}
