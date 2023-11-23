using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Empleado : BaseEntityInt
{
    public string Nombre { get; set; }

    public string Apellido1 { get; set; }

    public string Apellido2 { get; set; }

    public string Extension { get; set; }

    public string Email { get; set; }

    public string IdOficina { get; set; }

    public int? IdJefe { get; set; }

    public string Puesto { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual Empleado IdJefeNavigation { get; set; }

    public virtual Oficina IdOficinaNavigation { get; set; }

    public virtual ICollection<Empleado> InverseIdJefeNavigation { get; set; } = new List<Empleado>();
}
