using System;
using FluentAssertions;
using NUnit.Framework;
using uFluent.Utils.XPathFilters;

namespace uFluent.Tests.Unit.Utils.XPathFilters
{
    [TestFixture]
    public class DocTypeListXPathTests
    {
        [Test]
        public void GivenDocTypeWithSpecialCharacter_WhenIAddToXPathFilterWithOneExistingDocType_ThenExceptionIsThrown()
        {
            var docTypeXPathFilter = new DocTypeListXPath("Homepage");
            Action a = () => docTypeXPathFilter.Add("Current$Page");
            a.ShouldThrow<FluentException>().And.Message.Should().Be(DocTypeListXPathTestsConsts.AddDocTypeWithSpecialCharacterExceptionMessage);
        }

        [Test]
        public void GivenDocTypeWithSpecialCharacter_WhenIRemoveFromXPathFilterWithOneExistingDocType_ThenExceptionIsThrown()
        {
            var docTypeXPathFilter = new DocTypeListXPath("Homepage");
            Action a = () => docTypeXPathFilter.Remove("Current$Page");
            a.ShouldThrow<FluentException>().And.Message.Should().Be(DocTypeListXPathTestsConsts.RemoveDocTypeWithSpecialCharacterExceptionMessage);
        }

        [Test]
        public void GivenDocTypeXPathFilterHasOneDocTypeWithUnderscore_WhenICallIsValid_ResultIsTrue()
        {
            var docTypeXPathFilter = new DocTypeListXPath();
            var result = docTypeXPathFilter.IsValid(DocTypeListXPathTestsConsts.OneDocTypeUnderScore);
            result.Should().BeTrue();
        }

        [Test]
        public void GivenDocTypeXPathFilterHasTwoDocTypeSecondWithUnderscore_WhenCallIsValid_ResultIsTrue()
        {
            var docTypeXPathFilter = new DocTypeListXPath();
            var result = docTypeXPathFilter.IsValid(DocTypeListXPathTestsConsts.TwoDocTypeSecondUnderscore);
            result.Should().BeTrue();
        }

        [Test]
        public void GivenDocTypeXPathFilterHasTwoDocTypeWithUnderscore_WhenICallIsValid_ResultIsTrue()
        {
            var docTypeXPathFilter = new DocTypeListXPath();
            var result = docTypeXPathFilter.IsValid(DocTypeListXPathTestsConsts.TwoDocTypeUnderscore);
            result.Should().BeTrue();
        }

        [Test]
        public void GivenIHaveOneDocTypeInFilter_WhenIAddAnother_ThenThereWillBeTwoDocTypesPipeDelimited()
        {
            var docTypeListXPath = new DocTypeListXPath(DocTypeListXPathTestsConsts.OneDocTypeDocListXPath);
            docTypeListXPath.Add("ContentPage");
            docTypeListXPath.ToString().Should().Be("Homepage,ContentPage");
        }

        [Test]
        public void GivenXPathFilterHasExistingDocType_WhenITryAddingEmptyStringAsDocType_ThenFluentExceptionWillBeRaised()
        {
            var docTypeListXPath = new DocTypeListXPath(DocTypeListXPathTestsConsts.OneDocTypeDocListXPath);
            Action a = () => docTypeListXPath.Add("");
            a.ShouldThrow<FluentException>().And.Message.Should().Be(DocTypeListXPathTestsConsts.DocTypeBeingAddedIsEmptyOrNull);
        }

        [Test]
        public void GivenXPathFilterHasExistingDocType_WhenITryAddingNullAsDocType_ThenFluentExceptionWillBeRaised()
        {
            var docTypeListXPath = new DocTypeListXPath(DocTypeListXPathTestsConsts.OneDocTypeDocListXPath);
            Action a = () => docTypeListXPath.Add(null);
            a.ShouldThrow<FluentException>();
        }

