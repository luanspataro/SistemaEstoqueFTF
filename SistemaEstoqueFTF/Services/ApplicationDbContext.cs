using Microsoft.EntityFrameworkCore;
using SistemaEstoqueFTF.Models;

namespace SistemaEstoqueFTF.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Item> Itens { get; set; }
    }
}
