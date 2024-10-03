using Oracle.ManagedDataAccess.Client;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;
using System.Data;
using System.Data.Common;
namespace Budgie
{
    public class Customers
    {
        OracleConnection connection;
        public Customers() { 
            this.connection = new OracleConnection("Data Source = oracle / orcl; User ID = T00225039; Unicode = True");
            connection.Open();
            OracleCommand cmd = new OracleCommand("select *",this.connection);
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            OracleDataAdapter dataAdapter = new OracleDataAdapter(cmd);
            dataAdapter.Fill(ds);
            Console.WriteLine(ds.ToString());
        }
    }
}
