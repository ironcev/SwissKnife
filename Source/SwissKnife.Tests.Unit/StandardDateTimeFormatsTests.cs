using NUnit.Framework;

namespace SwissKnife.Tests.Unit
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class StandardDateTimeFormatsTests
    {
        #region AllStandardDateTimeFormats
        [Test]
        public void AllStandardDateTimeFormats_ContainsAllStandardDateTimeFormats()
        {
            CollectionAssert.AreEquivalent(StandardDateTimeFormats.AllStandardDateTimeFormats,
                                           new [] { "d", "D", "f", "F", "g", "G", "m", "o", "r", "s", "t", "T", "u", "U", "y" });
        }
        #endregion
    }
    // ReSharper restore InconsistentNaming
}
