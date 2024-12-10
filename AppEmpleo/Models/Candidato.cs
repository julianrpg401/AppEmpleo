using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppEmpleo.Models;

public partial class Candidato
{
    [Key]
    public int CandidatoId { get; set; }

    [ForeignKey("Usuario")]
    public int UsuarioId { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;

    public string? Direccion { get; set; }

    [Phone(ErrorMessage = "El teléfono no tiene un formato válido")]
    public string? Telefono { get; set; }

    public virtual ICollection<Curriculum> Curriculums { get; set; } = new List<Curriculum>();

    public virtual ICollection<Postulacion> Postulaciones { get; set; } = new List<Postulacion>();
}
