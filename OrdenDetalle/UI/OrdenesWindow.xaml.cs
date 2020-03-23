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
    /// Interaction logic for OrdenesWindow.xaml
    /// </summary>
    public partial class OrdenesWindow : Window
    {
        Ordenes orden = new Ordenes();
        
        public OrdenesWindow()
        {
            InitializeComponent();
            this.DataContext = orden;

            OrdenId_Text.Text = "0";
            ClienteId_Text.Text = "0";
            ProductoId_Text.Text = "0";
            Cantidad_Text.Text = "0";
            Precio_Text.Text = "0";
            Monto_Text.Text = "0";
        }

        private void Actualizar()
        {
            this.DataContext = null;
            this.DataContext = orden;
        }

        private void Limpiar()
        {
            OrdenId_Text.Text = "0";
            ClienteId_Text.Text = "0";
            NombreCliente_Text.Text = string.Empty;
            Fecha_Text.SelectedDate = DateTime.Now;
            ProductoId_Text.Text = "0";
            Descripcion_Text.Text = string.Empty;
            Cantidad_Text.Text = "0";
            Precio_Text.Text = "0";
            Monto_Text.Text = "0";
            MontoTotal_Text.Text = "0";

            orden.OrdenDetalle = new List<Entidades.OrdenDetalles>();
            orden = new Ordenes();
            Actualizar();
        }

        private  bool Validar()
        {
            bool paso = true;

            if (OrdenId_Text.Text == string.Empty)
            {
                MessageBox.Show("OrdenId Vacio");
                OrdenId_Text.Focus();
                paso = false;
            }
            else if (ClienteId_Text.Text == string.Empty)
            {
                MessageBox.Show("ClienteId Vacio");
                ClienteId_Text.Focus();
                paso = false;
            }
            else if (NombreCliente_Text.Text == string.Empty)
            {
                MessageBox.Show("Nombre del Cliente Vacio");
                paso = false;
            }
            else if (Fecha_Text.Text == string.Empty)
            {
                MessageBox.Show("Fecha Vacia");
                Fecha_Text.Focus();
                paso = false;
            }
            else if (MontoTotal_Text.Text == "0")
            {
                MessageBox.Show("Debe Agregar un producto");
                ProductoId_Text.Focus();
                paso = false;
            }
            else if (OrdenDetalleDataGrid.DataContext == null)
            {
                MessageBox.Show("Debe Agregar una orden");
                AgregarButton.Focus();
                paso = false;
            }

            return paso;
        }

        private void AgregarButton_Click(object sender, RoutedEventArgs e)
        {
            orden.OrdenDetalle.Add(new OrdenDetalles(Convert.ToInt32(OrdenId_Text.Text), Convert.ToInt32(ProductoId_Text.Text),
                Descripcion_Text.Text, Convert.ToDecimal(Cantidad_Text.Text), Convert.ToDecimal(Precio_Text.Text),
                Convert.ToDecimal(Monto_Text.Text)));

            orden.MontoTotal += Convert.ToDecimal(Monto_Text.Text);
            MontoTotal_Text.Text = Convert.ToString(orden.MontoTotal);

            Actualizar();
            ProductoId_Text.Clear();
            Descripcion_Text.Clear();
            Precio_Text.Clear();
            Cantidad_Text.Clear();
            Monto_Text.Clear();
            ProductoId_Text.Focus();
        }

        private bool ExisteEnLaBaseDeDatos()
        {
            Ordenes OrdenAnterior = OrdenesBLL.Buscar(orden.OrdenId);

            return OrdenAnterior != null;
        }

        private bool ExisteEnLaBaseDeDatosClientes()
        {
            Clientes ClienteAnterior = ClientesBLL.Buscar(orden.ClienteId);

            return ClienteAnterior != null;
        }

        private bool ExisteEnLaBaseDeDatosProductos()
        {
            Productos ProductoAnterior = ProductosBLL.Buscar(Convert.ToInt32(ClienteId_Text.Text));

            return ProductoAnterior != null;
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = false;

            if (!Validar())
            {
                return;
            }

            if (OrdenId_Text.Text == "0")
                paso = OrdenesBLL.Guardar(orden);
            else
            {
                if (ExisteEnLaBaseDeDatos() && ExisteEnLaBaseDeDatosClientes() && ExisteEnLaBaseDeDatosProductos())
                {
                    paso = OrdenesBLL.Modificar(orden);
                }
                else
                {
                    MessageBox.Show("No se Puede Modificar una Orden que no existe, no exista un producto o cliente");
                    return;
                }
            }

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado");
            }
            else
            {
                MessageBox.Show("La Orden No se Pudo Guardar");
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrdenesBLL.Eliminar(orden.OrdenId))
            {
                MessageBox.Show("Eliminado");
                Limpiar();
            }
            else
                MessageBox.Show("No se pudo eliminar una orden que no existe");
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Ordenes OrdenAnterior = OrdenesBLL.Buscar(Convert.ToInt32(OrdenId_Text.Text));

            if (OrdenAnterior != null)
            {
                orden = OrdenAnterior;
                Actualizar();
            }
            else
            {
                Limpiar();
                MessageBox.Show("Orden,cliente o producto no encontrado");
            }
        }

        private void LlenaCampoCliente(Clientes cliente)
        {
            NombreCliente_Text.Text = cliente.Nombre;
            MontoTotal_Text.Text = Convert.ToString(orden.MontoTotal);
        }

        private void ClienteId_Text_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ClienteId_Text.Text))
            {
                int id;
                int.TryParse(ClienteId_Text.Text, out id);

                Clientes cliente = new Clientes();
                cliente = ClientesBLL.Buscar(id);

                if (cliente != null)
                {
                    LlenaCampoCliente(cliente);
                }
                else
                {
                    NombreCliente_Text.Text = "No existe el Cliente";
                }
            }
        }

        private void LlenaCampoProducto(Productos producto)
        {
            Descripcion_Text.Text = producto.NombreProducto;
            Precio_Text.Text = Convert.ToString(producto.Precio);
        }

        private void ProductoId_Text_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ProductoId_Text.Text))
            {
                int id;
                int.TryParse(ProductoId_Text.Text, out id);

                Productos producto = new Productos();
                producto = ProductosBLL.Buscar(id);

                if (producto != null)
                {
                    LlenaCampoProducto(producto);
                }
                else
                {
                    Descripcion_Text.Text = "0";
                    Precio_Text.Text = "0";
                }
            }
        }

        private void Cantidad_Text_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Cantidad_Text.Text))
            {
                decimal Monto, Precio = Convert.ToDecimal(Precio_Text.Text);
                decimal Cantidad = Convert.ToDecimal(Cantidad_Text.Text);

                Monto = Precio * Cantidad;
                Monto_Text.Text = Convert.ToString(Monto);

            }
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void ClienteId_Text_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            int Id;
            int.TryParse(ClienteId_Text.Text, out Id);

            Clientes c = new Clientes();
            c = ClientesBLL.Buscar(Id);

            if(c != null)
            {
                NombreCliente_Text.Text = c.Nombre;
            }
        }

        private void ProductoId_Text_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            int Id;
            int.TryParse(ProductoId_Text.Text, out Id);

            Productos p = new Productos();
            p = ProductosBLL.Buscar(Id);

            if(p != null)
            {
                Descripcion_Text.Text = p.NombreProducto;
                Precio_Text.Text = Convert.ToString(p.Precio);
            }
        }

        private void Precio_Text_TextChanged(object sender, TextChangedEventArgs e)
        {
            decimal num1;
            decimal.TryParse(Precio_Text.Text, out num1);
            decimal num2;

            string cantidad = Cantidad_Text.Text;
            if(cantidad != string.Empty)
            {
                num2 = Convert.ToDecimal(cantidad);
                Monto_Text.Text = Convert.ToString(num1 * num2);
            }

        }

        private void Cantidad_Text_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            decimal num2;
            decimal.TryParse(Cantidad_Text.Text, out num2);
            decimal num1;

            string precio = Precio_Text.Text;
            if(precio != string.Empty)
            {
                num1 = Convert.ToDecimal(precio);
                Monto_Text.Text = Convert.ToString(num1 * num2);
            }

        }
    }
}
