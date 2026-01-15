using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppEmpleo.Models;

public partial class JobApplication
{
    [Key]
    public int JobApplicationId { get; set; }

    [ForeignKey(nameof(JobOffer))]
    public int JobOfferId { get; set; }

    [ForeignKey(nameof(Candidate))]
    public int CandidateId { get; set; }

    [ForeignKey(nameof(Resume))]
    public int ResumeId { get; set; }

    public DateOnly AppliedAt { get; set; } = DateOnly.FromDateTime(DateTime.Now);

    public virtual Candidate Candidate { get; set; } = null!;
    public virtual Resume Resume { get; set; } = null!;
    public virtual JobOffer JobOffer { get; set; } = null!;
}
