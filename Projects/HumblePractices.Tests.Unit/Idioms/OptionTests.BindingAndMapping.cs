using System;
using NUnit.Framework;
using HumblePractices.Idioms;

namespace HumblePractices.Tests.Unit.Idioms
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public partial class OptionTests
    {
        [Test]
        public void BindToOption_BinderIsNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => Option<object>.None.Bind((Func<object,Option<string>>)null));
        }

        [Test]
        public void BindToNullable_BinderIsNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => Option<object>.None.Bind((Func<object, int?>)null));
        }
    }
    // ReSharper restore InconsistentNaming
}
