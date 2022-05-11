using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query_Engine
{

    public class initilaizeObject : interfaceFunctions
    {
        /*
         * This class is create an object for using at all the program.
         * The reason that I build this ctor is some of the things that need to be realized for "Singelton method"
         */
        static readonly initilaizeObject instance = new initilaizeObject();
        internal static initilaizeObject Instance { get { return instance; } }
        private initilaizeObject() { }

        public List<User> userSearch(string query, SqlConnection connection)
        {
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(query, connection);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return ConvertFromTable(dt);
        }

        public List<User> ConvertFromTable(DataTable dt)
        {
            List<User> ans = new List<User>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                User user = new User();
                user.Email = dt.Rows[i]["Email"].ToString();
                user.FullName = dt.Rows[i]["FullName"].ToString();
                user.Age = Convert.ToInt32(dt.Rows[i]["Age"]);
                ans.Add(user);
            }
            return ans;
        }
        private List<Order> ConvertOrderFromTable(DataTable dt)
        {
            List<Order> ans = new List<Order>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Order user = new Order();
                user.Sender = dt.Rows[i]["Sender"].ToString();
                user.Target = dt.Rows[i]["Target"].ToString();
                ans.Add(user);
            }
            return ans;
        }
        private List<Order> orderSearch(string query, SqlConnection connection) 
        {
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(query, connection);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return ConvertOrderFromTable(dt);
        }
        public List<Data> dataSearch(int choose, string query, SqlConnection connection)
        {
            List<Data> ans = new List<Data>();
            if(choose == 1)
            {
                List<User> users = new List<User>();
                users = userSearch(query + "Users ", connection);
                Data data = new Data();
                data.Users = users;
                ans.Add(data);
            }
            if(choose == 2)
            {
                List<Order> orders = new List<Order>();
                orders = orderSearch(query + "Orders ", connection);
                Data data = new Data();
                data.Orders = orders;
                ans.Add(data);
            }
            return ans;
        }

        public IEnumerable<Data> caseData(SqlConnection connection, int index)
        {
            //string sqlConnect = "Data Source=DESKTOP-6PQJSFF;Initial Catalog=test_db;Integrated Security=True";
            List<Data> lst = new List<Data>();
            if (index == 0)
            {
                string query = "Select* from ";
                lst = dataSearch(1, query, connection);
            }
            if (index == 1)
            {
                string query = "Select* from ";
                lst = dataSearch(2, query, connection);

            }
            return lst;
        }

        public string caseUsers(string input, int index, string ageOperator)
        {
            string query = "";
            List<User> lst = new List<User>();
            if (index == 0)
            {//Email search
                query = $"Select* from Users where " + input;
            }
            if (index == 1)
            {
                query = "Select * from Users where " + input;
            }
            if (index == 2)
            {
                query = "Select * from Users where " + input;
            }
            return query; ;
        }
    }
}
