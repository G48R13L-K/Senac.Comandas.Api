namespace Comandas.Api.DTOs
{
    public class PedidoCozinhaResponse
    {
        public int id { get; set; }
        public int ComandaId { get; set; }
        public int NumeroMesa { get; set; }
        public List<PedidoCozinhaItemCreateResponse> items { get; set; } = [];
    }
    public class PedidoCozinhaItemCreateResponse
    {
        public int id { get; set; }
        public string Titulo { get; set; }


    }
}

