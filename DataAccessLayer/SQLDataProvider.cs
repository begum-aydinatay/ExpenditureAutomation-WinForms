using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class SQLDataProvider
    {
        public SqlConnection Connection { get; set; }
        public SqlCommand Command { get; set; }

        public SQLDataProvider(string connectionString)
        {
            this.Connection = new SqlConnection(connectionString);
            this.Command = this.Connection.CreateCommand();
        }

        public DataTable GetDataTable(string query)
        {
            this.Command.CommandText = query;

            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(this.Command);
            adapter.Fill(dt);

            return dt;
        }

        public object GetSingleCell(string query)
        {
            object result = null;
            this.Command.CommandText = query;

            this.Connection.Open();

            result = this.Command.ExecuteScalar(); // returns the first cell (first row, first column of query)

            this.Connection.Close();

            return result;
        }

    }
}
