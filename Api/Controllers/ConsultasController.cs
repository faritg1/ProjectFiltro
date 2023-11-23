using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Api.Controllers
{
    public class ConsultasController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ProyectoJardineriaContext _context;

        public ConsultasController(IUnitOfWork unitOfWork, IMapper mapper, ProyectoJardineriaContext context)
        {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
            _context = context;
        }


        // 1.
        [HttpGet("Primera")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IQueryable<PrimeraDto>> Getprimera()
        {
            var client = from cli in _context.Clientes 
                        join pe in _context.Pedidos on cli.Id equals pe.IdCliente 
                        join de in _context.DetallePedidos on cli.Id equals de.IdPedido
            where (pe.FechaEntrega > pe.FechaEsperada || pe.FechaEntrega.Day == 0)
            select new  PrimeraDto{    
                IdCliente = cli.Id,
                FechaEntrega = pe.FechaEntrega,
                FechaEsperada = pe.FechaEsperada

            };
            return  Task.FromResult(client);
        }

        // 2. 

        // 3. 
        [HttpGet("Tercera")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<IQueryable<TercerDto>> GetOfficeNotContent(){
            var office = from emp in _context.Empleados
                        join off in _context.Oficinas on emp.IdOficina equals off.Id
                        join cli in _context.Clientes on emp.Id equals cli.IdEmpleado
            where cli.IdEmpleado != emp.Id
            select new TercerDto{
                NombreOficina = off.LineaDireccion1,
                NombreEmpleado = emp.Nombre
            };
            return Task.FromResult(office);
        }


        // 4. 
        [HttpGet("Cuarta")] // revisar
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IEnumerable<CuartaDto>> GetTopSoldProducts()
        {
            var groupedResults = await (
            from Detalle in _context.DetallePedidos
            join product in _context.Productos on Detalle.IdProducto equals product.Id
            group new { Detalle, product } by new { product.Id, product.Nombre } into groupedDetalles
            orderby groupedDetalles.Sum(od => od.Detalle.Cantidad) descending
            select new
            {
                ProductId = groupedDetalles.Key.Id,
                ProductName = groupedDetalles.Key.Nombre,
                TotalUnitsSold = groupedDetalles.Sum(od => od.Detalle.Cantidad)
            })
            .Take(20)
            .ToListAsync();

            var topSoldProducts = groupedResults
            .Select(result => new CuartaDto
            {
                ProductId = result.ProductId,
                ProductName = result.ProductName,
                TotalUnitsSold = result.TotalUnitsSold
            })
            .ToList();

            return topSoldProducts;
        }

        // 5. 
        [HttpGet("Quinta")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IEnumerable<ProductSalesDto>> GetProductsSalesOver3000()
        {
            var productsSales = await (from DetallePedidoDto in _context.DetallePedidos
            join product in _context.Productos on DetallePedidoDto.IdProducto equals product.Id
            group new { DetallePedidoDto, product } by new { product.Id, product.Nombre } into grouped
            where grouped.Sum(g => g.DetallePedidoDto.Cantidad * g.product.CantidadEnStock) > 3000
            select new ProductSalesDto
            {
                ProductName = grouped.Key.Nombre,
                UnitsSold = grouped.Sum(g => g.DetallePedidoDto.Cantidad),
                TotalSales = grouped.Sum(g => g.DetallePedidoDto.Cantidad * g.product.CantidadEnStock),
                TotalSalesWithTax = grouped.Sum(g => g.DetallePedidoDto.Cantidad * g.product.CantidadEnStock) * 0.21
            })
            .ToListAsync();

            return productsSales;
        }

        // 6. 

        // 7. 

        // 8. 

        // 9. 
        [HttpGet("Novena")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<IQueryable<NovenaDto>> GetEmployeeNotClient(){
            var emplo = from emp in _context.Empleados
                        join cli in _context.Clientes on emp.Id equals cli.IdEmpleado
                        into emplClient from empCli in emplClient.DefaultIfEmpty()
            where emp.Id != empCli.IdEmpleado
            select new NovenaDto{
                Id = emp.Id,
                Nombre = emp.Nombre,
                Apellido1 = emp.Apellido1,
                Apellido2 = emp.Apellido2
            };
            return Task.FromResult(emplo);
        }

        // 10. 
        [HttpGet("Decima")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<IQueryable<DecimaDto>> GetProductOrderDetail(){
            var product = from pro in _context.Productos // CUANDO SE VALIA QUE NO HAYA APARECIDO NINGUN PRODUCTO DEBE SALIR EL PRODUCTO Y SE TENDRA QUE COMENZAR POR LA TABLA DEL PRODUCTO.
                        join orderDetail in _context.DetallePedidos on pro.Id equals orderDetail.IdProducto
                        into orderDetaProd from ordPro in orderDetaProd.DefaultIfEmpty()
                        join ranPro in _context.GamaProductos on pro.IdGama equals ranPro.Id
                        into rangProdu from rangerPro in rangProdu.DefaultIfEmpty()
            where pro.Id != ordPro.IdProducto
            select new DecimaDto{
                Nombre = pro.Nombre,
                Des = rangerPro.DescripcionTexto,
                Img = rangerPro.Imagen
            };
            return Task.FromResult(product);
        }


    }
}