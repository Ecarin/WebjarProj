using Microsoft.EntityFrameworkCore;
using WebjarProj.Models;

namespace WebjarProj.Data;
public class WebjarDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Feature> Features { get; set; }
    public DbSet<Addon> Addons { get; set; }
    public DbSet<Product_Feature> Product_Features { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source=172.20.10.3;User Id=sa;Password=AminAnsari123!@#;Initial Catalog=Webjar;TrustServerCertificate=True;MultiSubnetFailover=True");
    }
}
