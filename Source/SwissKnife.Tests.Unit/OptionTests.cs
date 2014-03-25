using System;
using NUnit.Framework;

namespace SwissKnife.Tests.Unit
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class OptionTests
    {
        #region Static Factory Methods
        [Test]
        public void None_CreatesNone()
        {
            var none = Option<object>.None;
            Assert.That(none.IsNone);
        }

        [Test]
        public void Some_CreatesSome()
        {
            var some = Option<object>.Some(new object());
            Assert.That(some.IsSome);
        }

        [Test]
        public void Some_CreatesSomeThatRepresentsValue()
        {
            var value = new object();
            var some = Option<object>.Some(value);
            Assert.That(some.Value, Is.SameAs(value));
        }

        [Test]
        public void From_ValueIsNull_CreatesNone()
        {
            var option = Option<object>.From(null);
            Assert.That(option.IsNone);
        }

        [Test]
        public void From_ValueIsNotNull_CreatesSome()
        {
            var option = Option<object>.From(new object());
            Assert.That(option.IsSome);
        }

        [Test]
        public void From_ValueIsNotNull_CreatesSomeThatRepresentsValue()
        {
            var value = new object();
            var option = Option<object>.From(value);
            Assert.That(option.Value, Is.SameAs(value));
        }
        #endregion

        #region Value Inspection and Retrieval
        [Test]
        public void IsNone_OptionIsNone_ReturnsTrue()
        {
            var none = Option<object>.None;
            Assert.That(none.IsNone, Is.True);
        }

        [Test]
        public void IsNone_OptionIsSome_ReturnsFalse()
        {
            var some = Option<object>.Some(new object());
            Assert.That(some.IsNone, Is.False);
        }

        [Test]
        public void IsSome_OptionIsSome_ReturnsTrue()
        {
            var some = Option<object>.Some(new object());
            Assert.That(some.IsSome, Is.True);
        }

        [Test]
        public void IsSome_OptionIsNone_ReturnsFalse()
        {
            var none = Option<object>.None;
            Assert.That(none.IsSome, Is.False);
        }

        [Test]
        public void Value_OptionIsNone_ThrowsException()
        {
            var none = Option<object>.None;
            var exceptionMessage = Assert.Throws<InvalidOperationException>(() => Console.WriteLine(none.Value)).Message;
            Assert.That(exceptionMessage, Is.EqualTo("Option must have a value."));
        }

        [Test]
        public void Value_OptionIsSome_ReturnsValue()
        {
            var value = new object();
            var some = Option<object>.Some(value);
            Assert.That(some.Value, Is.SameAs(value));
        }

        [Test]
        public void ValueOrNull_OptionIsNone_ReturnsNull()
        {
            var none = Option<object>.None;
            Assert.That(none.ValueOrNull, Is.Null);
        }

        [Test]
        public void ValueOrNull_OptionIsSome_ReturnsValue()
        {
            var value = new object();
            var some = Option<object>.Some(value);
            Assert.That(some.ValueOrNull, Is.SameAs(value));
        }

        [Test]
        public void ValueOr_OptionIsNone_ReturnsDefaultValue()
        {
            var defaultValue = new object();
            var none = Option<object>.None;
            Assert.That(none.ValueOr(defaultValue), Is.SameAs(defaultValue));
        }

        [Test]
        public void ValueOr_OptionIsSome_ReturnsValue()
        {
            var value = new object();
            var some = Option<object>.Some(value);
            Assert.That(some.ValueOr(new object()), Is.SameAs(value));
        }

        #endregion

        #region BindToOption
        [Test]
        public void BindToOption_BinderIsNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => Option<object>.None.Bind((Func<object,Option<string>>)null));
        }

        [Test]
        public void BindToOption_OptionIsNone_DoesNotCallBinder()
        {
            int callCounter = 0;
            Option<object>.None.Bind(x => { callCounter++; return Option<object>.From(x); });
            Assert.That(callCounter, Is.EqualTo(0));
        }

        [Test]
        public void BindToOption_OptionIsNone_ReturnsNone()
        {
            Assert.That(Option<object>.None.Bind(Option<object>.From).IsNone, Is.True);
        }

        [Test]
        public void BindToOption_OptionIsSome_CallsBinderForTheObjectRepresentedWithSome()
        {
            int callCounter = 0;
            object some = new object();
            object invokedObject = null;
            Option<object>.Some(some).Bind(x => { callCounter++; invokedObject = x; return Option<object>.From(x); });
            Assert.That(callCounter, Is.EqualTo(1));
            Assert.That(invokedObject, Is.SameAs(some));
        }

        [Test]
        public void BindToOption_OptionIsSome_ReturnsBindersResult()
        {
            object binderResult = new object();
            var result = Option<object>.Some(new object()).Bind(x => Option<object>.From(binderResult));
            Assert.That(result.Value, Is.SameAs(binderResult));
        }
        #endregion

        #region BindToNullable
        [Test]
        public void BindToNullable_BinderIsNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => Option<object>.None.Bind((Func<object, int?>)null));
        }

        [Test]
        public void BindToNullable_OptionIsNone_DoesNotCallBinder()
        {
            int callCounter = 0;
            Option<object>.None.Bind(x => { callCounter++; return new int?(); });
            Assert.That(callCounter, Is.EqualTo(0));
        }

        [Test]
        public void BindToNullable_OptionIsNone_ReturnsNullableWithoutValue()
        {
            Assert.That(Option<object>.None.Bind(x => new int?()).HasValue, Is.False);
        }

        [Test]
        public void BindToNullable_OptionIsSome_CallsBinderForTheObjectRepresentedWithSome()
        {
            int callCounter = 0;
            object some = new object();
            object invokedObject = null;
            Option<object>.Some(some).Bind(x => { callCounter++; invokedObject = x; return new int?(); });
            Assert.That(callCounter, Is.EqualTo(1));
            Assert.That(invokedObject, Is.SameAs(some));
        }

        [Test]
        public void BindToNullable_OptionIsSome_ReturnsBindersResult()
        {
            int? binderResult = 123;
            var result = Option<object>.Some(new object()).Bind(x => binderResult);
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(result, Is.EqualTo(binderResult));
            // ReSharper restore PossibleInvalidOperationException
        }
        #endregion

        #region MapToOption
        [Test]
        public void MapToOption_BinderIsNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => Option<object>.None.MapToOption((Func<object, string>)null));
        }

        [Test]
        public void MapToOption_OptionIsNone_DoesNotCallMapper()
        {
            int callCounter = 0;
            Option<object>.None.MapToOption(x => { callCounter++; return x; });
            Assert.That(callCounter, Is.EqualTo(0));
        }

        [Test]
        public void MapToOption_OptionIsNone_ReturnsNone()
        {
            Assert.That(Option<object>.None.MapToOption(x => x).IsNone, Is.True);
        }

        [Test]
        public void MaptToOption_OptionIsSome_CallsMapperForTheObjectRepresentedWithSome()
        {
            int callCounter = 0;
            object some = new object();
            object invokedObject = null;
            Option<object>.Some(some).MapToOption(x => { callCounter++; invokedObject = x; return x; });
            Assert.That(callCounter, Is.EqualTo(1));
            Assert.That(invokedObject, Is.SameAs(some));
        }

        [Test]
        public void MapToOption_OptionIsSome_ReturnsMapperResult()
        {
            object mapperResult = new object();
            var result = Option<object>.Some(new object()).MapToOption(x => mapperResult);
            Assert.That(result.Value, Is.SameAs(mapperResult));
        }
        #endregion

        #region MapToNullable
        [Test]
        public void MapToNullable_MapperIsNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => Option<object>.None.MapToNullable((Func<object, int>)null));
        }

        [Test]
        public void MapToNullable_OptionIsNone_DoesNotCallMapper()
        {
            int callCounter = 0;
            Option<object>.None.MapToNullable(x => { callCounter++; return new int(); });
            Assert.That(callCounter, Is.EqualTo(0));
        }

        [Test]
        public void MapToNullable_OptionIsNone_ReturnsNullableWithoutValue()
        {
            Assert.That(Option<object>.None.MapToNullable(x => new int()).HasValue, Is.False);
        }

        [Test]
        public void MapToNullable_OptionIsSome_CallsMapperForTheObjectRepresentedWithSome()
        {
            int callCounter = 0;
            object some = new object();
            object invokedObject = null;
            Option<object>.Some(some).MapToNullable(x => { callCounter++; invokedObject = x; return new int(); });
            Assert.That(callCounter, Is.EqualTo(1));
            Assert.That(invokedObject, Is.SameAs(some));
        }

        [Test]
        public void MapToNullable_OptionIsSome_ReturnsMapperResult()
        {
            const int mapperResult = 123;
            var result = Option<object>.Some(new object()).MapToNullable(x => mapperResult);
            // ReSharper disable PossibleInvalidOperationException
            Assert.That(result, Is.EqualTo(mapperResult));
            // ReSharper restore PossibleInvalidOperationException
        }
        #endregion

        #region Implicit Conversion
        [Test]
        public void ImplicitConversion_Null_ConvertsToNone()
        {
            Option<object> option = null;
            Assert.That(option.IsNone);
        }

        [Test]
        public void ImplicitConversion_NotNull_ConvertsToSome()
        {
            Option<object> option = new object();
            Assert.That(option.IsSome);
        }

        [Test]
        public void ImplicitConversion_NotNull_ConvertsToSomeThatRepresentsSameValue()
        {
            var value = new object();
            Option<object> option = value;
            Assert.That(option.Value, Is.SameAs(value));
        }
        #endregion
    }
    // ReSharper restore InconsistentNaming
}
