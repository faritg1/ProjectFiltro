using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace App.Repositories
{
    public class OficinaRepository : GenericRepositoryString<Oficina>, IOficina
    {
        public OficinaRepository(ProyectoJardineriaContext context) : base(context)
        {
        }
    }
}