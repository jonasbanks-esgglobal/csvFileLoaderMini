using BusinessLayer.Models;
using BusinessLayer.BLObjects;
using DAL.DAL_Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("API/Customer")]
    public class CustomerController : ApiController
    {
        public CustomerController()
        {
        }

        [HttpGet]
        [Route("GetCustomerByRef")]
        public HttpResponseMessage Get(string customerReference)
        {
            HttpResponseMessage response = null;
            CustomerBL customerBL = new CustomerBL();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBSConnection"].ConnectionString);
            try
            {
                customerBL.LoadCustomerByRef(connection,customerReference);
                if (!string.IsNullOrEmpty(customerBL.CustomerReference))
                {
                    response = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(customerBL), System.Text.Encoding.UTF8, "application/json")
                    };
                }
                else
                {
                    response = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("Customer not found", System.Text.Encoding.UTF8, "application/json")
                    };
                }
            }
            catch (Exception e)
            {
                response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"Error searching for customer reference {customerReference}: {e.Message}.", System.Text.Encoding.UTF8, "application/json")
                };
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return response;
        }

        // POST api/<controller>
        public HttpResponseMessage Post(JObject customerInformation)
        {
            HttpResponseMessage response = null;
            if (customerInformation != null)
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBSConnection"].ConnectionString);
                try
                {
                    var customerModel = customerInformation.ToObject<CustomerModel>();
                    var customerBL = new CustomerBL();
                    customerBL.LoadCustomerByRef(connection, customerModel.CustomerReference);
                    customerBL.FillBLFromCustomerModel(customerModel);

                    using (connection)
                    {
                        connection.Open();
                        customerBL.SaveCustomer(connection);
                    }
                        response = new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            Content = new StringContent($"Customer: {customerBL.CustomerReference}, saved successfully", System.Text.Encoding.UTF8, "application/json")
                        };
                }
                catch (Exception e)
                {
                    response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent($"Error saving customer: {e.Message}.", System.Text.Encoding.UTF8, "application/json")
                    };
                }
                finally {
                    connection.Close();
                    connection.Dispose();
                }

            }
            else
            {
                response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"There was something wrong with the Json object sent to the API end point, obj: {customerInformation}", System.Text.Encoding.UTF8, "application/json")
                };
            }
            return response;
        }

    }
}