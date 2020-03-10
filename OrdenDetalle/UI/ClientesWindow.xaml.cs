using OrdenDetalle.BLL;
using OrdenDetalle.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OrdenDetalle.UI
{
    /// <summary>
    /// Interaction logic for ClientesWindow.xaml
    /// </summary>
    public partial class ClientesWindow : Window
    {
        Clientes cliente = new Clientes();
        public ClientesWindow()
        {
            InitializeComponent();
            this.DataContext = cliente;
            ClienteId_Text.Text = "0";
        }

        private void Limpiar()
        {
            ClienteId_Text.Text = "0";
            Nombre_Text.Text = string.Empty;
            Cedula_Text.Text = string.Empty;
            Telefono_Text.Text = string.Empty;
            cliente = new Clientes();
            Actualizar();
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private bool ExisteEnLaBasedeDato()
        {
            Clientes clientes = ClientesBLL.Buscar(cliente.ClienteId);
            return clientes != null;
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = false;

            if (Convert.ToInt32(ClienteId_Text.Text) == 0)
            {
                paso = ClientesBLL.Guardar(cliente);
            }
            else
            {
                if (!ExisteEnLaBasedeDato())
                {
                    MessageBox.Show("No se puede modificar una persona que no existe", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    paso = ClientesBLL.Modificar(cliente);
                }
            }
            if (paso)
            {
                Limpiar();

            }

        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Clientes clienteAnterior = ClientesBLL.Buscar(Convert.ToInt32(ClienteId_Text.Text));

            if (clienteAnterior != null)
            {
                cliente = clienteAnterior;
                Actualizar();
                MessageBox.Show("Encontrado");
            }
            else
            {
                MessageBox.Show("persona no encontrada");
                Limpiar();
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientesBLL.Eliminar(Convert.ToInt32(ClienteId_Text.Text)))
            {
                MessageBox.Show("Cliente eliminado");
                Limpiar();
            }
            else
            {
                MessageBox.Show("no se puede eliminar un cliente que no existe");
            }
        }

        private void Actualizar()
        {
            this.DataContext = null;
            this.DataContext = cliente;
        }

    }
}
