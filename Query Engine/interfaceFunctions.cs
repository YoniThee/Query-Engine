using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Query_Engine
{
    public interface interfaceFunctions
    {
        /*
        This interface is responsible for allowing access to all functions in this project(despite the principles of SOLID,
        because its little project and I will not need more than this one)
        */
        //This function filters the DB by the repository "Users"
        public string caseUsers(string input, int index, string ageOperator);



        List<User> userSearch(string query, SqlConnection connection);
        List<Data> dataSearch(int choose,string query, SqlConnection connection);


        //This function filters the DB by the repository "Data"
        public IEnumerable<Data> caseData(SqlConnection sqlString, int index);


        List<User> ConvertFromTable(DataTable dt);
    }
}
