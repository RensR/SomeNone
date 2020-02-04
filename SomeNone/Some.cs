using System;
using System.Collections.Generic;

namespace SomeNone
{
    public sealed class Some<T> : Option<T>, IEquatable<Some<T>>
    {
        public Some(T value)
        {
            this.Content = value;
        }

        public T Content { get; }

        private string ContentToString => this.Content?.ToString() ?? "<null>";

        public static implicit operator T(Some<T> some) => some.Content;

        public static implicit operator Some<T>(T value) => new Some<T>(value);

        public static bool operator ==(Some<T> a, Some<T> b) =>
            (a is null && b is null) || (!(a is null) && a.Equals(b));

        public static bool operator !=(Some<T> a, Some<T> b) => !(a == b);

        public override Option<TResult> Map<TResult>(Func<T, TResult> map) => map(this.Content);

        public override void Map(Action<T> map) => map(this.Content);

        public override Option<TResult> MapOptional<TResult>(Func<T, Option<TResult>> map) => map(this.Content);

        public override T Reduce(T whenNone) => this.Content;

        public override T Reduce(Func<T> whenNone) => this.Content;

        public override string ToString() => $"Some({this.ContentToString})";

        public bool Equals(Some<T> other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            return ReferenceEquals(this, other) || EqualityComparer<T>.Default.Equals(this.Content, other.Content);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj is Some<T> some && this.Equals(some);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<T>.Default.GetHashCode(this.Content);
        }
    }
}