using System;
using System.Collections.Generic;

public static class List
{
    public static List<T> Empty<T>()
    {
        return new();
    }

    public static bool IsEmpty<T>(this List<T> list)
    {
        return list.Count == 0;
    }

    public static List<T> Of<T>(params T[] list)
    {
        return new(list);
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

    public static void Deconstruct<T>(this List<T> list, out T head, out List<T> tail)
    {
        head = list.Head();
        tail = list.Tail();
    }

    public static void ForEach<T>(List<T> list, Action<T> action)
    {
        if (list.IsEmpty())
            return;

        var (h, t) = list;
        action(h);
        ForEach(t, action);
    }

    private static List<U> Map_aux<T, U>(List<T> list, Func<T, U> map, List<U> acc)
    {
        if (list.IsEmpty())
            return acc;

        var (h, t) = list;
        return Map_aux(t, map, acc.Append(map(h)));
    }

    public static List<U> Map<T, U>(List<T> list, Func<T, U> map)
    {
        return Map_aux(list, map, Empty<U>());
    }

    public static b FoldLeft<a, b>(List<a> list, b starter, Func<b, a, b> folder)
    {
        if (list.IsEmpty())
            return starter;

        var (h, t) = list;
        return FoldLeft(t, folder(starter, h), folder);
    }
    
    private static List<a> Filter_aux<a>(List<a> list, Predicate<a> predicate, List<a> acc)
    {
        if (list.IsEmpty())
            return acc;

        var (h, t) = list;
        return Filter_aux<a>(t, predicate, predicate(h) ? acc.Append(h) : acc);
    }

    public static List<a> Filter<a>(List<a> list, Predicate<a> predicate)
    {
        return Filter_aux(list, predicate, Empty<a>());
    }

    public static List<a> Remove<a>(List<a> l, a t)
    {
        return Filter(l, e => !e.Equals(t));
    }

    public static string StringOf<T>(List<T> list)
    {
        if (list.IsEmpty())
            return "[]";

        var (h, t) = list;
        return "[" + h + FoldLeft(Map(t, e => e.ToString()), "", (lhs, rhs) => lhs + "; " + rhs) + "]";
    }
}