using System;

namespace CompleteApi.Domain.Models
{
    public class Produto : Entity
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
        public Guid FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public Guid CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

    }
}
