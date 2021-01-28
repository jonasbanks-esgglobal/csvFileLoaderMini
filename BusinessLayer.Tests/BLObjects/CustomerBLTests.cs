using System;
using System.Collections.Generic;
using BusinessLayer.BLObjects;
using BusinessLayer.Models;
using NUnit.Framework;

namespace BusinessLayer.Tests.BLObjects
{
    [TestFixture]
    public class CustomerBLTests
    {
        [TestCase(null)]
        [Test]
        public void FillBLFromCustomerModel_ModelWithNoCustomerRef_ThrowsExceptionErrorWhenConvertingCustomerModel(CustomerModel customerModel)
        {
            var SUT = new CustomerBL();

           var ex = Assert.Throws<Exception>(() => SUT.FillBLFromCustomerModel(customerModel));

            Assert.That(ex.Message, Is.EqualTo("Error when converting the customerModel, Either null or no reference was set"));
        }

        [TestCaseSource("FillBLFromCustomerModelTestCases")]
        [Test]
        public void FillBLFromCustomerModel_PopulatedModel_CustomerBLHasFieldsSetFromModel(CustomerModel customerModel,CustomerBL expectedResult)
        {
            var SUT = new CustomerBL();

            SUT.FillBLFromCustomerModel(customerModel);

            Assert.That(SUT.CustomerReference, Is.EqualTo(expectedResult.CustomerReference));
            Assert.That(SUT.AddressLine1, Is.EqualTo(expectedResult.AddressLine1));
            Assert.That(SUT.CustomerName, Is.EqualTo(expectedResult.CustomerName));
            Assert.That(SUT.AddressLine2, Is.EqualTo(expectedResult.AddressLine2));
        }

        [TestCase("00000024","000000|24")]
        [Test]
        public void ClearCustomerRefOfNonNumerics_customerRefHasIllegalCharacters_ReturnsSanitizedString(string expected,string customerRef)
        {
            var SUT = new CustomerBL();

            var res = SUT.ClearCustomerRefOfNonNumerics(customerRef);

            Assert.That(res, Is.EqualTo(expected));
        }

        [TestCase("00000024", "24")]
        [Test]
        public void PadCustomerRefWithZeroes_CustomerRefIsWithoutZeroes_CustomerRefIsPaddedToALengthOf8(string expected, string customerRef)
        {
            var SUT = new CustomerBL();

            var res = SUT.PadCustomerRefWithZeroes(customerRef);

            Assert.That(res, Is.EqualTo(expected));
        }

        public static IEnumerable<TestCaseData> FillBLFromCustomerModelTestCases
        {
            //Returns our test cases. Using this method let's you instantiate complex objects and send them in to a test as a parameter
            get
            {
                yield return new TestCaseData(new CustomerModel() {CustomerReference = "00000024",CustomerName="Nile Fergus",AddressLine1 = "12",AddressLine2="Populus St" },new CustomerBL() { CustomerReference = "00000024", CustomerName = "Nile Fergus", AddressLine1 = "12", AddressLine2 = "Populus St" });
            }
        }
    }
}
