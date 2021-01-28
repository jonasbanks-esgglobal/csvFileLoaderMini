using BusinessLayer.Models;
using DAL.DAL_Objects;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace BusinessLayer.BLObjects
{
    public class CustomerBL : Customer
    {
        private const int _customerReferenceMaxLength = 8;
        public CustomerBL()
        {
            NewRecord = true;
        }

        public bool LoadCustomerById(SqlConnection connection, int customerId, SqlTransaction transaction = null)
        {
            Select(connection, customerId, transaction);
            if (CustomerId > 0)
            {
                NewRecord = false;
                return true;
            }
            return false;
        }
        public bool LoadCustomerByRef(SqlConnection connection, string customerReference, SqlTransaction transaction = null)
        {
            if (!string.IsNullOrEmpty(customerReference))
            {
                customerReference = ClearCustomerRefOfNonNumerics(customerReference);
                customerReference = PadCustomerRefWithZeroes(customerReference);
                Select(connection, customerReference, transaction);
                if (CustomerId > 0)
                {
                    NewRecord = false;
                    return true;
                }
            }
            return false;
        }

        public void SaveCustomer(SqlConnection connection, SqlTransaction transaction = null)
        {
            if (!NewRecord)
            {
                Update(connection, transaction);
            }
            else
            {
                Insert(connection, transaction);
            }
        }

        public string PadCustomerRefWithZeroes(string customerReference)
        {
            return customerReference.PadLeft(_customerReferenceMaxLength, '0');
        }

        public string ClearCustomerRefOfNonNumerics(string customerReference)
        {
            return Regex.Replace(customerReference, "[^0-9.]", "");
        }

        public void FillBLFromCustomerModel(CustomerModel customerModel)
        {
            if(customerModel != null && !string.IsNullOrEmpty(customerModel.CustomerReference))
            {
                var custRef = customerModel.CustomerReference;
                custRef = ClearCustomerRefOfNonNumerics(custRef);
                custRef = PadCustomerRefWithZeroes(custRef);

                CustomerReference = custRef;
                CustomerName = customerModel.CustomerName;
                AddressLine1 = customerModel.AddressLine1;
                AddressLine2 = customerModel.AddressLine2;
                County = customerModel.County;
                Country = customerModel.Country;
                Postcode = customerModel.Postcode;
                Town = customerModel.Town;
            }
            else
            {
                throw new System.Exception("Error when converting the customerModel, Either null or no reference was set");
            }
        }
    }
}
