using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace Query_Engine
{
    class Program
    {
        static void Main(string[] args)
        {

            //for get information from SQL server we have to know the connection string for get connection to the server
            Console.WriteLine("Please enter the sql source(connection string)");
            string sqlConnect = Console.ReadLine();
            //string sqlConnect = "Data Source=DESKTOP-6PQJSFF;Initial Catalog=test_db;Integrated Security=True";

            SqlConnection connection = new SqlConnection(sqlConnect);
            connection.Open();
            Console.WriteLine("What do you want to search?");
            Console.WriteLine("Exit - 0\nUsers - 1\nData - 2\n");
            int choose = Convert.ToInt32(Console.ReadLine());
            interfaceFunctions initilaizeObject = Factory.GetObject();
            string queryCMD = "";
            string input = "";
            while (choose <= 2 && choose >= 0)
            {
                switch (choose)
                {
                    case 0:
                        Console.WriteLine("End\n");
                        break;
                    case 1: //Users case - the format is Email, full name and age
                        {
                            List<User> ans = new List<User>();
                            Console.WriteLine("What would you like to look for?");
                            Console.WriteLine("Email address - 1\nFull name - 2\nAge-3");
                            int userChoose = Convert.ToInt32(Console.ReadLine());
                            string operatorAge = "Age ";
                            string temp = "";

                            SqlCommand cmd = new SqlCommand();
                            if (userChoose == 1) {//Email search
                                Console.WriteLine("Enter Email address");
                                input = Console.ReadLine();
                                temp = "Email = " + $"'{input}'";

                            }
                            if (userChoose == 2) { // full name search
                                Console.WriteLine("Enter full name");
                                input = Console.ReadLine();
                                temp = "FullName = " + $"'{input}'";
                            }
                            if (userChoose == 3) {//age search
                                Console.WriteLine("Enter age");
                                input = Console.ReadLine();
                                Console.WriteLine("> or < or = or >= or <= this age?");
                                operatorAge = Console.ReadLine();
                                temp = "Age " + operatorAge + $"'{input}'";
                            }
                            queryCMD = initilaizeObject.caseUsers(temp, userChoose-1, operatorAge);
                            int addFilter = 1;
                            while (addFilter == 1)
                            {//the user can add more filters to the query
                                Console.WriteLine("Would you like to add more filter?\nYes - 1\nNo -2");
                                addFilter = Convert.ToInt32(Console.ReadLine());
                                if (addFilter == 1)
                                {
                                    Console.WriteLine("Witch filter do you want to add?\nEmail - 1\nFullName - 2\nAge - 3");
                                    int filterChoose = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Your filter is addition - 1 (AND) or multi choose(OR)- 2?");
                                    int kindFilter = Convert.ToInt32(Console.ReadLine());
                                    string insideInput = "";
                                    switch (filterChoose)
                                    {
                                        case 1:
                                            Console.WriteLine("Enter Email address");
                                            insideInput = Console.ReadLine();
                                            if (kindFilter == 1)
                                                queryCMD += " and Email = " + $"'{insideInput}'";
                                            if (kindFilter == 2)
                                                queryCMD += " or Email = " + $"'{insideInput}'";
                                            break;
                                        case 2:
                                            Console.WriteLine("Enter full name");
                                            insideInput = Console.ReadLine();
                                            if (kindFilter == 1)
                                                queryCMD += " and FullName = " + $"'{insideInput}'";
                                            if (kindFilter == 2)
                                                queryCMD += " or FullName = " + $"'{insideInput}'";
                                            break;
                                        case 3:
                                            Console.WriteLine("Enter Age");
                                            insideInput = Console.ReadLine();
                                            Console.WriteLine("> or < or = or >= or <= this age?");
                                            operatorAge = Console.ReadLine();
                                            if (kindFilter == 1)
                                                queryCMD += " and Age " + operatorAge + $"'{insideInput}'";
                                            if (kindFilter == 2)
                                                queryCMD += " or Age " + operatorAge + $"'{insideInput}'";
                                            break;
                                    }
                                }
                                if (addFilter == 2)
                                    break;
                            }
                            ans = initilaizeObject.userSearch(queryCMD, connection);

                            //print the result to the consule
                            foreach (var result in ans)
                            {
                                Console.WriteLine(result.ToString());
                            }
                            if (ans.Count == 0)
                                Console.WriteLine("Sorry, your search have no answer");
                            break;
                        }
                    case 2://Data case - the formt is list of Users and list of orders
                        {
                            List<Data> data = new List<Data>();
                            Console.WriteLine("What would you like to look for?");
                            Console.WriteLine("Users - 0\nOrder - 1");
                            int dataChoose = Convert.ToInt32(Console.ReadLine());
                            connection = new SqlConnection(sqlConnect);
                            data = initilaizeObject.caseData(connection, dataChoose).ToList();
                            //print the result to the screen
                            foreach (var result in data)
                            {
                                Console.WriteLine(result.ToString());
                            }
                            if (data.Count == 0)
                                Console.WriteLine("Sorry, your search have no answer");
                            break;
                        }

                }
                if (choose == 0)
                    break;
                Console.WriteLine("Would you like to look for more information?");
                Console.WriteLine("For exit - 0\nUsers - 1\nData - 2\n");
                choose = Convert.ToInt32(Console.ReadLine());
            }

        }
    }
}
