using System.Numerics;

namespace Fu.Core;




public delegate T Adder<T>(T value) 
    where T : IAdditionOperators<T, T, T>;
    