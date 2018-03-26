using System;
using static Subble.Core.Func.BaseType;

namespace Subble.Core.Func
{
    /// <summary>
    /// Force user to handle null
    /// You must match the exact type, eg: Int16 won't match to int
    /// </summary>
    public struct Option
    {
        private readonly object _value;

        internal Option(object value)
            => _value = value;

        /// <summary>
        /// Match value to Type
        /// </summary>
        /// <typeparam name="T">type to match</typeparam>
        /// <param name="value">out value</param>
        /// <returns>true if type match</returns>
        public bool HasValue<T>(out T value)
        {
            value = default(T);

            if (_value is T)
            {
                value = (T)_value;
                return true;
            }

            return false;
        }

        public bool HasValue<T>()
            => _value is T;

        /// <summary>
        /// Pattern match simulation
        /// </summary>
        /// <typeparam name="T">Type to match</typeparam>
        /// <param name="None">Called if value id null</param>
        /// <param name="Some">Called if value is not null</param>
        public Option Match<T>(Action None, Action<T> Some)
        {
            if (_value is T) Some((T)_value);
            else None();

            return this;
        }

        public Option Some<T>(Action<T> Some)
            => Match(Void, Some);

        public Option None<T>(Action None)
            => Match<T>(None, Void);

        public Option<T> ToTyped<T>()
            => new Option<T>(this);

        public static Option<T> Some<T>(T value)
            => new Option<T>(value);

        public static Option<T> None<T>()
            => new Option<T>(None());

        public static Option None()
            => new Option(null);

        public static Option Some(object instance)
            => new Option(instance);
    }

    /// <summary>
    /// Like Option, but forces type
    /// </summary>
    /// <typeparam name="T">Type to be matched</typeparam>
    public struct Option<T>
    {
        private readonly Option option;

        internal Option(T value)
            => option = new Option(value);

        internal Option(Option option)
            => this.option = option;

        public bool HasValue()
            => option.HasValue<T>();

        public bool HasValue(out T value)
            => option.HasValue(out value);

        public Option<T> Match(Action None, Action<T> Some)
        {
            option.Match(None, Some);
            return this;
        }

        public Option<T> Some(Action<T> Some)
            => Match(Void, Some);

        public Option<T> None(Action None)
            => Match(None, Void);

        public Option<TResult> Cast<TResult>(Func<T, TResult> func)
        {
            if (HasValue(out var res))
                return Option.Some(func(res));

            return Option.None<TResult>();
        }

        public static implicit operator Option(Option<T> parent)
            => parent.option;
    }
}
