using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppEmpleo.Models;

public partial class Reclutador
{
    [Key]
    public int ReclutadorId { get; set; }

    [ForeignKey("Usuario")]
    public int UsuarioId { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;

    public virtual ICollection<Oferta> Ofertas { get; set; } = new List<Oferta>();
}
