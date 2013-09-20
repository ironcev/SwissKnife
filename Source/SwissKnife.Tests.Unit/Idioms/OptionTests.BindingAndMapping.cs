using System;
using NUnit.Framework;

namespace SwissKnife.Tests.Unit.Idioms
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public partial class OptionTests
    {
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
    }
    // ReSharper restore InconsistentNaming
}
