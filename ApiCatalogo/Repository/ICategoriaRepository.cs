using ApiCatalogo.Models;
using ApiCatalogo.Pagination;

namespace ApiCatalogo.Repository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<PagedList<Categoria>> GetCategorias(CategoriasParameters categoriasParameters);
        Task <IEnumerable<Categoria>> GetCategoriasProdutos();
    }
}
