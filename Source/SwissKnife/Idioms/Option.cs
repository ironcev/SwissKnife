using System;

namespace SwissKnife.Idioms
{
    /// <summary>
    /// Describes the option type. An option is used when a value may or may not exist. An option has an underlying type and may either hold a value of that type or it may not have a value.
    /// </summary>
    /// <typeparam name="T">The underlying type.</typeparam>
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
        /// Creates an option value that is a None value.
        /// </summary>
        /// <returns>
        /// An option that is set to None.
        /// </returns>
        public static Option<T> None
        {
            get { return none; }
        }

        /// <summary>
        /// Creates an option value that is a Some value.
        /// </summary>
        /// <param name="value">The value represented by the option.</param>
        /// <returns>An option that represents the <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="value"/> is null.</exception>
        public static Option<T> Some(T value)
        {
            #region Preconditions
            if (value == null) throw new ArgumentNullException("value");
            #endregion

            return new Option<T>(value);
        }

        /// <summary>
        /// Creates an option value that is either a None or a Some value.
        /// If <paramref name="valueOrNull"/> is null, a None value is created.
        /// If <paramref name="valueOrNull"/> is not null, a Some value is created.
        /// </summary>
        /// <param name="valueOrNull">The value represented by the option or null.</param>
        /// <returns>None if <paramref name="valueOrNull"/> is null, otherwise Some that represents the value.</returns>
        public static Option<T> From(T valueOrNull)
        {
            return valueOrNull == null ? None : Some(valueOrNull);
        }

        /// <summary>
        /// Returns true if the option has the None value.
        /// </summary>
        public bool IsNone
        {
            get { return value == null; }
        }

        /// <summary>
        /// Returns true if the option has a value that is not None.
        /// </summary>
        public bool IsSome
        {
            get { return value != null; }
        }

        /// <summary>
        /// Gets the value of the option if the option is a Some value.
        /// </summary>
        /// <returns>
        /// The value of the option if the option is a Some value. An exception is thrown if the option is a None value.
        /// </returns>
        /// <exception cref="InvalidOperationException">If the option is a None value.</exception>
        public T Value
        {
            get
            {
                if (value == null) throw new InvalidOperationException("Option must have a value.");
                return value;
            }
        }

        /// <summary>
        /// Gets the value of the option if the option is a Some value, or null if the option is a None value.
        /// </summary>
        /// <returns>
        /// The value of the option if the option is a Some value, or null if the option is a None value.
        /// </returns>
        public T ValueOrNull
        {
            get { return value; }
        }

        /// <summary>
        /// Gets the value of the option if the option is a Some value, or the <paramref name="defaultValue"/> if the option is a None value.
        /// </summary>
        /// <returns>
        /// The value of the option if the option is a Some value, or <paramref name="defaultValue"/> if the option is a None value.
        /// </returns>
        public T ValueOr(T defaultValue)
        {
            return IsSome ? value : defaultValue;
        }

        /// <summary>
        /// Invokes a function on an optional value that itself yields an option.
        /// </summary>
        /// <param name="binder">A function that takes the value of the type <typeparamref name="T"/> from the option and transforms it into an option containing a value of the type <typeparamref name="TOutput"/>.</param>
        /// <typeparam name="TOutput">The output type.</typeparam>
        /// <returns>None if the option is None. The output of the <paramref name="binder"/> if the option is Some.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="binder"/> is null.</exception>
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
        /// <param name="binder">A function that takes the value of type T from the option and transforms it into an nullable containing a value of type <typeparamref name="TOutput"/>.</param>
        /// <typeparam name="TOutput">The output type.</typeparam>
        /// <returns>Null if the option is None. The output of the <paramref name="binder"/> if the option is Some.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="binder"/> is null.</exception>
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
        /// <param name="mapper">A function to apply to the option value.</param>
        /// <typeparam name="TOutput">The output type.</typeparam>
        /// <returns>None if the option is None or the output of the <paramref name="mapper"/> is null. Some if the output of the <paramref name="mapper"/> is not null.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="mapper"/> is null.</exception>
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
        /// <param name="mapper">A function to apply to the option value.</param>
        /// <typeparam name="TOutput">The output type.</typeparam>
        /// <returns>None if the option is None or the output of the <paramref name="mapper"/> is null. Some if the output of the <paramref name="mapper"/> is not null.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="mapper"/> is null.</exception>
        public TOutput? MapToNullable<TOutput>(Func<T, TOutput> mapper) where TOutput : struct
        {
            #region Preconditions
            if (mapper == null) throw new ArgumentNullException("mapper");
            #endregion

            return IsNone ? null : (TOutput?)mapper(value);
        }

        /// <summary>
        /// Converts <paramref name="valueOrNull"/> into the <see cref="Option{T}"/> that represents it.
        /// If the <paramref name="valueOrNull"/> is not null, Some will be returned.
        /// If the <paramref name="valueOrNull"/> is null, None will be returned.
        /// </summary>
        /// <param name="valueOrNull">Value that has to be converted into <see cref="Option{T}"/>.</param>
        /// <returns>Some if <paramref name="valueOrNull"/> is not null, otherwise None.</returns>
        public static implicit operator Option<T> (T valueOrNull)
        {
            return From(valueOrNull);
        }

        // TODO-IG: Think about equality.
    }
}
