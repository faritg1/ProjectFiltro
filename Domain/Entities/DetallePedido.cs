using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class DetallePedido
{
    public int IdPedido { get; set; }

    public string IdProducto { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnidad { get; set; }

    public short NumeroLinea { get; set; }

    public virtual Pedido IdPedidoNavigation { get; set; }

    public virtual Producto IdProductoNavigation { get; set; }
}
