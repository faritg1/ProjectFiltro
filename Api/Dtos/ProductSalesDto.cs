using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos
{
    public class ProductSalesDto
    {
        public string ProductName { get; set; }
        public int UnitsSold { get; set; }
        public double TotalSales { get; set; }
        public double TotalSalesWithTax { get; set; }
    }
}