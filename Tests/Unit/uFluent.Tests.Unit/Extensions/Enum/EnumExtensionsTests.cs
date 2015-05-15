using FluentAssertions;
using NUnit.Framework;
using System;
using uFluent.Extensions.Enumeration;
using uFluent.Extensions.MultiNodeTreePicker.Enums;
using uFluent.Extensions.Tags.Enums;

namespace uFluent.Tests.Unit.Extensions.Enum
{
    [TestFixture]
    public class EnumExtensionsTests
    {
        [Test]
        public void GivenIHaveEnumTreeType_WhenGetDescriptionForVaueContent_ThenReturnedStringIsContent()
        {
            var result = TreeType.Content.GetDescription();
            result.Should().Be("content");
        }

        [Test]
        public void GivenIHaveEnumStorageType_WhenGetDescriptionForCsv_ThenReturnedStringIsNull()
        {
            var result = StorageType.Csv.GetDescription();
            result.Should().BeNull();
        }

        [Test]
        public void GivenIHaveValueMediaInTreeTypeEnum_WhenIGetValueFromDescription_ThenReturnedIsTreeTypeMedia()
        {
            var result = EnumExtensions.GetValueFromDescription<TreeType>("media");
            result.Should().Be(TreeType.Media);
        }

        [Test]
        [ExpectedException("System.ArgumentException")]
        public void GivenTreeTypeEnum_WhenIGetValueFromDescriptionRandom_ThenArgumentExceptionIsThrownWithParameterNameDescription()
        {
            try
            {
                var result = EnumExtensions.GetValueFromDescription<TreeType>("random");
                Assert.Fail("The argument exception should have been thrown as there is no tree type of random");
            }
            catch (ArgumentException ex)
            {
                ex.ParamName.Should().Be("description");
                throw;
            }
        }
    }
}
