using System;
using System.Collections.Generic;
using System.Text;

namespace LRUSharp.Tests.Models
{
    class FunctionsWithCaching
    {
        public int MethodExecutions = 0;

        /// <summary>
        /// Add two numbers together.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [LruCache]
        public int Add(int x, int y)
        {
            MethodExecutions++;

            return x + y;
        }
    }
}
