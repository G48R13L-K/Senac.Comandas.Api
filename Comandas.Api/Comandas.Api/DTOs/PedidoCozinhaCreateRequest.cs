namespace Comandas.Api.DTOs
{
    public class PedidoCozinhaCreateRequest
    {
        public int id { get; set; }
        public int ComandaId { get; set; }
        public List<PedidoCozinhaItemCreateRequest> items { get; set; } = [];
    }

    public class PedidoCozinhaItemCreateRequest
    {
        public int id { get; set; }
        public int ComandaItemId { get; set; }
        public int PedidoCozinhaId { get; set; }
    }
}
