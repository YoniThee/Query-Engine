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

namespace GUI
{
    /// <summary>
    /// Interaction logic for ordersWindow.xaml
    /// </summary>
    public partial class ordersWindow : Window
    {
        public ordersWindow(List<Query_Engine.Order>orders)
        {
            InitializeComponent();
            ordersList.ItemsSource = orders;
            popUp.Visibility = Visibility.Hidden;
        }

        private void ordersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            popUp.Visibility = Visibility.Visible;
            textSender.Text = ordersList.SelectedItem.ToString();
        }

        private void Ok_buttom_Click(object sender, RoutedEventArgs e)
        {
            popUp.Visibility = Visibility.Hidden;
        }
    }
}
