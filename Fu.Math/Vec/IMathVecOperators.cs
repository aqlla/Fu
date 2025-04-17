using System.Numerics;

namespace Fu.Math.Vec;



public interface IZero<out T> {
    static abstract T Zero { get; }
}

public interface IOne<out T> {
    static abstract T One { get; }
}


public interface IMathVec<out TSelf, TComp> :
    IVec<TComp>, IZero<TSelf>, IOne<TSelf>
    where TComp : struct, INumberBase<TComp>;

public interface IMathVecOperators<TSelf, TComponent> :
    IMathVec<TSelf, TComponent>,
    IAdditionOperators<TSelf, TSelf, TSelf>,
    ISubtractionOperators<TSelf, TSelf, TSelf>,
    IAdditiveIdentity<TSelf, TSelf>,
    IMultiplicativeIdentity<TSelf, TSelf>,
    IDivisionOperators<TSelf, TSelf, TSelf>,
    IMultiplyOperators<TSelf, TSelf, TSelf>,
    IUnaryNegationOperators<TSelf, TSelf>
    where TComponent : struct, INumberBase<TComponent>
    where TSelf : IMathVecOperators<TSelf, TComponent>
{
    TComponent Dot(TSelf other);
    
    static TSelf IAdditiveIdentity<TSelf, TSelf>.AdditiveIdentity => TSelf.Zero;
    static TSelf IMultiplicativeIdentity<TSelf, TSelf>.MultiplicativeIdentity => TSelf.One;
}


public interface IMathVecScalarOperators<TSelf, TComponent> :
    IDivisionOperators<TSelf, TComponent, TSelf>,
    IMultiplyOperators<TSelf, TComponent, TSelf>
    where TComponent : struct, INumberBase<TComponent>
    where TSelf :
        IMathVecOperators<TSelf, TComponent>,
        IMultiplyOperators<TSelf, TComponent, TSelf>,
        IDivisionOperators<TSelf, TComponent, TSelf>;

