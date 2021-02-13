using System;
using System.Collections.Generic;
using System.Text;

namespace LRUSharp.Tests.Models
{
    class FunctionsWithoutCaching
    {
        public int MethodExecutions { get; private set; }

        /// <summary>
        /// Add two numbers together.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Add(int x, int y)
        {
            MethodExecutions++;

            return x + y;
        }
    }
}

