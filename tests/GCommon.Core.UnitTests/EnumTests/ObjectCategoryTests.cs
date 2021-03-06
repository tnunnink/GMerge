using FluentAssertions;
using GCommon.Core.Enumerations;
using NUnit.Framework;

namespace GCommon.Core.UnitTests.EnumTests
{
    [TestFixture]
    public class ObjectCategoryTests
    {
        [Test]
        public void FromValue_WhenCalled_ReturnsExpected()
        {
            var category = ObjectCategory.FromValue(0);

            category.Should().Be(ObjectCategory.Undefined);
        }
        
        [Test]
        public void Count_WhenCalled_ReturnsExpected()
        {
            var count = ObjectCategory.List.Count;

            count.Should().Be(24);
        }
    }
}