using OrdenDetalle.UI;
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

namespace OrdenDetalle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RegistrarClienteButton_Click(object sender, RoutedEventArgs e)
        {
            ClientesWindow cw = new ClientesWindow();
            cw.ShowDialog();
        }

        private void RegistrarOrdenButton_Click(object sender, RoutedEventArgs e)
        {
            OrdenesWindow ow = new OrdenesWindow();
            ow.ShowDialog();
        }

        private void RegistrarProductoButton_Click(object sender, RoutedEventArgs e)
        {
            ProductosWindow pw = new ProductosWindow();
            pw.ShowDialog();
        }
    }
}
