using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppEmpleo.Models;

public partial class Recruiter
{
    [Key]
    public int RecruiterId { get; set; }

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    public virtual UserAccount User { get; set; } = null!;

    public virtual ICollection<JobOffer> JobOffers { get; set; } = new List<JobOffer>();
}
