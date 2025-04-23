using System.Collections.Generic;

namespace Fu.Seq.Extensions;


public static class StackExt
{

	public static List<T> Pop<T>(this Stack<T> stack, int count)
	{
		var result = new List<T>(count);
		while (count-- > 0) result.Add(stack.Pop());
		return result;
	}

	public static T Psh<T>(this Stack<T> stack, T item)
	{
		stack.Push(item);
		return item;
	}
}
