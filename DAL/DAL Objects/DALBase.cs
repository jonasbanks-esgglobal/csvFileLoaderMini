using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALBase
    {
        public bool _newRecord;
        public bool _recordUpdated;

        public virtual void Select(SqlConnection connection, string customerReference, SqlTransaction transaction)
        {
        }
        public virtual void Select(SqlConnection connection, int customerReference, SqlTransaction transaction)
        {
        }
        public virtual void Insert(SqlConnection connection, SqlTransaction transaction)
        {
        }
        protected virtual void Delete(SqlConnection connection, SqlTransaction transaction)
        {
        }
        protected virtual void Update(SqlConnection connection, SqlTransaction transaction)
        {
        }

        protected void LoadDataRow(DataRow dataRow)
        {
            Type type = this.GetType();
            PropertyInfo[] properties = type.GetProperties();
            foreach (var property in properties)
            {
                if (dataRow.Table.Columns.Contains(property.Name))
                {
                    property.SetValue(this, dataRow.Field<string>(property.Name));
                }
            }
        }
    }
}
