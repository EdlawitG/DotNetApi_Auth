using authApi.Models;
using Microsoft.EntityFrameworkCore;

namespace authApi.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext (options){
        public DbSet<User> Users { get; set; }
         public DbSet<Product> Products { get; set; }
    }
}