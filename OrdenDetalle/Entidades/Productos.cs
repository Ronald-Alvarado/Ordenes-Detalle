using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OrdenDetalle.Entidades
{
    public class Productos
    {
        [Key]
        public int ProductoId { get; set; }
        public String NombreProducto { get; set; }
        public decimal Precio { get; set; }

        [ForeignKey("ProductoId")]
        public virtual List<OrdenDetalles> OrdenDetalle { get; set; }

        public Productos()
        {
            ProductoId = 0;
            NombreProducto = String.Empty;
            Precio = 0;
        }

        public Productos(int productoId, string nombreProducto, decimal precio, List<OrdenDetalles> ordenDetalle)
        {
            ProductoId = productoId;
            NombreProducto = nombreProducto ?? throw new ArgumentNullException(nameof(nombreProducto));
            Precio = precio;
            OrdenDetalle = ordenDetalle ?? throw new ArgumentNullException(nameof(ordenDetalle));
        }



        /*    public Productos(int ProductoId, string NombreProducto, decimal Precio)
            {
                this.ProductoId = ProductoId;
                this.NombreProducto = NombreProducto;
                this.Precio = Precio;
            }*/
    }
}
