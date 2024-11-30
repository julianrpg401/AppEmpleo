﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppEmpleo.Models;

public partial class Empresa
{
    [Key]
    public int EmpresaId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? SitioWeb { get; set; }

    public string? Telefono { get; set; }

    public virtual ICollection<Empleador> Empleadores { get; set; } = new List<Empleador>();
}
