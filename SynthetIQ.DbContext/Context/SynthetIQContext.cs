using SynthetIQ.DbContext.Models;
using SynthetIQ.DbContext.Models.Configurations;

#nullable disable

namespace SynthetIQ.DbContext.Context
{
    public partial class SynthetIQContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public SynthetIQContext(DbContextOptions<SynthetIQContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Assistant> Assistants { get; set; }

        public virtual DbSet<Capability> Capabilities { get; set; }

        public virtual DbSet<Models.Exception> Exceptions { get; set; }

        public virtual DbSet<Llmconfiguration> Llmconfigurations { get; set; }

        public virtual DbSet<Project> Projects { get; set; }

        public virtual DbSet<Tag> Tags { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Data Source=HP\\MFSQL;Initial Catalog=SynthetIQ;Integrated Security=True;Encrypt=False");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Models.Configurations.AssistantConfiguration());
            modelBuilder.ApplyConfiguration(new ExceptionConfiguration());
            modelBuilder.ApplyConfiguration(new LlmconfigurationConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}