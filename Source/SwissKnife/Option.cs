using System;

namespace SwissKnife
{
    /// <summary>
    /// Describes the option type. An option is used when a value may or may not exist. An option has an underlying type and may either hold a value of that type or it may not have a value.
    /// </summary>
    /// <typeparam name="T">The underlying type.</typeparam>
    [Serializable]
    public struct Option<T> where T : class
    {
        // ReSharper disable StaticFieldInGenericType
        private static readonly Option<T> none = new Option<T>(null); // Of course, we want to have 'specialized' static fields, one for each T.
        // ReSharper restore StaticFieldInGenericType

        private readonly T value;

        private Option(T value)
        {
            this.value = value;
        }

        /// <summary>
        /// Creates a None option.
        /// </summary>
        /// <returns>
        /// An option that is set to None.
        /// </returns>
        public static Option<T> None
        {
            get { return none; }
        }

        /// <summary>
        /// Creates a Some option that represents the <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value represented by the option.</param>
        /// <returns>An option that represents the <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        public static Option<T> Some(T value)
        {
            #region Preconditions
            if (value == null) throw new ArgumentNullException("value");
            #endregion

            return new Option<T>(value);
        }

        /// <summary>
        /// Creates an option that is either None or Some depending on the provided value.
        /// </summary>
        /// <remarks>
        /// If <paramref name="valueOrNull"/> is null, a None option is created.
        /// If <paramref name="valueOrNull"/> is not null, Some option is created.
        /// </remarks>
        /// <param name="valueOrNull">The value represented by the option or null.</param>
        /// <returns>None if <paramref name="valueOrNull"/> is null, otherwise Some that represents the value.</returns>
        public static Option<T> From(T valueOrNull)
        {
            return valueOrNull == null ? None : Some(valueOrNull);
        }

        /// <summary>
        /// Returns true if the option is a None option.
        /// </summary>
        public bool IsNone
        {
            get { return value == null; }
        }

        /// <summary>
        /// Returns true if the option is a Some option.
        /// </summary>
        public bool IsSome
        {
            get { return value != null; }
        }

        /// <summary>
        /// Gets the value of the option if the option is a Some option.
        /// </summary>
        /// <returns>
        /// The value of the option if the option is Some. An <see cref="InvalidOperationException"/> is thrown if the option is a None option.
        /// </returns>
        /// <exception cref="InvalidOperationException">The option is a None option.</exception>
        public T Value
        {
            get
            {
                if (value == null) throw new InvalidOperationException("Option must have a value.");
                return value;
            }
        }

        /// <summary>
        /// Gets the value of the option if the option is Some option, or null if the option is a None option.
        /// </summary>
        /// <returns>
        /// The value of the option if the option represents some value, or null if the option is a None option.
        /// </returns>
        public T ValueOrNull
        {
            get { return value; }
        }

        /// <summary>
        /// Gets the value of the option if the option is Some option, or some default value if the option is a None option.
        /// </summary>
        /// <param name="defaultValue">The value that will be returned if the option is a None option.</param>
        /// <returns>
        /// The value of the option if the option represents some value, or <paramref name="defaultValue"/> if the option is a None option.
        /// </returns>
        public T ValueOr(T defaultValue)
        {
            return IsSome ? value : defaultValue;
        }

        /// <summary>
        /// Invokes a function on an optional value that itself yields an option.
        /// </summary>
        /// <remarks>
        /// <b>Note</b><br/>
        /// If the <paramref name="binder"/> throws an exception, that exception will be propagated to the caller.
        /// </remarks>
        /// <param name="binder">A function that takes the value of the type <typeparamref name="T"/> from the option and transforms it into an option containing a value of the type <typeparamref name="TOutput"/>.</param>
        /// <typeparam name="TOutput">The output type.</typeparam>
        /// <returns>None if the option is None. The output of the <paramref name="binder"/> if the option is Some.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="binder"/> is null.</exception>
        public Option<TOutput> Bind<TOutput>(Func<T, Option<TOutput>> binder) where TOutput : class
        {
            #region Preconditions
            if (binder == null) throw new ArgumentNullException("binder");
            #endregion

            return IsNone ? Option<TOutput>.None : binder(value);
        }

        /// <summary>
        /// Invokes a function on an optional value that yields an <see cref="Nullable{TOutput}"/>.
        /// </summary>
        /// <remarks>
        /// <b>Note</b><br/>
        /// If the <paramref name="binder"/> throws an exception, that exception will be propagated to the caller.
        /// </remarks>
        /// <typeparam name="TOutput">The output type.</typeparam>
        /// <param name="binder">A function that takes the value of type T from the option and transforms it into an nullable containing a value of type <typeparamref name="TOutput"/>.</param>
        /// <returns>Null if the option is None. The output of the <paramref name="binder"/> if the option is Some.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="binder"/> is null.</exception>
        public TOutput? Bind<TOutput>(Func<T, TOutput?> binder) where TOutput : struct
        {
            #region Preconditions
            if (binder == null) throw new ArgumentNullException("binder");
            #endregion

            return IsNone ? null : binder(value);
        }

        /// <summary>
        /// Transforms an option value by using the specified mapping function.
        /// </summary>
        /// <remarks>
        /// <b>Note</b><br/>
        /// If the <paramref name="mapper"/> throws an exception, that exception will be propagated to the caller.
        /// </remarks>
        /// <param name="mapper">A function to apply to the option value.</param>
        /// <typeparam name="TOutput">The output type.</typeparam>
        /// <returns>None if the option is None or the output of the <paramref name="mapper"/> is null. Some if the output of the <paramref name="mapper"/> is not null.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is null.</exception>
        public Option<TOutput> MapToOption<TOutput>(Func<T, TOutput> mapper) where TOutput : class
        {
            #region Preconditions
            if (mapper == null) throw new ArgumentNullException("mapper");
            #endregion

            return IsNone ? Option<TOutput>.None : Option<TOutput>.From(mapper(value));
        }

        /// <summary>
        /// Transforms an option value by using the specified mapping function.
        /// </summary>
        /// <remarks>
        /// <b>Note</b><br/>
        /// If the <paramref name="mapper"/> throws any exception, that exception will be propagated to the caller.
        /// </remarks>
        /// <param name="mapper">A function to apply to the option value.</param>
        /// <typeparam name="TOutput">The output type.</typeparam>
        /// <returns>None if the option is None or the output of the <paramref name="mapper"/> is null. Some if the output of the <paramref name="mapper"/> is not null.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> is null.</exception>
        public TOutput? MapToNullable<TOutput>(Func<T, TOutput> mapper) where TOutput : struct
        {
            #region Preconditions
            if (mapper == null) throw new ArgumentNullException("mapper");
            #endregion

            return IsNone ? null : (TOutput?)mapper(value);
        }

        /// <summary>
        /// Converts value or null into the <see cref="Option{T}"/> that represents it.
        /// </summary>
        /// <remarks>
        /// Null is converted into None option.
        /// Non-null vale is converted into Some option.
        /// </remarks>
        /// <param name="valueOrNull">Value that has to be converted into <see cref="Option{T}"/>.</param>
        /// <returns>Some if <paramref name="valueOrNull"/> is not null, otherwise None.</returns>
        public static implicit operator Option<T> (T valueOrNull)
        {
            return From(valueOrNull);
        }

        // TODO-IG: Think about equality.
    }
}
