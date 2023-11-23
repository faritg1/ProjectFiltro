using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos
{
    public class CuartaDto
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public double TotalUnitsSold { get; set; }
    }
}