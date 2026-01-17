using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppEmpleo.Models;

public partial class JobOffer
{
    [Key]
    public int JobOfferId { get; set; }

    [ForeignKey(nameof(Recruiter))]
    public int? RecruiterId { get; set; }

    public virtual Recruiter? Recruiter { get; set; }

    [ForeignKey(nameof(Category))]
    public int? CategoryId { get; set; }

    public virtual Category? Category { get; set; }

    [Required]
    [MaxLength(100)]
    [DisplayName("Nombre de la vacante")]
    public string JobTitle { get; set; } = null!;

    [Required]
    [DisplayName("Descripción")]
    public string Description { get; set; } = null!;

    [DisplayName("Inicio")]
    public DateOnly StartDate { get; set; }

    [DisplayName("Cierre")]
    public DateOnly EndDate { get; set; }

    [Required]
    [DisplayName("Moneda")]
    [EnumDataType(typeof(CurrencyCode), ErrorMessage = "La moneda no es válida.")]
    public required string Currency { get; set; }

    [Range(0, double.MaxValue)]
    [Column("Salary", TypeName = "decimal(10, 2)")]
    public decimal? Salary { get; set; }

    [DisplayName("Modalidad")]
    public string WorkMode { get; set; } = null!;

    [DisplayName("País")]
    [EnumDataType(typeof(CountryCode), ErrorMessage = "El país no es válido.")]
    public required string Country { get; set; } = null!;

    public virtual ICollection<JobOfferSkill> JobOfferSkills { get; set; } = new List<JobOfferSkill>();
    public virtual ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();
}

public enum CurrencyCode
{
    USD, MXN, COP, ARS, BRL, PEN, CLP, UYU, VES, PYG, BOB, CRC, DOP, GTQ, HNL, PAB
}

public enum CountryCode
{
    MEX, COL, ARG, BRA, PER, CHL, URY, VEN, PRY, BOL, CRI, DOM, GTM, HND, PAN, NIC, CUB, ECU, SLV
}
