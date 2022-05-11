using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using Query_Engine;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<User> users_list = new List<User>();
        List<Data> data_list = new List<Data>();
        //now this objectt(accessToFunctions) will be our accsess to all the "Query Engine" project
        interfaceFunctions accessToFunctions = Factory.GetObject();
        List<string> operatorsQuery = new List<string>();
        List<string> keyQuery = new List<string>();
        SqlConnection connection = new SqlConnection();
        public MainWindow()
        {
            InitializeComponent();
            popUp.Visibility = Visibility.Hidden;
        }

        private void DBcombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DBcombo.SelectionChanged += parameters_Initialize;
            //Until the user selects which DB the query option is blocked
            if (DBcombo.SelectedIndex == -1)
                query.IsEnabled = false;
            else
            {
                DBcombo.SelectionChanged += parameters_Initialize;
                popUp.Visibility = Visibility.Visible;
                query.IsEnabled = true;
            }
        }

        private void combo_initialize(object sender, EventArgs e)
        {
            //initiliaze the combo box with this 2 options
            List<string> lst = new List<string>();
            lst.Add("Users");
            lst.Add("Data");
            DBcombo.ItemsSource = lst;
        }

        private void query_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void listAns_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void paramater_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (optionsFilter.SelectedIndex != -1)
            {
                query.Text = "";
                if (optionsFilter.SelectedIndex == 2)//Age
                {//for age we have some operators so we need to open another comboBox
                    ageFilter.Visibility = Visibility.Visible;
                }
                if (DBcombo.SelectedIndex == 0 && optionsFilter.SelectedIndex != 2)//not age case
                    operatorsQuery.Add(optionsFilter.SelectedItem.ToString() + "=");
                else if (DBcombo.SelectedIndex == 1)
                    operatorsQuery.Add(optionsFilter.SelectedItem.ToString());
            }
        }
        private void parameters_Initialize(object sender, EventArgs e)
        {
            List<string> lstUser = new List<string>() { "Email", "FullName", "Age" };
            List<string> lstData = new List<string>() { "Users", "Orders" };
            if (DBcombo.SelectedIndex == 0)
            {//the user choose get from User
                optionsFilter.ItemsSource = lstUser;
                add_filter.IsEnabled = true;
            }
            if (DBcombo.SelectedIndex == 1)
            {
                optionsFilter.ItemsSource = lstData;
                add_filter.IsEnabled = false;
            }
        }
        private void age_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (optionsFilter.SelectedIndex == 2)//age
            {
                operatorsQuery.Add(ageFilter.SelectedItem.ToString() + " ");
            }
        }
        private void age_Initialize(object sender, EventArgs e)
        {
            List<string> lst = new List<string>() { "Age <", "Age <=", "Age =", "Age >", "Age >=" };
            ageFilter.ItemsSource = lst;
        }

        private void enter_button_Click(object sender, RoutedEventArgs e)
        {
            string input = createQuery(query.Text, keyQuery, operatorsQuery);

            if (DBcombo.SelectedIndex == 0) //Users case
            {
                popUp.Visibility = Visibility.Visible;
                string operatorAge = "";
                if (ageFilter.SelectedIndex != -1)

                    //this may cause exception becouse the user can choose Email or name and not choose any age filter
                    operatorAge = ageFilter.SelectedItem.ToString();

                string query = accessToFunctions.caseUsers(input, optionsFilter.SelectedIndex, operatorAge);
                users_list = accessToFunctions.userSearch(query, connection);
                if (users_list.Count() == 0)
                    MessageBox.Show("No results", "information", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    new usersWindow(users_list).Show();

                popUp.Visibility = Visibility.Hidden;
            }
            if (DBcombo.SelectedIndex == 1) //Data case
            {
                data_list = accessToFunctions.caseData(connection, optionsFilter.SelectedIndex).ToList();
                if (data_list.Count() == 0)
                    MessageBox.Show("No results", "information", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                {
                    if (optionsFilter.SelectedIndex == 0)
                        new usersWindow(data_list[0].Users).Show();
                    if (optionsFilter.SelectedIndex == 1)
                        new ordersWindow(data_list[0].Orders).Show();
                }

            

        }
    }

    private string createQuery(string text, List<string> keyQuery, List<string> operatorsQuery)
    {
        string query = "";
        int i = 0;
        for (; i < operatorsQuery.Count - 1; i++)
        {
            query += operatorsQuery[i] + " " + keyQuery[i] + " ";
        }
        if (optionsFilter.SelectedIndex != 2)
        {
            if (operatorsQuery[i] != "")
                query += operatorsQuery[i] + "'" + text + "'";
            if (operatorsQuery[i] == "")
                query += text;
        }
        if (optionsFilter.SelectedIndex == 2)
            query += operatorsQuery[i] + text;
        return query;
    }

    private void add_filter_Click(object sender, RoutedEventArgs e)
    {
        add_or_condition.Visibility = Visibility.Visible;
        add_and_condition.Visibility = Visibility.Visible;
    }

    private void add_or_Click(object sender, RoutedEventArgs e)
    {
        keyQuery.Add("'" + query.Text + "'" + " or ");
        query.Text = "";
        query.IsEnabled = false;
        MessageBox.Show("choose whitch parmeter add to filter", "information", MessageBoxButton.OK, MessageBoxImage.Information);
        optionsFilter.SelectionChanged += freeQuery;
    }

    private void add_add_Click(object sender, RoutedEventArgs e)
    {
        keyQuery.Add("'" + query.Text + "'" + " and ");
        query.Text = "";
        query.IsEnabled = false;
        MessageBox.Show("choose whitch parmeter add to filter", "information", MessageBoxButton.OK, MessageBoxImage.Information);
        optionsFilter.SelectionChanged += freeQuery;

    }

    private void freeQuery(object sender, SelectionChangedEventArgs e)
    {
        query.IsEnabled = true;
    }

    private void Enter_server_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            connection = new SqlConnection(sqlConnect.Text);
            login.Visibility = Visibility.Hidden;
            engine.Visibility = Visibility.Visible;

        }
        catch (Exception) { MessageBox.Show("This server dos'nt exist!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error); }
    }

    private void new_query_click(object sender, RoutedEventArgs e)
    {
        DBcombo.SelectedIndex = -1;
        optionsFilter.SelectedIndex = -1;
        query.Text = "";
        keyQuery = new List<string>();
        operatorsQuery = new List<string>();
        popUp.Visibility = Visibility.Hidden;
    }
}
}
