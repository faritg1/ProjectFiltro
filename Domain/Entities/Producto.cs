﻿using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Producto
{
    public string Id { get; set; }

    public string Nombre { get; set; }

    public string IdGama { get; set; }

    public string Dimensiones { get; set; }

    public string Proveedor { get; set; }

    public string Descripcion { get; set; }

    public short CantidadEnStock { get; set; }

    public decimal PrecioVenta { get; set; }

    public decimal? PrecioProveedor { get; set; }

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    public virtual GamaProducto IdGamaNavigation { get; set; }
}