        [Test]
        public void GivenXPathFilterHasExistingDocType_WhenITryAddingStringEmptyAsDocType_ThenFluentExceptionWillBeRaised()
        {
            var docTypeListXPath = new DocTypeListXPath(DocTypeListXPathTestsConsts.OneDocTypeDocListXPath);
            Action a = () => docTypeListXPath.Add(string.Empty);
            a.ShouldThrow<FluentException>().And.Message.Should().Be(DocTypeListXPathTestsConsts.DocTypeBeingAddedIsEmptyOrNull);
        }

        [Test]
        public void GivenXPathFilterHasHomepageAndContentPage_WhenIRemoveContentPage_ThenXPathFilterReturnedWillOnlyHaveHomepage()
        {
            var docTypeListXPath = new DocTypeListXPath(DocTypeListXPathTestsConsts.OneDocTypeDocListXPath);
            Action a = () => docTypeListXPath.Remove("");
            a.ShouldThrow<FluentException>().And.Message.Should().Be(DocTypeListXPathTestsConsts.DocTypeBeingRemovedIsEmptyOrNull);
        }

        [Test]
        public void GivenXPathFilterHasHomepageAndContentPage_WhenIRemoveEmptyString_ThenXPathFilterReturnedWillOnlyHaveHomepage()
        {
            var docTypeListXPath = new DocTypeListXPath(DocTypeListXPathTestsConsts.OneDocTypeDocListXPath);
            Action a = () => docTypeListXPath.Add("");
            a.ShouldThrow<FluentException>().And.Message.Should().Be(DocTypeListXPathTestsConsts.DocTypeBeingAddedIsEmptyOrNull);
        }

        [Test]
        public void GivenXPathFilterHasHomepageAndContentPage_WhenIRemoveNull_ThenXPathFilterReturnedWillOnlyHaveHomepage()
        {
            var docTypeListXPath = new DocTypeListXPath(DocTypeListXPathTestsConsts.TwoDocTypeDocListXPath);
            Action a = () => docTypeListXPath.Remove(null);
            a.ShouldThrow<FluentException>();
        }

        [Test]
        public void GivenXPathFilterHasHomepageAndContentPage_WhenIRemoveStringEmpty_ThenXPathFilterReturnedWillOnlyHaveHomepage()
        {
            var docTypeListXPath = new DocTypeListXPath(DocTypeListXPathTestsConsts.TwoDocTypeDocListXPath);
            Action a = () => docTypeListXPath.Remove(string.Empty);
            a.ShouldThrow<FluentException>().And.Message.Should().Be(DocTypeListXPathTestsConsts.DocTypeBeingRemovedIsEmptyOrNull);
        }

        [Test]
        public void GivenXPathFilterHasNoDocTypes_WhenIAddAHomepageDocType_ThenXPathFilterReturnedWillHaveHomepage()
        {
            var docTypeListXPath = new DocTypeListXPath();
            docTypeListXPath.Add("Homepage");
            docTypeListXPath.ToString().Should().Be("Homepage");
        }

        [Test]
        public void GivenXPathFilterHasOneDocTypeAndNoSpecialCharacters_WhenTryParseXPathFilter_ThenReturnTrue()
        {
            var docTypeListXPath = new DocTypeListXPath();
            var canParse = docTypeListXPath.IsValid(DocTypeListXPathTestsConsts.OneDocTypeDocListXPath);
            canParse.Should().BeTrue();
        }

        [Test]
        public void GivenXPathFilterHasOneDocTypesAndSpecialCharacters_WhenTryParseXPathFilter_ThenReturnFalse()
        {
            var docTypeListXPath = new DocTypeListXPath();
            var canParse = docTypeListXPath.IsValid(DocTypeListXPathTestsConsts.OneDocTypeSpecialCharacters);
            canParse.Should().BeFalse();
        }

