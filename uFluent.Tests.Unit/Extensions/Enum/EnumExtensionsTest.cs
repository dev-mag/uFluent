using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using uFluent.Extensions.Enumeration;
using uFluent.Extensions.MultiNodeTreePicker.Enums;

namespace uFluent.Tests.Unit.Extensions.Enum
{
    [TestClass]
    public class EnumExtensionsTest
    {
        [TestMethod]
        public void GivenIHaveEnumTreeType_WhenGetDescriptionForValueContent_ThenReturnedStringIscontent()
        {
            var result = TreeType.Content.GetDescription();

            Assert.AreEqual("content", result);
        }

        [TestMethod]
        public void GivenIHaveValuemediaInTreeTypeEnum_WhenIGetValueFromDescription_ThenReturnedIsTreeTypeMedia()
        {
            var result = EnumExtensions.GetValueFromDescription<TreeType>("media");

            Assert.AreEqual(TreeType.Media, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GivenTreeTypeEnum_WhenIGetValueFromDescriptionRandom_ThenArgumentExceptionIsThrown()
        {
            try
            {
                var result = EnumExtensions.GetValueFromDescription<TreeType>("random");
                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("description", ex.ParamName);
                throw;
            }
        }
    }

}


