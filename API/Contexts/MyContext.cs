using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Contexts;

public class MyContext : DbContext
{
	public MyContext(DbContextOptions<MyContext> options) : base(options)
	{

	}

	// Introduces the Model to the Database that eventually becomes an Entity
	public DbSet<Account> Accounts { get; set; }
	public DbSet<AccountRole> AccountRoles { get; set; }
	public DbSet<Education> Educations { get; set; }
	public DbSet<Employee> Employees { get; set; }
	public DbSet<Profiling> Profilings { get; set; }
	public DbSet<Role> Roles { get; set; }
	public DbSet<University> Universities { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
        // Configure Unique Constraint
        // FAQ : Kenapa gk pake anotasi? gk bisa dan ribet :))
        modelBuilder.Entity<Employee>().HasAlternateKey(e => e.Phone);
		modelBuilder.Entity<Employee>().HasAlternateKey(e => e.Email);

        // Configure PK as FK 
        // FAQ : Kenapa gk pake anotasi? Karena Data Anotation gk support kalau ada PK sebagai FK juga
        // One Employee to One Account
        modelBuilder.Entity<Employee>()
			.HasOne(a => a.Account)
			.WithOne(e => e.Employee)
			.HasForeignKey<Account>(a => a.NIK);

        // One Account to One Profiling
        modelBuilder.Entity<Account>()
            .HasOne(a => a.Profiling)
            .WithOne(e => e.Account)
            .HasForeignKey<Profiling>(a => a.NIK);
    }
}
