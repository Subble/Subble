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
        /// <summary>
        /// value to encapsulate
        /// </summary>
        private readonly object _value;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="value">value to encapsulate</param>
        internal Option(object value)
            => _value = value;
            
        /// <summary>
        /// Check is has value not null
        /// </summary>
        /// <returns>False if null</returns>
        public bool HasValue()
        {
            return !(_value is null);
        }

        /// <summary>
        /// Match value to Type
        /// </summary>
        /// <typeparam name="T">type to match</typeparam>
        /// <param name="value">out value</param>
        /// <returns>true if type match</returns>
        public bool HasValue<T>(out T value)
        {
            value = default(T);

            if (_value is T t)
            {
                value = t;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Match value to Type
        /// </summary>
        /// <typeparam name="T">type to match</typeparam>
        /// <returns>true if type match</returns>
        public bool HasValue<T>()
            => _value is T;

        /// <summary>
        /// If type match and has value,
        /// Some is invoked, else none is invoked
        /// </summary>
        /// <typeparam name="T">Type to match</typeparam>
        /// <param name="None">Invoked if value is null</param>
        /// <param name="Some">Invoked if value is not null</param>
        public Option Match<T>(Action None, Action<T> Some)
        {
            if (_value is T v) Some(v);
            else None();

            return this;
        }

        /// <summary>
        /// If type match, invoke action
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Some">action to invoke</param>
        /// <returns>returns self to allow chain</returns>
        public Option Some<T>(Action<T> Some)
            => Match(Void, Some);

        /// <summary>
        /// If type do not match or if null, invoke action
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="None">action to invoke</param>
        /// <returns>returns self to allow chain</returns>
        public Option None<T>(Action None)
            => Match<T>(None, Void);

        /// <summary>
        /// Return option with implicit type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Option<T> ToTyped<T>()
            => new Option<T>(this);

        /// <summary>
        /// Allow to chain function
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="pipe"></param>
        /// <returns></returns>
        public Option Pipe<TIn, TOut>(Func<TIn, TOut> pipe)
        {
            if (!HasValue<TIn>(out var obj))
                return None();

            try
            {
                return Some(pipe(obj));
            }
            catch
            {
                return None();
            }
        }

        /// <summary>
        /// Encapsulate value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Option<T> Some<T>(T value)
            => new Option<T>(value);

        /// <summary>
        /// Empty encapsulation 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Option<T> None<T>()
            => new Option<T>(None());

        /// <summary>
        /// Empty encapsulation 
        /// </summary>
        /// <returns></returns>
        public static Option None()
            => new Option(null);

        /// <summary>
        /// Encapsulate value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Option Some(object value)
            => new Option(value);
    }

    /// <summary>
    /// Like Option, but with implicit type
    /// </summary>
    /// <typeparam name="T">Type to be matched</typeparam>
    public struct Option<T>
    {
        /// <summary>
        /// Base encapsulation
        /// </summary>
        private readonly Option option;

        internal Option(T value)
            => option = new Option(value);

        internal Option(Option option)
            => this.option = option;

        /// <summary>
        /// Check value
        /// </summary>
        /// <returns>False, if null or type don't match</returns>
        public bool HasValue()
            => option.HasValue<T>();

        /// <summary>
        /// Check value
        /// </summary>
        /// <param name="value">value or default</param>
        /// <returns>False, if null or type don't match</returns>
        public bool HasValue(out T value)
            => option.HasValue(out value);

        /// <summary>
        /// Invoke function based value
        /// </summary>
        /// <param name="None">Invoked if null or type don't match</param>
        /// <param name="Some">Invoked if has value</param>
        /// <returns></returns>
        public Option<T> Match(Action None, Action<T> Some)
        {
            option.Match(None, Some);
            return this;
        }

        /// <summary>
        /// Invoke action if has value
        /// </summary>
        /// <param name="Some"></param>
        /// <returns></returns>
        public Option<T> Some(Action<T> Some)
            => Match(Void, Some);

        /// <summary>
        /// Invoke action if null or type won't match
        /// </summary>
        /// <param name="None"></param>
        /// <returns></returns>
        public Option<T> None(Action None)
            => Match(None, Void);

        /// <summary>
        /// Cast value to another type
        /// </summary>
        /// <typeparam name="TResult">result type</typeparam>
        /// <param name="func">function to return cast result</param>
        /// <returns></returns>
        public Option<TResult> Cast<TResult>(Func<T, TResult> func)
        {
            if (!HasValue(out var res))
                return Option.None<TResult>();

            return Option.Some(func(res));
        }

        /// <summary>
        /// Allow to chain function
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="pipe"></param>
        /// <returns></returns>
        public Option<TOut> Pipe<TOut>(Func<T, TOut> pipe)
        {
            return ((Option)this).Pipe(pipe);
        }

        public static implicit operator Option(Option<T> parent)
            => parent.option;

        public static implicit operator Option<T>(Option parent)
            => parent.ToTyped<T>();
    }
}
