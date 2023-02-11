using ApiCatalogo.Validacao;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiCatalogo.Models
{
    [Table("Produtos")]
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório!")]
        [StringLength(80)]
        [PrimeiraLetraMaiuscula]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "Vicê precisa de uma descrição")]
        [StringLength(300, ErrorMessage = "A descrição deve ter no máximo 300 caracteres")]
        public string?  Descricao { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Preco { get; set; }
        public string? ImagemUrl { get; set; }
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }
        public int CategoriaId { get; set; }

        [JsonIgnore]
        public Categoria? Categoria { get; set; }
    }
}
