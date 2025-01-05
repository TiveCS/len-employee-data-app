using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
	public class EmployeesDbContext : DbContext
	{
		public EmployeesDbContext(DbContextOptions<EmployeesDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Employee>().HasIndex(e => e.NIK).IsUnique();
			modelBuilder.Entity<Employee>().HasMany(e => e.WorkHistories).WithOne(e => e.Employee).HasForeignKey(e => e.EmployeeId);
			modelBuilder.Entity<Employee>().HasOne(e => e.EmploymentLevel).WithMany(e => e.Employees).HasForeignKey(e => e.EmploymentLevelId);
			modelBuilder.Entity<Employee>().HasOne(e => e.Unit).WithMany(e => e.Employees).HasForeignKey(e => e.UnitId);

			modelBuilder.Entity<EmploymentLevel>().HasData(
				new EmploymentLevel { Id = 1, Name = "Junior Backend Developer" },
				new EmploymentLevel { Id = 2, Name = "Junior Frontend Developer" },
				new EmploymentLevel { Id = 3, Name = "Chief Executive Officer" },
				new EmploymentLevel { Id = 4, Name = "Project Manager" },
				new EmploymentLevel { Id = 5, Name = "Chief Technical Officer" },
				new EmploymentLevel { Id = 6, Name = "Software Architect" },
				new EmploymentLevel { Id = 7, Name = "Senior Software Developer" },
				new EmploymentLevel { Id = 8, Name = "Lead Marketing" }
			);

			modelBuilder.Entity<WorkUnit>().HasData(
				new WorkUnit { Id = 1, Name = "IT" },
				new WorkUnit { Id = 2, Name = "Sales" }
			);
		}

		public DbSet<Employee> Employees { get; set; }
		public DbSet<EmployeeWorkHistory> WorkHistories { get; set; }

		public DbSet<EmploymentLevel> EmploymentLevels { get; set; }

		public DbSet<WorkUnit> WorkUnits { get; set; }
	}
}
