using System.Collections.Generic;

namespace Fu.Seq.Extensions;


public static class ListExt
{
	public static T Pop<T>(this List<T> list)
	{
		var ret = list[^1];
		list.RemoveAt(list.Count - 1);
		return ret;
	}

	public static T Adds<T>(this List<T> list, T item)
	{
		list.Add(item);
		return item;
	}
}
