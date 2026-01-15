using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppEmpleo.Models;

public partial class JobOfferSkill
{
    [Key]
    public int JobOfferSkillId { get; set; }

    [ForeignKey(nameof(JobOffer))]
    public int JobOfferId { get; set; }

    [ForeignKey(nameof(Skill))]
    public int SkillId { get; set; }

    public virtual Skill Skill { get; set; } = null!;
    public virtual JobOffer JobOffer { get; set; } = null!;
}
