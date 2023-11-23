using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace App.Repositories
{
    public class GamaProductoRepository : GenericRepositoryString<GamaProducto>, IGamaProducto
    {
        public GamaProductoRepository(ProyectoJardineriaContext context) : base(context)
        {
        }
    }
}