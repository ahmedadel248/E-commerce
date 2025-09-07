using E_commerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using E_commerce.ViewModels;
namespace E_commerce.Data;

public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opitions) : base(opitions)
    {
    }

    public DbSet<Catgory> Catgories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Brand> Brands { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
 
    //Deprecated
    public ApplicationDbContext()
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    optionsBuilder.UseSqlServer("Data Source=(localdb)\\" +
        "MSSQLLocalDB;Initial Catalog=E-commerce2172002;Integrated Security=True;Connect Timeout=30;");
    }

public DbSet<E_commerce.ViewModels.RegisterVM> RegisterVM { get; set; } = default!;

}


