using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppEmpleo.Models;

public partial class Oferta
{
    [Key]
    public int OfertaId { get; set; }

    [ForeignKey("Reclutador")]
    public int ReclutadorId { get; set; }

    public virtual Reclutador Reclutador { get; set; } = null!;

    [ForeignKey("Categoria")]
    public int CategoriaId { get; set; }

    public virtual Categoria Categoria { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string NombreOferta { get; set; } = null!;

    [Required]
    public string Descripcion { get; set; } = null!;

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaCierre { get; set; }

    [Range(0, 1000000)]
    [Column(TypeName = "decimal(10, 2)")]
    public decimal Salario { get; set; }

    public string ModalidadTrabajo { get; set; } = null!;

    public string UbicacionTrabajo { get; set; } = null!;

    public virtual ICollection<OfertaHabilidad> OfertaHabilidades { get; set; } = new List<OfertaHabilidad>();

    public virtual ICollection<Postulacion> Postulaciones { get; set; } = new List<Postulacion>();
}
