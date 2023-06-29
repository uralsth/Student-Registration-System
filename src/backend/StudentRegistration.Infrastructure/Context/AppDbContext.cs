using Microsoft.EntityFrameworkCore;
using StudentRegistration.Domain.Entities;

namespace StudentRegistration.Infrastructure.Context
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}

		public DbSet<Student> Students { get; set; }
		public DbSet<Project> Projects { get; set; }

		public DbSet<Document> Documents { get; set; }

        public DbSet<ImagePath> Images { get; set; }

        public DbSet<User> Users { get; set; }
		public DbSet<RefreshToken> RefreshTokens { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			#region StudentBuilder
			var studentBuilder = modelBuilder.Entity<Student>();
			studentBuilder.ToTable("Students").HasKey(t => t.Id);

			studentBuilder.Property(t => t.UserName).IsRequired().HasMaxLength(100);

			studentBuilder.Property(t => t.Email).HasMaxLength(150).IsRequired();

			studentBuilder.Property(t => t.Contact).HasMaxLength(10).IsRequired().IsFixedLength();

			studentBuilder.Property(t => t.Password).HasMaxLength(15).IsRequired();

			studentBuilder.Property(t => t.Address).HasMaxLength(100).IsRequired();

			studentBuilder.Property(t => t.DateOfBirth).IsRequired();

			studentBuilder.HasMany(t => t.Projects)
				.WithOne(s => s.Student)
				.HasForeignKey(s => s.StudentId);

			studentBuilder.HasMany(t => t.Documents)
				.WithOne(s => s.Student)
				.HasForeignKey(p => p.StudentId);

			//studentBuilder.HasMany(t => t.Documents)
			//	.WithOne(s => s.Student)
			//	.HasForeignKey(f => f.StudentId);

			#endregion


			#region ProjectBuilder
			var projectBuilder = modelBuilder.Entity<Project>();
			projectBuilder.ToTable("Projects").HasKey(t => t.ProjectId);

			projectBuilder.Property(t => t.Name).IsRequired().HasMaxLength(100);

			projectBuilder.Property(t => t.Description).IsRequired().HasMaxLength(250);

			projectBuilder.Property(t => t.StudentId).IsRequired(false);


			projectBuilder.Property(t => t.CreatedDate).IsRequired();
			#endregion

			#region DocumentBuilder
			var documentBuilder = modelBuilder.Entity<Document>();
			//documentBuilder.ToTable("Documents").HasKey(t => t.DocId);

			documentBuilder.ToTable("Documents").HasKey(t => new {t.DocType, t.StudentId});

			documentBuilder.HasMany(m => m.DocPath)
				.WithOne(o => o.Document)
				.HasForeignKey(f => new { f.DocType, f.StudentId });
			#endregion


			#region ImageBuilder
			var imageBuilder = modelBuilder.Entity<ImagePath>();
			imageBuilder.ToTable("ImagePaths").HasKey(k => k.ImageId);
			#endregion

		}
	}
}
