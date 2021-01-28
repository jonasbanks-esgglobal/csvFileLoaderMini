using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CSV_Loader_UI
{
    public static class SqlBuilder
    {
        public static bool SetUp(ref string errorMessage)
        {
            {
                try
                {
                    var connectionString = ConfigurationManager.ConnectionStrings["DBSConnection"].ConnectionString;
                    SetUpDataBase(connectionString);
                    SetUpCustomerTable(connectionString);
                    return true;
                }
                catch (Exception e)
                {
                    errorMessage = e.Message;
                    return false;
                }
            }
        }

        private static void SetUpDataBase(string connectionString)
        {
            string _createDatabaseIfNotExists = "USE master; IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'JB_MiniProject') BEGIN CREATE DATABASE[JB_MiniProject]; END;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(_createDatabaseIfNotExists, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        private static void SetUpCustomerTable(string connectionString)
        {
            string _createCustomerTableIfNotExists = "USE [JB_MiniProject]; IF OBJECT_ID('Customer','U') IS NULL BEGIN CREATE TABLE [Customer] (CustomerId INT IDENTITY(1,1) PRIMARY KEY,CustomerReference VARCHAR(8) NOT NULL,CustomerName VARCHAR(200) NOT NULL,AddressLine1 VARCHAR(200) NOT NULL,AddressLine2 VARCHAR(200),Town VARCHAR(200),Postcode VARCHAR(10) NOT NULL,County VARCHAR(200),Country VARCHAR(200),) END;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(_createCustomerTableIfNotExists, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
    }
}
