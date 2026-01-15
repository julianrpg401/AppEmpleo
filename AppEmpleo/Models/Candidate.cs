using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppEmpleo.Models;

public partial class Candidate
{
    [Key]
    public int CandidateId { get; set; }

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    public virtual UserAccount User { get; set; } = null!;

    public string? Address { get; set; }

    [Phone(ErrorMessage = "El teléfono no tiene un formato válido")]
    public string? Phone { get; set; }

    public virtual ICollection<Resume> Resumes { get; set; } = new List<Resume>();
    public virtual ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();
}
