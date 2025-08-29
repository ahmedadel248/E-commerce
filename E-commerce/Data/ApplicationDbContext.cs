using E_commerce.Models;
using Microsoft.EntityFrameworkCore;
namespace E_commerce.Data;

public class ApplicationDbContext:DbContext
{
    public DbSet<Catgory> Catgories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Brand> Brands { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=E-commerce2172002;Integrated Security=True;Connect Timeout=30;");
    }
}


