using Comandas.Api.Models;

namespace Comandas.Api.DTOs
{
    public class ComandaUpdateRequest
    {
        public int NumeroMesa { get; set; }

        public string NomeCliente { get; set; } = default!;

        public ComandaItemUpdateRequest[] Itens { get; set; } = [];//lista
    }
    public class ComandaItemUpdateRequest
    {
        public int Id { get; set; } //id da comanda item
        public bool Remove { get; set; }//indica se esta removendo
        public int cardapíoItemId { get; set; }//indica se esta inserindo
    }

}


















































