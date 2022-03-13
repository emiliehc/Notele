using System;

public static class Reference
{
    public static ReferenceImpl<T> Ref<T>(T value)
    {
        return new() {contents = value};
    }

    public class ReferenceImpl<T>
    {
        public T contents;

        public void Assign(T t)
        {
            contents = t;
        }

        public static T operator !(ReferenceImpl<T> r) => r.contents;
    }
}
