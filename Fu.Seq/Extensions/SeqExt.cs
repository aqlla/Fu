using System;
using System.Collections.Generic;
using System.Linq;

namespace Fu.Seq.Extensions;


public static class SeqExt
{
    extension(int n)
    {
        
        public IEnumerable<int> Range(int start = 0) => 
            Enumerable.Range(start, n);
        
        public IEnumerable<TOut> Repeat<TOut>(Func<TOut> fn) => 
            Range(n).Select(_ => fn());
        
        public IEnumerable<TOut> Select<TOut>(Func<int, TOut> fn, int start = 0) => 
            Enumerable.Range(start, n).Select(fn);
    }
}