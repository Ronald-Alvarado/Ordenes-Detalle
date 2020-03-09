using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace OrdenDetalle.Entidades
{
    public class OrdenDetalles
    {
        [Key]
        public int OrdenDetalleId { get; set; }
        public int OrdenId { get; set; }
        public int ProductoId { get; set; }
        public String Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Monto { get; set; }

        public OrdenDetalles()
        {
            OrdenDetalleId = 0;
            OrdenId = 0;
            ProductoId = 0;
            Descripcion = String.Empty;
            Cantidad = 0;
            Precio = 0;
            Monto = 0;
        }

        public OrdenDetalles(int OrdenId,int ProductoId,String Descripcion,decimal Cantidad,decimal Precio,decimal Monto)
        {
           this.OrdenId = OrdenId;
           this.ProductoId = ProductoId;
           this.Descripcion = Descripcion;
           this.Cantidad = Cantidad;
           this.Precio = Precio;
           this.Monto = Monto;
        }
    }
}
