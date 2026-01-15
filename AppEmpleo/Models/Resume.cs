using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppEmpleo.Models;

public partial class Resume
{
    [Key]
    public int ResumeId { get; set; }

    [ForeignKey(nameof(Candidate))]
    public int CandidateId { get; set; }

    public string FileName { get; set; } = null!;

    public string FilePath { get; set; } = null!;

    public DateOnly UploadedDate { get; set; }

    public bool? IsPreferred { get; set; }

    public virtual Candidate Candidate { get; set; } = null!;
    public virtual ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();
}
