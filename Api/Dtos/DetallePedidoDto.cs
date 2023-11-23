using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos
{
    public class DetallePedidoDto
    {
        public int IdPedido { get; set; }

        public string IdProducto { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioUnidad { get; set; }

        public short NumeroLinea { get; set; }
    }
}