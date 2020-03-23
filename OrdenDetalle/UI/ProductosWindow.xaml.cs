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
    /// Interaction logic for ProductosWindow.xaml
    /// </summary>
    public partial class ProductosWindow : Window
    {
        Productos producto = new Productos();
        public ProductosWindow()
        {
            InitializeComponent();
            this.DataContext = producto;
            ProductoId_Text.Text = "0";
        }

        private void Limpiar()
        {
            ProductoId_Text.Text = "0";
            NombreProducto_Text.Text = string.Empty;
            Precio_Text.Text = "0";
            producto = new Productos();
            Actualizar();
        }

        private bool Validar()
        {
            bool paso = true;

            if(ProductoId_Text.Text == string.Empty)
            {
                MessageBox.Show("Id vacio");
                ProductoId_Text.Focus();
                paso = false;
            }

            if(NombreProducto_Text.Text == string.Empty)
            {
                MessageBox.Show("Nombre Vacio");
                NombreProducto_Text.Focus();
                paso = false;
            }

            if(Precio_Text.Text == "0" || Precio_Text.Text == string.Empty)
            {
                MessageBox.Show("El precio no puede ser 0 o esta Vacio");
                Precio_Text.Focus();
                paso = false;
            }

            return paso;
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private bool ExisteEnLaBasedeDato()
        {
            Productos productos = ProductosBLL.Buscar(producto.ProductoId);
            return productos != null;
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = false;

            if (!Validar())
            {
                return;
            }

            if (Convert.ToInt32(ProductoId_Text.Text) == 0)
            {
                paso = ProductosBLL.Guardar(producto);
                //producto.OrdenDetalle.Add(new Ordenes(Convert.ToInt32(ClienteIdTextBox.Text)));
            }
            else
            {
                if (!ExisteEnLaBasedeDato())
                {
                    MessageBox.Show("No se puede modificar un producto que no existe", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    paso = ProductosBLL.Modificar(producto);
                }
            }

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado");
            }

        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Productos productoAnterior = ProductosBLL.Buscar(Convert.ToInt32(ProductoId_Text.Text));

            if (productoAnterior != null)
            {
                producto = productoAnterior;
                Actualizar();
            }
            else
            {
                MessageBox.Show("producto no encontrado");
                Limpiar();
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductosBLL.Eliminar(Convert.ToInt32(ProductoId_Text.Text)))
            {
                MessageBox.Show("Producto eliminado");
                Limpiar();
            }
            else
            {
                MessageBox.Show("no se puede eliminar un producto que no existe");
            }
        }

        private void Actualizar()
        {
            this.DataContext = null;
            this.DataContext = producto;
        }
    }
}
