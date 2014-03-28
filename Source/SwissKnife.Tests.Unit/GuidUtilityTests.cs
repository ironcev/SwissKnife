using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace SwissKnife.Tests.Unit
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GuidUtilityTests
    {
        [Test]
        public void ToShortString_HasLengthOf22Characters()
        {
            for (int i = 0; i < 100; i++)
                Assert.That(GuidUtility.ToShortString(Guid.NewGuid()).Length, Is.EqualTo(22));
        }

        [Test]
        public void ToShortString_ContainsOnlyAllowedCharacters()
        {
            HashSet<char> allowedCharacters = new HashSet<char>(
                Enumerable.Range('A', 'Z' - 'A' + 1)
                .Concat(Enumerable.Range('a', 'z' - 'a' + 1))
                .Concat(Enumerable.Range('0', '9' - '0' + 1))
                .Concat(new int[] {'-', '_'})
                .Select(character => (char)character)
            );

            for (int i = 0; i < 100; i++)
            {
                string shortRepresentation = GuidUtility.ToShortString(Guid.NewGuid());
                foreach (char character in shortRepresentation)
                    Assert.That(allowedCharacters.Contains(character));
            }                
        }

        [Test]
        public void FromShortString_ShortStringGuidRepresentationIsNull_ReturnsNull()
        {
            Assert.That(GuidUtility.FromShortString(null).HasValue, Is.False);
        }

        [Test]
        public void FromShortString_ShortStringGuidRepresentationIsEmpty_ReturnsNull()
        {
            Assert.That(GuidUtility.FromShortString(string.Empty).HasValue, Is.False);
        }

        [Test]
        public void FromShortString_ShortStringGuidRepresentationIsWhiteSpace_ReturnsNull()
        {
            Assert.That(GuidUtility.FromShortString(" ").HasValue, Is.False);
        }

        [Test]
        public void FromShortString_ShortStringGuidRepresentationHasInvalidLength_ReturnsNull()
        {
            Assert.That(GuidUtility.FromShortString("ABC").HasValue, Is.False);
        }

        [Test]
        public void FromShortString_ShortStringGuidRepresentationHasInvalidCharacters_ReturnsNull()
        {
            const string shortStringGuidRepresentation = "!AAAAAAAAAAAAAAAAAAAAA";
            Assert.That(shortStringGuidRepresentation.Length, Is.EqualTo(22));

            Assert.That(GuidUtility.FromShortString(shortStringGuidRepresentation).HasValue, Is.False);
        }

        [Test]
        public void FromShortString_ShortStringGuidRepresentationIsValid_ReturnsGuid()
        {
            const string shortStringGuidRepresentation = "AAAAAAAAAAAAAAAAAAAAAA";
            Assert.That(shortStringGuidRepresentation.Length, Is.EqualTo(22));

            Assert.That(GuidUtility.FromShortString(shortStringGuidRepresentation).HasValue, Is.True);
        }

        [Test]
        public void FromShortString_ReturnsSameGuidUsedToBuildTheShortStringRepresentation()
        {
            Guid guid = Guid.NewGuid();

            // ReSharper disable PossibleInvalidOperationException
            Assert.That(GuidUtility.FromShortString(GuidUtility.ToShortString(guid)).Value, Is.EqualTo(guid));
            // ReSharper restore PossibleInvalidOperationException
        }
    }
    // ReSharper restore InconsistentNaming
}
