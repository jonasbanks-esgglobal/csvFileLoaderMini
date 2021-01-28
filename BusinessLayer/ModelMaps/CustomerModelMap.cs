using BusinessLayer.Models;
using CsvHelper.Configuration;
using System.Globalization;

namespace BusinessLayer.ModelMaps
{
    public sealed class CustomerModelMap : ClassMap<CustomerModel>
    {
        public CustomerModelMap()
        {
            Map(m => m.CustomerReference);
            Map(m => m.CustomerName);
            Map(m => m.AddressLine1);
            Map(m => m.AddressLine2);
            Map(m => m.Town);
            Map(m => m.County);
            Map(m => m.Country);
            Map(m => m.Postcode);
        }

    }
}