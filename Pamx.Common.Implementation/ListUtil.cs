namespace Pamx.Common.Implementation;

public static class ListUtil
{
    public static void AddRange<T>(this IList<T> list, IEnumerable<T> collection)
    {
        foreach (var item in collection)
            list.Add(item);
    }
}