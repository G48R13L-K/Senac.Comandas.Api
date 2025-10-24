namespace Comandas.Api.DTOs
{
    public class PedidoCozinhaUpdateRequest
    {
        public int id { get; set; }
        public int ComandaId { get; set; }
        public List<PedidoCozinhaItemCreateRequest> items { get; set; } = [];
    }

    
}
