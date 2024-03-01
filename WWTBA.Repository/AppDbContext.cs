using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WWTBA.Core.Models;

namespace WWTBA.Repository
{
    public class AppDbContext : DbContext
    {
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        
        public DbSet<User> Users { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }
        public DbSet<UserTest> UserTests { get; set; }
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public override int SaveChanges()
        {
            UpdateChangeTracker();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            UpdateChangeTracker();
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        private void UpdateChangeTracker()
        {
            foreach (EntityEntry item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityReference)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            entityReference.CreatedDate = DateTime.Now;
                            Entry(entityReference).Property(x => x.UpdatedDate).IsModified = false;
                            break;
                        case EntityState.Modified:
                            entityReference.UpdatedDate = DateTime.Now;
                            Entry(entityReference).Property(x => x.CreatedDate).IsModified = false;
                            break;
                    }
                }
            }
        }
    }
}

