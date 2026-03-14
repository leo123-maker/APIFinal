using APIFinal.Models;
using Microsoft.EntityFrameworkCore;
public class AplicationDBContext:DbContext
{
    public AplicationDBContext(DbContextOptions<AplicationDBContext> options): base(options)
    {
        

    }

   public DbSet<Category> Categories {get; set; }

   public DbSet<Product> Products {get; set; }

   public DbSet<User> Users {get; set; }
}