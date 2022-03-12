using System.Collections.Generic;

public static class List
{
    public static List<T> New<T>()
    {
        return new();
    }

    public static List<T> Append<T>(this List<T> lhs, IEnumerable<T> rhs)
    {
        lhs = lhs.Clone();
        lhs.AddRange(rhs);
        return lhs;
    }

    public static List<T> Append<T>(this List<T> lhs, T rhs)
    {
        lhs = lhs.Clone();
        lhs.Add(rhs);
        return lhs;
    }

    public static List<T> Append<T>(this T lhs, List<T> rhs)
    {
        rhs = rhs.Clone();
        rhs.Insert(0, lhs);
        return rhs;
    }

    public static List<T> Append<T>(this T lhs, T rhs)
    {
        return new() {lhs, rhs};
    }

    public static List<T> Clone<T>(this List<T> list)
    {
        return new(list);
    }

    public static T Head<T>(this List<T> list)
    {
        return list[0];
    }

    public static List<T> Tail<T>(this List<T> list)
    {
        list = list.Clone();
        list.RemoveAt(0);
        return list;
    }
}