using Microsoft.AspNetCore.Mvc;
using API.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArtistaController : ControllerBase
    {
        private readonly EFDBContext _dbContext;
        public ArtistaController(EFDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public static List<Artista> ArtistaLista = new List<Artista>();
        [HttpGet()]
        public IEnumerable<Artista> Get([FromBody] Artista artista)
        {
            return _dbContext.Artista.ToList();
        }
        [HttpPost()]
        public Artista Post([FromBody] Artista artista)
        {
            _dbContext.Artista.Add(artista);
            _dbContext.SaveChanges();
            return artista;
        }
    }
}
