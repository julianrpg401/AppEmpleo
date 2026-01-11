using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppEmpleo.Models;

[Table("User")]
public partial class UserAccount
{
    [Key]
    public int UserId { get; set; }

    [Required(ErrorMessage = "El campo no puede estar vacío")]
    [MaxLength(50)]
    [DisplayName("Nombre")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "El campo no puede estar vacío")]
    [MaxLength(50)]
    [DisplayName("Apellido")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "El campo no puede estar vacío")]
    [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido")]
    [DisplayName("Correo electrónico")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "El campo no puede estar vacío")]
    [DisplayName("Fecha de nacimiento")]
    public DateOnly BirthDate { get; set; }

    [Required(ErrorMessage = "El campo no puede estar vacío")]
    [DisplayName("Contraseña")]
    public string PasswordHash { get; set; } = null!;

    [Required(ErrorMessage = "El campo no puede estar vacío")]
    [MaxLength(20)]
    [DisplayName("Rol")]
    public string Role { get; set; } = null!;

    public DateOnly RegisterDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

    public virtual Recruiter? Recruiter { get; set; }
    public virtual Candidate? Candidate { get; set; }
}
