using System.Collections.Generic;

namespace CompleteApi.Business.Models
{
    public class Categoria : Entity
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public IEnumerable<Produto> Produtos { get; set; }
    }
}
