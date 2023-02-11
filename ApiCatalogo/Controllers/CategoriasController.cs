using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]///produtos
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;
         
        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasprodutos()
        {
            //return _context.Categorias.Include(x => x.Produtos).AsNoTracking().ToList();
            return _context.Categorias.Include(x => x.Produtos).Where(c=> c.CategoriaId <=5 ).AsNoTracking().ToList();
        }

        [HttpGet("saudacao/{nome}")]
        public ActionResult<string> GetSaudacao([FromServices] IMeuServico meuServico, string nome)
        {
            return meuServico.Saudacao(nome);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            try
            {
                var categorias = _context.Categorias.AsNoTracking().ToList();
                if (categorias is null)
                    return NotFound();
                return categorias;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar obter as categorias do banco de dados");
            }
            

        }



        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {

            try
            {
                var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
                if (categoria is null)
                    return NotFound("Categoria não encontrada");
                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro na sua solicitação");
            }

        }

        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            if (categoria is null)
                return BadRequest();
            
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
            
            return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
        }
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
                return BadRequest();
            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(categoria);
        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
            if (categoria is null)
                return NotFound("Categoria não encontrada");
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
            return Ok(categoria);
        }

    }
}
