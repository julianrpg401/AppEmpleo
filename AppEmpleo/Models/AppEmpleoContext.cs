using Microsoft.EntityFrameworkCore;

namespace AppEmpleo.Models;

public partial class AppEmpleoContext : DbContext
{
    public AppEmpleoContext()
    {
    }

    public AppEmpleoContext(DbContextOptions<AppEmpleoContext> options)
        : base(options)
    {
    }

    public DbSet<UserAccount> Users { get; set; }
    public DbSet<Recruiter> Recruiters { get; set; }
    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<Resume> Resumes { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<JobOffer> JobOffers { get; set; }
    public DbSet<JobOfferSkill> JobOfferSkills { get; set; }
    public DbSet<JobApplication> JobApplications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Recruiter>()
            .HasOne(r => r.User)
            .WithOne(u => u.Recruiter)
            .HasForeignKey<Recruiter>(r => r.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Candidate>()
            .HasOne(c => c.User)
            .WithOne(u => u.Candidate)
            .HasForeignKey<Candidate>(c => c.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        // Configurar las relaciones de las claves foráneas
        modelBuilder.Entity<JobApplication>()
            .HasOne(p => p.Candidate)
            .WithMany(c => c.JobApplications)
            .HasForeignKey(p => p.CandidateId)
            .OnDelete(DeleteBehavior.NoAction); // No eliminar en cascada

        modelBuilder.Entity<JobApplication>()
            .HasOne(p => p.Resume)
            .WithMany(c => c.JobApplications)
            .HasForeignKey(p => p.ResumeId)
            .OnDelete(DeleteBehavior.NoAction); // No eliminar en cascada

        modelBuilder.Entity<JobApplication>()
            .HasOne(p => p.JobOffer)
            .WithMany(o => o.JobApplications)
            .HasForeignKey(p => p.JobOfferId)
            .OnDelete(DeleteBehavior.NoAction); // No eliminar en cascada

        // Configuración adicional de otras entidades si es necesario...
    }
}
