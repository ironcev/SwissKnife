using NUnit.Framework;
using SwissKnife.Idioms;

namespace SwissKnife.Tests.Unit.Idioms
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public partial class OptionTests
    {
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
    }
    // ReSharper restore InconsistentNaming
}
