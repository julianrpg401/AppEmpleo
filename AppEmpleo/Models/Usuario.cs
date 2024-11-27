using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppEmpleo.Models;

public partial class Usuario
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El campo no puede estár vacío")]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage = "El campo no puede estár vacío")]
    public string Apellido { get; set; } = null!;

    [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "El campo no puede estár vacío")]
    [DisplayName("Contraseña")]
    public string ClaveHash { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    [Required(ErrorMessage = "El campo no puede estár vacío")]
    [DisplayName("Fecha de nacimiento")]
    public DateOnly FechaNacimiento { get; set; }

    public virtual Candidato? Candidato { get; set; }

    public virtual Empleador? Empleador { get; set; }
}
