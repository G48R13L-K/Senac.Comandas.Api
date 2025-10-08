using Comandas.Api.Models;

namespace Comandas.Api.DTOs
{
    public class ComandaCreateRequest
    {
        public int NumeroMesa { get; set; }

        public string NomeCliente { get; set; } = default!;

        public [] CardapioItemIds { get; set; };
    }
}
