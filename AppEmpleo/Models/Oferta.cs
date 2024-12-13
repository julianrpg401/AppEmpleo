using System.ComponentModel;
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
    [DisplayName("Nombre de la oferta")]
    public string NombreOferta { get; set; } = null!;

    [Required]
    [DisplayName("Descripción")]
    public string Descripcion { get; set; } = null!;

    [DisplayName("Fecha de inicio")]
    public DateOnly FechaInicio { get; set; }

    [DisplayName("Fecha de cierre")]
    public DateOnly FechaCierre { get; set; }

    [Required]
    [EnumDataType(typeof(CurrencyType), ErrorMessage = "La moneda no es válida.")]
    public string? Moneda { get; set; }

    [Range(0, double.MaxValue)]
    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Salario { get; set; }

    [DisplayName("Modalidad")]
    public string ModalidadTrabajo { get; set; } = null!;

    [DisplayName("Ubicación de trabajo")]
    public string UbicacionTrabajo { get; set; } = null!;

    public virtual ICollection<OfertaHabilidad> OfertaHabilidades { get; set; } = new List<OfertaHabilidad>();

    public virtual ICollection<Postulacion> Postulaciones { get; set; } = new List<Postulacion>();
}

public enum CurrencyType
{
    USD, MXN, COP, ARS, BRL, PEN, CLP, UYU, VES, PYG, BOB, CRC, DOP, GTQ, HNL, PAB
}
