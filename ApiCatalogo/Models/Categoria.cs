using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCatalogo.Models;

[Table("Categorias")]
public class Categoria
{
    public Categoria()
    {
        Produtos = new Collection<Produto>();
    }
    [Key]
    public int CategoriaId { get; set; }
    [Required(ErrorMessage = "Name is requited")]
    [StringLength(80)]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "Image link is requited")]
    [StringLength(300)]
    public string? ImagemUrl { get; set; }
    public ICollection<Produto>? Produtos { get; set; }
}
