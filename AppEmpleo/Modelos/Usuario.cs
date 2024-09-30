using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppEmpleo.Modelos;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    [Required(ErrorMessage = "Campo obligatorio")]
    public string NombreUsuario { get; set; } = null!;

    [Required(ErrorMessage = "Campo obligatorio")]
    public string ApellidoUsuario { get; set; } = null!;

    [Required(ErrorMessage = "Campo obligatorio")]
    [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido")]
    public string EmailUsuario { get; set; } = null!;

    [Required(ErrorMessage = "Campo obligatorio")]
    [DataType(DataType.Password)]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
    public string ClaveUsuario { get; set; } = null!;

    [Required(ErrorMessage = "Campo obligatorio")]
    public string Telefono { get; set; } = null!;

    [Required(ErrorMessage = "Campo obligatorio")]
    public DateOnly FechaNacimiento { get; set; }

    public virtual Empleador IdUsuario1 { get; set; } = null!;

    public virtual Candidato IdUsuarioNavigation { get; set; } = null!;
}
