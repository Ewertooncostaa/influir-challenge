
using Microsoft.EntityFrameworkCore;

namespace INFLUIR.Models
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {

        }

        public DbSet<Perimetro> Perimetro { get; set; }

        public DbSet<Lote> Lote { get; set; }

        public DbSet<Produtor> Produtor { get; set; }

    }
}
