using Comandas.Api.Models;

namespace Comandas.Api.DTOs
{
    public class ComandaUpdateRequest
    {
        public int NumeroMesa { get; set; }

        public string NomeCliente { get; set; } = default!;

        public List<ComandaItem> Itens { get; set; } = new List<ComandaItem>();
    }
}
