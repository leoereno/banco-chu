using BancoChu.Models;
using Microsoft.EntityFrameworkCore;

namespace BancoChu.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Conta> Contas => Set<Conta>();


    }
}
