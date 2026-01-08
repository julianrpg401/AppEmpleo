using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppEmpleo.Models;

public partial class Usuario
{
    [Key]
    public int UsuarioId { get; set; }

    [Required(ErrorMessage = "El campo no puede estar vacío")]
    [MaxLength(50)]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage = "El campo no puede estar vacío")]
    [MaxLength(50)]
    public string Apellido { get; set; } = null!;

    [Required(ErrorMessage = "El campo no puede estar vacío")]
    [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "El campo no puede estar vacío")]
    [DisplayName("Fecha de nacimiento")]
    public DateOnly FechaNacimiento { get; set; }

    [Required(ErrorMessage = "El campo no puede estar vacío")]
    [DisplayName("Contraseña")]
    public string ClaveHash { get; set; } = null!;

    [Required(ErrorMessage = "El campo no puede estar vacío")]
    [MaxLength(20)]
    public string Rol { get; set; } = null!;

    public DateOnly FechaRegistro { get; set; }

    [ForeignKey("UsuarioId")]
    public virtual Reclutador? Reclutador { get; set; }

    [ForeignKey("UsuarioId")]
    public virtual Candidato? Candidato { get; set; }
}
