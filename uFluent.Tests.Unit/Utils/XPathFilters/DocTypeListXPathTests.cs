using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using uFluent.Utils.XPathFilters;

namespace uFluent.Tests.Unit.Utils.XPathFilters
{
    [TestClass]
    public class DocTypeListXPathTests
    {
        [TestMethod]
        public void GivenIHaveOneDocTypeInFilter_WhenIAddAnother_ThenThereWillBeTwoDocTypesPipeDelimited()
        {
            var docTypeListXPath = new DocTypeListXPath(DocTypeListXPathTestsConsts.OneDocTypeDocListXPath);

            docTypeListXPath.Add("ContentPage");

            Assert.AreEqual("Homepage | ContentPage", docTypeListXPath.ToString());
        }

        [TestMethod]
        public void GivenXPathFilterHasNoDocTypes_WhenIAddAHomepageDocType_ThenXPathFilterReturnedWillHaveHomepage()
        {
            var docTypeListXPath = new DocTypeListXPath();

            docTypeListXPath.Add("Homepage");

            Assert.AreEqual("Homepage", docTypeListXPath.ToString());
        }

        [TestMethod]
        public void GivenXPathFilterHasExistingDocType_WhenITryAddingEmptyStringAsDocType_ThenFluentExceptionWillBeRaised()
        {
            try
            {
                var docTypeListXPath = new DocTypeListXPath(DocTypeListXPathTestsConsts.OneDocTypeDocListXPath);

                docTypeListXPath.Add("");

                Assert.Fail();
            }
            catch(FluentException ex)
            {
                Assert.AreEqual(DocTypeListXPathTestsConsts.DocTypeBeingAddedIsEmptyOrNull, ex.Message);
            }
            catch(Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void GivenXPathFilterHasExistingDocType_WhenITryAddingNullStringAsDocType_ThenFluentExceptionWillBeRaised()
        {
            try
            {
                var docTypeListXPath = new DocTypeListXPath(DocTypeListXPathTestsConsts.OneDocTypeDocListXPath);

                docTypeListXPath.Add(null);

                Assert.Fail();
            }
            catch (FluentException ex)
            {
                Assert.AreEqual(DocTypeListXPathTestsConsts.DocTypeBeingAddedIsEmptyOrNull, ex.Message);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void GivenXPathFilterHasExistingDocType_WhenITryAddingStringEmptyAsDocType_ThenFluentExceptionWillBeRaised()
        {
            try
            {
                var docTypeListXPath = new DocTypeListXPath(DocTypeListXPathTestsConsts.OneDocTypeDocListXPath);

                docTypeListXPath.Add(string.Empty);

                Assert.Fail();
            }
            catch (FluentException ex)
            {
                Assert.AreEqual(DocTypeListXPathTestsConsts.DocTypeBeingAddedIsEmptyOrNull, ex.Message);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FluentException))]
        public void WhenITryAndAddADocTypeAliasThatAlreadyExists_ThenFluentExceptionWillBeRaisedWithAppropriateMessage()
        {
            try
            {
                var docTypeListXPath = new DocTypeListXPath(DocTypeListXPathTestsConsts.OneDocTypeDocListXPath);

                docTypeListXPath.Add("Homepage");

                Assert.Fail();
            }
            catch (FluentException ex)
            {
                Assert.AreEqual(DocTypeListXPathTestsConsts.DocTypeBeingAddedAlreadyExists, ex.Message);
                throw;
            }
        }

        [TestMethod]
        public void GivenXPathFilterHasHomepageAndContentPage_WhenIRemoveContentPage_ThenXPathFilterReturnedWillOnlyHaveHomepage()
        {
            var docTypeListXPath = new DocTypeListXPath(DocTypeListXPathTestsConsts.TwoDocTypeDocListXPath);

            docTypeListXPath.Remove("ContentPage");

            Assert.AreEqual("Homepage", docTypeListXPath.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(FluentException))]
        public void GivenXPathFilterHasHomepageAndContentPage_WhenIRemoveEmptyString_ThenXPathFilterReturnedWillOnlyHaveHomepage()
        {
            try
            {
                var docTypeListXPath = new DocTypeListXPath(DocTypeListXPathTestsConsts.TwoDocTypeDocListXPath);

                docTypeListXPath.Remove("");

                Assert.Fail();
            }
            catch(FluentException ex)
            {
                Assert.AreEqual(DocTypeListXPathTestsConsts.DocTypeBeingRemovedIsEmptyOrNull, ex.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FluentException))]
        public void GivenXPathFilterHasHomepageAndContentPage_WhenIRemoveNull_ThenXPathFilterReturnedWillOnlyHaveHomepage()
        {
            try
            {
                var docTypeListXPath = new DocTypeListXPath(DocTypeListXPathTestsConsts.TwoDocTypeDocListXPath);

                docTypeListXPath.Remove(null);
            }
            catch(FluentException ex)
            {
                Assert.AreEqual(DocTypeListXPathTestsConsts.DocTypeBeingRemovedIsEmptyOrNull, ex.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FluentException))]
        public void GivenXPathFilterHasHomepageAndContentPage_WhenIRemoveStringEmpty_ThenXPathFilterReturnedWillOnlyHaveHomepage()
        {
            var docTypeListXPath = new DocTypeListXPath(DocTypeListXPathTestsConsts.TwoDocTypeDocListXPath);

            docTypeListXPath.Remove(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(FluentException))]
        public void WhenTryToRemoveDocTypeThatDoesntExistInFilter_ThenFluentExceptionIsRaised()
        {
            try
            {
                var docTypeListXPath = new DocTypeListXPath(DocTypeListXPathTestsConsts.TwoDocTypeDocListXPath);
                docTypeListXPath.Remove("RandomDocType");
            }
            catch(FluentException ex)
            {
                Assert.AreEqual(DocTypeListXPathTestsConsts.DocTypeBeingRemovedDoesntExist, ex.Message);
                throw;
            }
        }

        [TestMethod]
        public void GivenXPathFilterHasOneDocTypeAndNoSpecialCharacters_WhenTryParseXPathFilter_ThenReturnTrue()
        {
            var docTypeListXPath = new DocTypeListXPath();

            var canParse = docTypeListXPath.IsValid(DocTypeListXPathTestsConsts.OneDocTypeDocListXPath);

            Assert.IsTrue(canParse);
        }

        [TestMethod]
        public void GivenXPathFilterHasOneDocTypesAndSpecialCharacters_WhenTryParseXPathFilter_ThenReturnFalse()
        {
            var docTypeListXPath = new DocTypeListXPath();

            var canParse = docTypeListXPath.IsValid(DocTypeListXPathTestsConsts.OneDocTypeSpecialCharacters);

            Assert.IsFalse(canParse);
        }

        [TestMethod]
        public void GivenXPathFilterHasTwoDocTypesPipeDelimitedAndNoSpecialCharacters_WhenTryParseXPathFilter_ThenReturnTrue()
        {
            var docTypeListXPath = new DocTypeListXPath();

            var canParse = docTypeListXPath.IsValid(DocTypeListXPathTestsConsts.TwoDocTypeDocListXPath);

            Assert.IsTrue(canParse);
        }

        [TestMethod]
        public void GivenXPathFilterHasTwoDocTypesPipeDelimitedAndSpecialCharacters_WhenTryParseXPathFilter_ThenReturnFalse()
        {
            var docTypeListXPath = new DocTypeListXPath();

            var canParse = docTypeListXPath.IsValid(DocTypeListXPathTestsConsts.TwoDocTypeSpecialCharacters);

            Assert.IsFalse(canParse);
        }

        [TestMethod]
        public void GivenXPathFilterIsTwoDocTypeNonPipeDelimited_WhenTryParseXPathFilter_ThenReturnFalse()
        {
            var docTypeListXPath = new DocTypeListXPath();

            var canParse = docTypeListXPath.IsValid(DocTypeListXPathTestsConsts.TwoDocTypeNonPipeDelimited);

            Assert.IsFalse(canParse);
        }

        [TestMethod]
        public void GivenXPathFilterHasThreeDocTypeDocListXPath_WhenTryParseXPathFilter_ThenReturnTrue()
        {
            var docTypeListXPath = new DocTypeListXPath();

            var canParse = docTypeListXPath.IsValid(DocTypeListXPathTestsConsts.ThreeDocTypeDocListXPath);

            Assert.IsTrue(canParse);
        }

        [TestMethod]
        public void GivenXPathFilterHasThreeDocTypeSpecialCharacter_WhenTryParseXPathFilter_ThenReturnFalse()
        {
            var docTypeListXPath = new DocTypeListXPath();

            var canParse = docTypeListXPath.IsValid(DocTypeListXPathTestsConsts.ThreeDocTypeSpecialCharacter);

            Assert.IsFalse(canParse);
        }

        [TestMethod]
        public void GivenXPathFilterHasThreeDocTypeLastNodeNonPipeDelimited_WhenTryParseXPathFilter_ThenReturnFalse()
        {
            var docTypeListXPath = new DocTypeListXPath();

            var canParse = docTypeListXPath.IsValid(DocTypeListXPathTestsConsts.ThreeDocTypeLastNodeNonPipeDelimited);

            Assert.IsFalse(canParse);
        }

        [TestMethod]
        public void GivenDocTypeXPathFilterHasOneDocTypeWithUnderscore_WhenICallIsValid_ResultIsTrue()
        {
            var docTypeXPathFilter = new DocTypeListXPath();

            var result = docTypeXPathFilter.IsValid(DocTypeListXPathTestsConsts.OneDocTypeUnderScore);
            
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenDocTypeXPathFilterHasTwoDocTypeWithUnderscore_WhenICallIsValid_ResultIsTrue()
        {
            var docTypeXPathFilter = new DocTypeListXPath();

            var result = docTypeXPathFilter.IsValid(DocTypeListXPathTestsConsts.TwoDocTypeUnderscore);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenDocTypeXPathFilterHasTwoDocTypeSecondWithUnderscore_WhenCallIsValid_ResultIsTrue()
        {
            var docTypeXPathFilter = new DocTypeListXPath();

            var result = docTypeXPathFilter.IsValid(DocTypeListXPathTestsConsts.TwoDocTypeSecondUnderscore);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(FluentException))]
        public void GivenDocTypeWithSpecialCharacter_WhenIAddToXPathFilterWithOneExistingDocType_ThenExceptionIsThrown()
        {
            try
            {
                var docTypeXPathFilter = new DocTypeListXPath("Homepage");

                docTypeXPathFilter.Add("Current$Page");
            }
            catch(FluentException ex)
            {
                Assert.AreEqual(DocTypeListXPathTestsConsts.AddDocTypeWithSpecialCharacterExceptionMessage, ex.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FluentException))]
        public void GivenDocTypeWithSpecialCharacter_WhenIRemoveFromXPathFilterWithOneExistingDocType_ThenExceptionIsThrown()
        {
            try
            {
                var docTypeXPathFilter = new DocTypeListXPath("Homepage");

                docTypeXPathFilter.Remove("Current$Page");
            }
            catch(FluentException ex)
            {
                Assert.AreEqual(DocTypeListXPathTestsConsts.RemoveDocTypeWithSpecialCharacterExceptionMessage, ex.Message);
                throw;
            }
        }
    }
}