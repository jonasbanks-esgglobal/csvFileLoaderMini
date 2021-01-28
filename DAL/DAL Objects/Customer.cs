using DAL.Helpers;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace DAL.DAL_Objects
{

    public class Customer
    {
        #region Inline SQL
        private const string _selectCustomerByCustomerRefSQL = "USE [JB_MiniProject]; SELECT [CustomerId],[CustomerReference],[CustomerName],[AddressLine1],[AddressLine2],[Town],[Postcode],[County],[Country] FROM [dbo].[Customer] WHERE [CustomerReference] = @customerReference";
        private const string _selectCustomerByCustomerIdSQL = "USE [JB_MiniProject]; SELECT [CustomerId],[CustomerReference],[CustomerName],[AddressLine1],[AddressLine2],[Town],[Postcode],[County],[Country] FROM [dbo].[Customer] WHERE [CustomerId] = @customerId";
        private const string _insertCustomerSQL = "USE [JB_MiniProject]; INSERT INTO [dbo].[Customer] ([CustomerReference],[CustomerName],[AddressLine1],[AddressLine2],[Town],[Postcode],[County],[Country])VALUES(@customerReference,@customerName,@addressLine1,@addressLine2,@town,@postcode,@county,@country);SET @customerId=IDENT_CURRENT('Customer')";
        private const string _updateCustomerSQL = "USE [JB_MiniProject]; UPDATE [dbo].[Customer] SET [CustomerName] = @customerName,[AddressLine1] = @addressLine1,[AddressLine2] = @addressLine2,[Town] = @town,[Postcode] = @postcode,[County] = @county,[Country] = @country WHERE CustomerReference = @customerReference";
        private const string _deleteCustomerSQL = "USE [JB_MiniProject]; DELETE FROM [dbo].[Customer] WHERE [CustomerId] = @customerId";
        #endregion

        private int _customerId;
        private string _customerReference;
        private string _customerName;
        private string _addressLine1;
        private string _addressLine2;
        private string _town;
        private string _county;
        private string _country;
        private string _postCode;
        private bool _newRecord;
        private bool _recordUpdated;

        public int CustomerId { get { return _customerId; } protected set { _customerId = value; } }
        public string CustomerReference { get { return _customerReference; } set { _customerReference = value; _recordUpdated = true; } }
        public string CustomerName { get { return _customerName; } set { _customerName = value; _recordUpdated = true; } }
        public string AddressLine1 { get { return _addressLine1; } set { _addressLine1 = value; _recordUpdated = true; } }
        public string AddressLine2 { get { return _addressLine2; } set { _addressLine2 = value; _recordUpdated = true; } }
        public string Town { get { return _town; } set { _town = value; _recordUpdated = true; } }
        public string County { get { return _county; } set { _county = value; _recordUpdated = true; } }
        public string Country { get { return _country; } set { _country = value; _recordUpdated = true; } }
        public string Postcode { get { return _postCode; } set { _postCode = value; _recordUpdated = true; } }
        public bool NewRecord { get { return _newRecord; } protected set { _newRecord = value; } }
        public bool RecordUpdated { get { return _recordUpdated; } protected set { _recordUpdated = value; } }


        public Customer()
        {
        }

        public void Select(SqlConnection connection, string customerReference, SqlTransaction transaction = null)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.Transaction = transaction;
            command.CommandType = CommandType.Text;
            command.CommandText = _selectCustomerByCustomerRefSQL;

            SqlParameter parameter = new SqlParameter("@customerReference", SqlDbType.VarChar);
            parameter.Size = 8;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = customerReference;
            command.Parameters.Add(parameter);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = command;
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                DataRow dataRow = dataSet.Tables[0].Rows[0];
                LoadDataRow(dataRow);
            }
        }

        public void Select(SqlConnection connection, int customerId, SqlTransaction transaction = null)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.Transaction = transaction;
            command.CommandType = CommandType.Text;
            command.CommandText = _selectCustomerByCustomerIdSQL;

            SqlParameter parameter = new SqlParameter("@customerId", SqlDbType.VarChar);
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = customerId;
            command.Parameters.Add(parameter);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = command;
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                DataRow dataRow = dataSet.Tables[0].Rows[0];
                LoadDataRow(dataRow);
            }
        }

        public void Insert(SqlConnection connection, SqlTransaction transaction = null)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.Transaction = transaction;
            command.CommandType = CommandType.Text;
            command.CommandText = _insertCustomerSQL;

            SqlParameter parameter = new SqlParameter("@customerReference", SqlDbType.VarChar);
            parameter.Size = 8;
            parameter.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(this.CustomerReference))
                throw new System.Exception("Customer reference can't be null");
            else
                parameter.Value = this.CustomerReference;
            command.Parameters.Add(parameter);

            SqlParameter parameter1 = new SqlParameter("@customerName", SqlDbType.VarChar);
            parameter.Size = 200;
            parameter.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(this.CustomerName))
                throw new System.Exception("Customer Name can't be null");
            else
                parameter1.Value = this.CustomerName;
            command.Parameters.Add(parameter1);

            SqlParameter parameter2 = new SqlParameter("@addressLine1", SqlDbType.VarChar);
            parameter.Size = 200;
            parameter.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(this.AddressLine1))
                throw new System.Exception("AddressLine 1 can't be null");
            else
                parameter2.Value = this.AddressLine1;
            command.Parameters.Add(parameter2);

            SqlParameter parameter3 = new SqlParameter("@addressLine2", SqlDbType.VarChar);
            parameter.Size = 200;
            parameter.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(this.AddressLine2))
                parameter3.Value = System.DBNull.Value;
            else
                parameter3.Value = this.AddressLine2;
            command.Parameters.Add(parameter3);

            SqlParameter parameter4 = new SqlParameter("@postcode", SqlDbType.VarChar);
            parameter.Size = 10;
            parameter.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(this.Postcode))
                throw new System.Exception("Postcode can't be null");
            else
                parameter4.Value = this.Postcode;
            command.Parameters.Add(parameter4);

            SqlParameter parameter5 = new SqlParameter("@town", SqlDbType.VarChar);
            parameter.Size = 200;
            parameter.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(this.Town))
                parameter5.Value = System.DBNull.Value;
            else
                parameter5.Value = this.Town;
            command.Parameters.Add(parameter5);

            SqlParameter parameter6 = new SqlParameter("@county", SqlDbType.VarChar);
            parameter.Size = 200;
            parameter.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(this.County))
                parameter6.Value = System.DBNull.Value;
            else
                parameter6.Value = this.County;
            command.Parameters.Add(parameter6);

            SqlParameter parameter7 = new SqlParameter("@country", SqlDbType.VarChar);
            parameter.Size = 200;
            parameter.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(this.Country))
                parameter7.Value = System.DBNull.Value;
            else
                parameter7.Value = this.Country;
            command.Parameters.Add(parameter7);


            SqlParameter parameter8 = new SqlParameter("@customerId", SqlDbType.Int);
            parameter8.Direction = ParameterDirection.Output;
            command.Parameters.Add(parameter8);

            command.ExecuteNonQuery();
            this.CustomerId = (int)(parameter8.Value);
        }

        public void Update(SqlConnection connection, SqlTransaction transaction = null)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.Transaction = transaction;
            command.CommandType = CommandType.Text;
            command.CommandText = _updateCustomerSQL;

            SqlParameter parameter = new SqlParameter("@customerReference", SqlDbType.VarChar);
            parameter.Size = 8;
            parameter.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(this.CustomerReference))
                throw new System.Exception("Customer reference can't be null");
            else
                parameter.Value = this.CustomerReference;
            command.Parameters.Add(parameter);

            SqlParameter parameter1 = new SqlParameter("@customerName", SqlDbType.VarChar);
            parameter.Size = 200;
            parameter.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(this.CustomerName))
                throw new System.Exception("Customer Name can't be null");
            else
                parameter1.Value = this.CustomerName;
            command.Parameters.Add(parameter1);

            SqlParameter parameter2 = new SqlParameter("@addressLine1", SqlDbType.VarChar);
            parameter.Size = 200;
            parameter.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(this.AddressLine1))
                throw new System.Exception("AddressLine 1 can't be null");
            else
                parameter2.Value = this.AddressLine1;
            command.Parameters.Add(parameter2);

            SqlParameter parameter3 = new SqlParameter("@addressLine2", SqlDbType.VarChar);
            parameter.Size = 200;
            parameter.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(this.AddressLine2))
                parameter3.Value = System.DBNull.Value;
            else
                parameter3.Value = this.AddressLine2;
            command.Parameters.Add(parameter3);

            SqlParameter parameter4 = new SqlParameter("@postcode", SqlDbType.VarChar);
            parameter.Size = 10;
            parameter.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(this.Postcode))
                throw new System.Exception("Postcode can't be null");
            else
                parameter4.Value = this.Postcode;
            command.Parameters.Add(parameter4);

            SqlParameter parameter5 = new SqlParameter("@town", SqlDbType.VarChar);
            parameter.Size = 200;
            parameter.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(this.Town))
                parameter5.Value = System.DBNull.Value;
            else
                parameter5.Value = this.Town;
            command.Parameters.Add(parameter5);

            SqlParameter parameter6 = new SqlParameter("@county", SqlDbType.VarChar);
            parameter.Size = 200;
            parameter.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(this.County))
                parameter6.Value = System.DBNull.Value;
            else
                parameter6.Value = this.County;
            command.Parameters.Add(parameter6);

            SqlParameter parameter7 = new SqlParameter("@country", SqlDbType.VarChar);
            parameter.Size = 200;
            parameter.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(this.Country))
                parameter7.Value = System.DBNull.Value;
            else
                parameter7.Value = this.Country;
            command.Parameters.Add(parameter7);

            SqlParameter parameter8 = new SqlParameter("@customerId", SqlDbType.Int);
            parameter8.Direction = ParameterDirection.Input;
            command.Parameters.Add(parameter8);

            command.ExecuteNonQuery();
        }
        public void Delete(SqlConnection connection, SqlTransaction transaction = null)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.Transaction = transaction;
            command.CommandType = CommandType.Text;
            command.CommandText = _selectCustomerByCustomerIdSQL;

            SqlParameter parameter = new SqlParameter("@customerId", SqlDbType.VarChar);
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = CustomerId;
            command.Parameters.Add(parameter);

            command.ExecuteNonQuery();
        }

        public void LoadDataRow(DataRow dataRow)
        {
            Type type = this.GetType();
            PropertyInfo[] properties = type.GetProperties();
            foreach (var property in properties)
            {

                if (dataRow.Table.Columns.Contains(property.Name))
                {
                    Type underlyingPropertyType = PropertyHelper.GetUnderlyingPropertyType(property.PropertyType);
                    if (underlyingPropertyType == typeof(string))
                    {
                        property.SetValue(this, dataRow.Field<string>(property.Name));
                    }
                    else if (underlyingPropertyType == typeof(int))
                    {
                        property.SetValue(this, dataRow.Field<int>(property.Name));
                    }

                }
            }
        }
    }
}
