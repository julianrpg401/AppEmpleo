using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppEmpleo.Models;

public partial class Curriculum
{
    [Key]
    public int CurriculumId { get; set; }

    [ForeignKey("Candidato")]
    public int CandidatoId { get; set; }

    public string NombreArchivo { get; set; } = null!;

    public string RutaArchivo { get; set; } = null!;

    public DateOnly FechaCarga { get; set; }

    public bool? EsPreferido { get; set; }

    public virtual Candidato Candidato { get; set; } = null!;

    public virtual ICollection<Postulacion> Postulaciones { get; set; } = new List<Postulacion>();
}