        [Test]
        public void GivenXPathFilterHasThreeDocTypeDocListXPath_WhenTryParseXPathFilter_ThenReturnTrue()
        {
            var docTypeListXPath = new DocTypeListXPath();
            var canParse = docTypeListXPath.IsValid(DocTypeListXPathTestsConsts.ThreeDocTypeDocListXPath);
            canParse.Should().BeTrue();
        }

        [Test]
        public void GivenXPathFilterHasThreeDocTypeLastNodeNonPipeDelimited_WhenTryParseXPathFilter_ThenReturnFalse()
        {
            var docTypeListXPath = new DocTypeListXPath();
            var canParse = docTypeListXPath.IsValid(DocTypeListXPathTestsConsts.ThreeDocTypeLastNodeNonPipeDelimited);
            canParse.Should().BeFalse();
        }

        [Test]
        public void GivenXPathFilterHasThreeDocTypeSpecialCharacter_WhenTryParseXPathFilter_ThenReturnFalse()
        {
            var docTypeListXPath = new DocTypeListXPath();
            var canParse = docTypeListXPath.IsValid(DocTypeListXPathTestsConsts.ThreeDocTypeSpecialCharacter);
            canParse.Should().BeFalse();
        }

        [Test]
        public void GivenXPathFilterHasTwoDocTypesPipeDelimitedAndNoSpecialCharacters_WhenTryParseXPathFilter_ThenReturnTrue()
        {
            var docTypeListXPath = new DocTypeListXPath();
            var canParse = docTypeListXPath.IsValid(DocTypeListXPathTestsConsts.TwoDocTypeDocListXPath);
            canParse.Should().BeTrue();
        }

        [Test]
        public void GivenXPathFilterHasTwoDocTypesPipeDelimitedAndSpecialCharacters_WhenTryParseXPathFilter_ThenReturnFalse()
        {
            var docTypeListXPath = new DocTypeListXPath();
            var canParse = docTypeListXPath.IsValid(DocTypeListXPathTestsConsts.TwoDocTypeSpecialCharacters);
            canParse.Should().BeFalse();
        }

        [Test]
        public void GivenXPathFilterIsEmpty_WhenAddHomepageAndCategory_ThenResultIsHomepageCategoryInCorrectFormat()
        {
            var docTypeListXPath = new DocTypeListXPath();
            docTypeListXPath.Add("Homepage", "Category");
            var result = docTypeListXPath.ToString();

            result.Should().Be("Homepage,Category");
        }

        [Test]
        public void GivenXPathFilterIsHomepageCategory_WhenRemoveHomepageAndCategory_ThenResultIsEmptyString()
        {
            var docTypeListXPath = new DocTypeListXPath("Homepage,Category");
            docTypeListXPath.Remove("Homepage", "Category");
            var result = docTypeListXPath.ToString();

            result.Should().Be(string.Empty);
        }

        [Test]
        public void GivenXPathFilterIsHomepageCategoryArticle_WhenITryRemoveCategoryArticle_ResultIsHomepage()
        {
            var docTypeListXPath = new DocTypeListXPath("Homepage,Category,Article");
            docTypeListXPath.Remove("Category", "Article");
            var result = docTypeListXPath.ToString();

            result.Should().Be("Homepage");
        }

        [Test]
        public void GivenXPathFilterIsTwoDocTypeNonPipeDelimited_WhenTryParseXPathFilter_ThenReturnFalse()
        {
            var docTypeListXPath = new DocTypeListXPath();
            var canParse = docTypeListXPath.IsValid(DocTypeListXPathTestsConsts.TwoDocTypeNonPipeDelimited);
            canParse.Should().BeFalse();
        }

        [Test]
        public void WhenITryAddThreeDocTypesToXPath_ResultIsHomepageCategoryArticle()
        {
            var docTypeListXPath = new DocTypeListXPath();
            docTypeListXPath.Add("Homepage", "Category", "Article");
            var result = docTypeListXPath.ToString();

            result.Should().Be("Homepage,Category,Article");
        }

