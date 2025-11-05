using Microsoft.EntityFrameworkCore;

namespace Comandas.Api
{
    public class ComandaDbContext : DbContext
    {
        public ComandaDbContext(
        DbContextOptions <ComandaDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Usuario>().HasData(
                    new Models.Usuario
                    {
                        Id = 1,
                        Nome = "Admin",
                        Email = "admin@admin.com",
                        Senha = "111111"

                    }
                    );
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Models.Mesa>().HasData(
                    new Models.Mesa
                    {
                        Id = 1,
                        NumeroMesa = 1,
                        SituacaoMesa = 0
                    },
                    new Models.Mesa
                    {
                        Id = 2,
                        NumeroMesa = 2,
                        SituacaoMesa = 0
                    }
                    );
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Models.CardapioItem>().HasData(
                    new Models.CardapioItem
                    {
                        Id = 2,
                        Titulo = "Coxinha",
                        Descricao = "Coxinha de frango com catupiry",
                        Preco = 6,
                        PossuiPreparo = false
                    },
                    new Models.CardapioItem
                    {
                        Id = 3,
                        Titulo = "Pizza Calabresa",
                        Descricao = "Pizza de calabresa com cebola",
                        Preco = 35,
                        PossuiPreparo = true
                    }
                    );

        }
        public DbSet<Models.Usuario> Usuarios { get; set; } = default!;
        public DbSet<Models.Mesa> Mesas { get; set; } = default!;
        public DbSet<Models.Reserva> Reservas { get; set; } = default!;
        public DbSet<Models.Comanda> Comandas { get; set; } = default!;
        public DbSet<Models.ComandaItem> ComandasItem { get; set; } = default!;
        public DbSet<Models.PedidoCozinha> pedidoCozinhas { get; set; } = default!;
        public DbSet<Models.PedidoCozinhaItem> pedidoCozinhaItems { get; set; } = default!;
        public DbSet<Models.CardapioItem> cardapioItems { get; set; } = default!;
       
    }
}
