using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Comandas.Api.Models
{
    public class CategoriaCardapio
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; } = default!;
        public string? Descricao { get; set; } = default!;

        public ICollection<CardapioItem>? Items { get; set; }
    }
}
