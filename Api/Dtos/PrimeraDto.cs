using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos
{
    public class PrimeraDto
    {
        public int IdCliente { get; set; }
        public DateOnly FechaEntrega { get; set; }
        public DateOnly FechaEsperada { get; set; }
    }
}