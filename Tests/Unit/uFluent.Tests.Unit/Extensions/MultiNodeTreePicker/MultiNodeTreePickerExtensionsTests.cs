using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using uFluent.Extensions.MultiNodeTreePicker;
using uFluent.Extensions.MultiNodeTreePicker.Enums;
using uFluent.Extensions.MultiNodeTreePicker.Models;

namespace uFluent.Tests.Unit.Extensions.MultiNodeTreePicker
{
    [TestFixture]
    public class MultiNodeTreePickerExtensionsTests
    {
        private readonly string[] _dummyRawPreValues =
        {
            @"{ type:""content"", query:""$site"" }",
            "Homepage,Article",
            null,
            null,
            "1"
        };

        [Test]
        public void GivenNoQueryInStartNodeJson_WhenGetMultiNodeTreePickerValues_PropertiesAreMappedCorrectly()
        {
            var dummyRawPreValues = new[]
            {
                @"{ type:""content"" }",
                "Homepage,Article",
                "1",
                "10",
                "1"
            };

            var mock = new Mock<IDataType>();
            mock.Setup(x => x.GetDataTypePreValues()).Returns(dummyRawPreValues);

            var dummyMNTP = mock.Object;

            var results = dummyMNTP.GetMultiNodeTreePickerPreValues();

            results.StartNode.StartNodeType.Should().Be(NodeType.Content);
            results.StartNodeXPathFilter.Should().Be(null);
            results.AllowedDocTypes.Should().Be("Homepage,Article");
            results.MinSelectedNodes.Should().Be(1);
            results.MaxSelectedNodes.Should().Be(10);
            results.ShowEditButton.Should().BeTrue();
        }

        [Test]
        public void GivenPreValuesAreNotInAValidFormat_WhenGetMultiNodeTreePickerValues_FluentExceptionIsThrown()
        {
            var dummyInvalidPreValues = new[]
            {
                @"{type:""content""}",
                "1"
            };

            var mock = new Mock<IDataType>();
            mock.Setup(x => x.GetDataTypePreValues()).Returns(dummyInvalidPreValues);

            var dummyMNTP = mock.Object;

            Action a = () => dummyMNTP.GetMultiNodeTreePickerPreValues();
            a.ShouldThrow<FluentException>();
        }

        [Test]
        public void GivenStartNodeHasBothTypeAndXPath_WhenToString_ResultIsJsonWithTypeContentQuerySite()
        {
            var startNode = new StartNode(NodeType.Content, null, "$site");
            var result = startNode.ToJsonString();
            result.Should().Be(@"{""type"":""content"",""query"":""$site""}");
        }

        [Test]
        public void GivenStartNodeHasTypeButNoXPath_WhenToString_ResultIsJsonWithTypeContent()
        {
            var startNode = new StartNode(NodeType.Content, null, null);
            var result = startNode.ToJsonString();
            result.Should().Be(@"{""type"":""content""}");
        }

        [Test]
        public void WhenGetMultiNodeTreePickerPreValues_PropertiesAreMappedCorrectly()
        {
            var mock = new Mock<IDataType>();
            mock.Setup(x => x.GetDataTypePreValues()).Returns(_dummyRawPreValues);

            var dummyMNTP = mock.Object;

            var results = dummyMNTP.GetMultiNodeTreePickerPreValues();

            results.StartNode.StartNodeType.Should().Be(NodeType.Content);
            results.StartNode.XPathFilter.Should().Be("$site");
            results.AllowedDocTypes.Should().Be("Homepage,Article");
            results.MinSelectedNodes.Should().Be(null);
            results.MaxSelectedNodes.Should().Be(null);
            results.ShowEditButton.Should().BeTrue();
        }
    }
}