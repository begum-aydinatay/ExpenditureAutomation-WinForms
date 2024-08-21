using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesAutomation
{
    public class ConfigurationHelper
    {
        public static string GetConnectionString(string name)
        { 
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
