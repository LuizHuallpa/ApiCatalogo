using ApiCatalogo.Filters;
using ApiCatalogo.Models;
using ApiCatalogo.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnityOfWork _uow;

        public ProdutosController(IUnityOfWork context)
        {
            _uow = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> GetProdutosPrecos()
        {
            return _uow.ProdutoRepository.GetProdutosPorPreco().ToList();
        }

        [HttpGet("menorpreco")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            return _uow.ProdutoRepository.Get().ToList();
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _uow.ProdutoRepository.GetById(p => p.ProdutoId == id);
            if (produto is null)
            {
                return NotFound("Produto não encontrado");
            }
            return produto;
        }

        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            if (produto is null)
                return BadRequest();

            _uow.ProdutoRepository.Add(produto);
            _uow.Commit();

            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);

        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, [FromBody] Produto produto)
        {
            if (id != produto.ProdutoId)
                return BadRequest("Produto não encontrado");

            _uow.ProdutoRepository.Update(produto);
            _uow.Commit();

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public ActionResult<Produto> Delete(int id)
        {
            var produto = _uow.ProdutoRepository.GetById(p => p.ProdutoId == id);
            // var produto = _uow.Produtos.Find(id);

            if (produto is null)
                return NotFound("Produto não encontrado");

            _uow.ProdutoRepository.Delete(produto);
            _uow.Commit();

            return produto;
        }
    }
}
