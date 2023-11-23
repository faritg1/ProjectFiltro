using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace App.Repositories
{
    public class ClienteRepository : GenericRepository<Cliente>, ICliente
    {
        public ClienteRepository(ProyectoJardineriaContext context) : base(context)
        {
        }
    }
}