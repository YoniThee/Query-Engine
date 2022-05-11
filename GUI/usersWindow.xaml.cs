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
using System.Windows.Shapes;
using System.Collections.Generic;

namespace GUI
{
    /// <summary>
    /// Interaction logic for usersWindow.xaml
    /// </summary>
    public partial class usersWindow : Window
    {
        public usersWindow(List<Query_Engine.User> users)
        {
            InitializeComponent();
            usersList.ItemsSource = users;
        }
        private void usersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            popUp.Visibility = Visibility.Visible;
            text.Text = usersList.SelectedItem.ToString();
        }

        private void Ok_buttom_Click(object sender, RoutedEventArgs e)
        {
            popUp.Visibility = Visibility.Hidden;
        }
    }
}
