using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Repositories;
using Domain.Interfaces;
using Persistence.Data;

namespace App.UnitOfWork;
public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ProyectoJardineriaContext _context;

    public UnitOfWork(ProyectoJardineriaContext context)
    {
        _context = context;
    }

    private ICliente _clientes;
    private IDetallePedido _detallespedidos;
    private IEmpleado _empleados;
    private IGamaProducto _gamasproductos;
    private IOficina _oficinas;
    private IPago _pagos;
    private IPedido _pedidos;
    private IProducto _productos;

    public ICliente Clientes
    {
        get
        {
            if (_clientes == null)
            {
                _clientes = new ClienteRepository(_context);
            }
            return _clientes;
        }
    }
    public IDetallePedido DetallesPedidos
    {
        get
        {
            if (_detallespedidos == null)
            {
                _detallespedidos = new DetallePedidoRepository(_context);
            }
            return _detallespedidos;
        }
    }
    public IEmpleado Empleados
    {
        get
        {
            if (_empleados == null)
            {
                _empleados = new EmpleadoRepository(_context);
            }
            return _empleados;
        }
    }
    public IGamaProducto GamasProductos
    {
        get
        {
            if (_gamasproductos == null)
            {
                _gamasproductos = new GamaProductoRepository(_context);
            }
            return _gamasproductos;
        }
    }
    public IOficina Oficinas
    {
        get
        {
            if (_oficinas == null)
            {
                _oficinas = new OficinaRepository(_context);
            }
            return _oficinas;
        }
    }
    public IPago Pagos
    {
        get
        {
            if (_pagos == null)
            {
                _pagos = new PagoRepository(_context);
            }
            return _pagos;
        }
    }
    public IPedido Pedidos
    {
        get
        {
            if (_pedidos == null)
            {
                _pedidos = new PedidoRepository(_context);
            }
            return _pedidos;
        }
    }
    public IProducto Productos
    {
        get
        {
            if (_productos == null)
            {
                _productos = new ProductoRepository(_context);
            }
            return _productos;
        }
    }
    public Task<int> SaveAsync()
    {
        return _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