        [Test]
        public void WhenITryAndAddADocTypeAliasThatAlreadyExists_ThenFluentExceptionWillBeRaisedWithAppropriateMessage()
        {
            var docTypeListXPath = new DocTypeListXPath(DocTypeListXPathTestsConsts.OneDocTypeDocListXPath);
            Action a = () => docTypeListXPath.Add("Homepage");
            a.ShouldThrow<FluentException>().And.Message.Should().Be(DocTypeListXPathTestsConsts.DocTypeBeingAddedAlreadyExists);
        }

        [Test]
        public void WhenTryAddTheSameDocTypeAtOnce_ThenFluentExceptionISThrown()
        {
            var docTypeListXPath = new DocTypeListXPath();
            Action a = () => docTypeListXPath.Add("Homepage", "Homepage");
            a.ShouldThrow<FluentException>().And.Message.Should().Be(DocTypeListXPathTestsConsts.DocTypeBeingAddedAlreadyExists);
        }

        [Test]
        public void WhenTryAddTwoDocTypesAndOneIsEmptyString_ThenFluentExceptionIsThrown()
        {
            var docTypeListXPath = new DocTypeListXPath();
            Action a = () => docTypeListXPath.Add("Homepage", "");
            a.ShouldThrow<FluentException>().And.Message.Should().Be(DocTypeListXPathTestsConsts.DocTypeBeingAddedIsEmptyOrNull);
        }

        [Test]
        public void WhenTryAddTwoDocTypesAndOneIsStringDotEmpty_ThenFluentExceptionIsThrown()
        {
            var docTypeListXPath = new DocTypeListXPath();
            Action a = () => docTypeListXPath.Add("Homepage", string.Empty);
            a.ShouldThrow<FluentException>().And.Message.Should().Be(DocTypeListXPathTestsConsts.DocTypeBeingAddedIsEmptyOrNull);
        }

        [Test]
        public void WhenTryRemoveTheSameDocTypeAtOnce_ThenFluentExceptionIsThrownWithMessageThatDocTypeDoesntExist()
        {
            var docTypeListXPath = new DocTypeListXPath();
            Action a = () => docTypeListXPath.Remove("Homepage", "Homepage");
            a.ShouldThrow<FluentException>().And.Message.Should().Be(DocTypeListXPathTestsConsts.DocTypeBeingRemovedDoesntExist);
        }

        [Test]
        public void WhenTryRemoveTwoDocTypesAndOneIsEmptyString_ThenFluentExceptionIsThrown()
        {
            var docTypeListXPath = new DocTypeListXPath("Homepage,Category");
            Action a = () => docTypeListXPath.Remove("Category", "");
            a.ShouldThrow<FluentException>().And.Message.Should().Be(DocTypeListXPathTestsConsts.DocTypeBeingRemovedIsEmptyOrNull);
        }

        [Test]
        public void WhenTryRemoveTwoDocTypesAndOneIsStringDotEmpty_ThenFluentExceptionIsThrown()
        {
            var docTypeListXPath = new DocTypeListXPath("Homepage,Category");
            Action a = () => docTypeListXPath.Remove("Category", string.Empty);
            a.ShouldThrow<FluentException>().And.Message.Should().Be(DocTypeListXPathTestsConsts.DocTypeBeingRemovedIsEmptyOrNull);
        }

        [Test]
        public void WhenTryToRemoveDocTypeThatDoesntExistInFilter_ThenFluentExceptionIsRaised()
        {
            var docTypeListXPath = new DocTypeListXPath(DocTypeListXPathTestsConsts.TwoDocTypeDocListXPath);
            Action a = () => docTypeListXPath.Remove("RandomDocType");
            a.ShouldThrow<FluentException>().And.Message.Should().Be(DocTypeListXPathTestsConsts.DocTypeBeingRemovedDoesntExist);
        }
    }
}