using Microsoft.EntityFrameworkCore;

namespace Comandas.Api
{
    public class ComandaDbContext : DbContext
    {
        public ComandaDbContext(
        DbContextOptions <ComandaDbContext> options) : base(options)
        {
        }
        public DbSet<Models.Usuario> Usuarios { get; set; } = default!;
        public DbSet<Models.Mesa> Mesas { get; set; } = default!;
        public DbSet<Models.Reserva> Reservas { get; set; } = default!;
        public DbSet<Models.Comanda> Comandas { get; set; } = default!;
        public DbSet<Models.ComandaItem> ComandasItem { get; set; } = default!;
        public DbSet<Models.PedidoCozinha> pedidoCozinhas { get; set; } = default!;
        public DbSet<Models.CardapioItem> cardapioItems { get; set; } = default!;
       
    }
}
