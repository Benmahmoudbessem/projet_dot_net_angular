using Microsoft.AspNetCore.Mvc;
using BibliothequeAPI.Models;
using BibliothequeAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BibliothequeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivresController : ControllerBase
    {
        private readonly LivreRepository _livreRepository;

        public LivresController(LivreRepository livreRepository)
        {
            _livreRepository = livreRepository;
        }

        // GET: api/Livres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livre>>> GetLivres()
        {
            var livres = await _livreRepository.GetAllLivresAsync();
            return Ok(livres);
        }

        // GET: api/Livres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Livre>> GetLivre(int id)
        {
            var livre = await _livreRepository.GetLivreByIdAsync(id);

            if (livre == null)
            {
                return NotFound();
            }

            return livre;
        }

        // POST: api/Livres
        [HttpPost]
        public async Task<ActionResult<Livre>> PostLivre([FromBody] Livre livre)
        {
            await _livreRepository.AddLivreAsync(livre);
            return CreatedAtAction(nameof(GetLivre), new { id = livre.Id }, livre);
        }

        // PUT: api/Livres/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLivre(int id, [FromBody] Livre livre)
        {
            if (id != livre.Id)
            {
                return BadRequest();
            }

            await _livreRepository.UpdateLivreAsync(livre);
            return NoContent();
        }

        // DELETE: api/Livres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLivre(int id)
        {
            var result = await _livreRepository.DeleteLivreAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
