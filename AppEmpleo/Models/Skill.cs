using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppEmpleo.Models;

public partial class Skill
{
    [Key]
    public int SkillId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<JobOfferSkill> JobOfferSkills { get; set; } = new List<JobOfferSkill>();
}
