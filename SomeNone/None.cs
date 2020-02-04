using System;
using System.Linq.Expressions;

namespace SomeNone
{
    public sealed class None<T> : Option<T>, IEquatable<None<T>>, IEquatable<None>
    {
        public static bool operator ==(None<T> a, None<T> b) =>
            (a is null && b is null) || (!(a is null) && a.Equals(b));

        public static bool operator !=(None<T> a, None<T> b) => !(a == b);

        public override Option<TResult> Map<TResult>(Func<T, TResult> map) => None.Value;

        public override void Map(Action<T> map) => Expression.Empty();

        public override Option<TResult> MapOptional<TResult>(Func<T, Option<TResult>> map) => None.Value;

        public override T Reduce(T whenNone) => whenNone;

        public override T Reduce(Func<T> whenNone) => whenNone();

        public override bool Equals(object obj) => !(obj is null) && ((obj is None<T>) || (obj is None));

        public override int GetHashCode() => 0;

        public bool Equals(None<T> other) => true;

        public bool Equals(None other) => true;

        public override string ToString() => "None";
    }

    public sealed class None : IEquatable<None>
    {
        private None()
        {
        }

        public static None Value { get; } = new None();

        public override string ToString() => "None";

        public override bool Equals(object obj) => 
            !(obj is null) && ((obj is None) || IsGenericNone(obj.GetType()));

        public bool Equals(None other) => true;

        public override int GetHashCode() => 0;

        private static bool IsGenericNone(Type type) =>
            type.GenericTypeArguments.Length == 1 &&
            typeof(None<>).MakeGenericType(type.GenericTypeArguments[0]) == type;
    }
}