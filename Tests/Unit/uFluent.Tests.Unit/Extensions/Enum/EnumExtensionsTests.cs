using System;
using FluentAssertions;
using NUnit.Framework;
using uFluent.Extensions.Enumeration;
using uFluent.Extensions.MultiNodeTreePicker.Enums;
using uFluent.Extensions.Tags.Enums;

namespace uFluent.Tests.Unit.Extensions.Enum
{
    [TestFixture]
    public class EnumExtensionsTests
    {
        [Test]
        public void GivenIHaveEnumNodeType_WhenGetDescriptionForVaueContent_ThenReturnedStringIsContent()
        {
            var result = NodeType.Content.GetDescription();
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
            var result = EnumExtensions.GetValueFromDescription<NodeType>("media");
            result.Should().Be(NodeType.Media);
        }

        [Test]
        public void GivenTreeTypeEnum_WhenIGetValueFromDescriptionRandom_ThenArgumentExceptionIsThrownWithParameterNameDescription()
        {
            Action act = () => EnumExtensions.GetValueFromDescription<NodeType>("random");
            act.ShouldThrow<ArgumentException>("description").And.ParamName.Should().Be("description");
        }
    }
}