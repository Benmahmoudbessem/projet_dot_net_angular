using Microsoft.AspNetCore.Mvc;
using BibliothequeAPI.Models;
using BibliothequeAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BibliothequeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateursController : ControllerBase
    {
        private readonly UtilisateurRepository _utilisateurRepository;

        public UtilisateursController(UtilisateurRepository utilisateurRepository)
        {
            _utilisateurRepository = utilisateurRepository;
        }

        // GET: api/Utilisateurs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Utilisateur>>> GetUtilisateurs()
        {
            var utilisateurs = await _utilisateurRepository.GetAllUtilisateursAsync();
            return Ok(utilisateurs);
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Utilisateur>> GetUtilisateur(int id)
        {
            var utilisateur = await _utilisateurRepository.GetUtilisateurByIdAsync(id);

            if (utilisateur == null)
            {
                return NotFound();
            }

            return utilisateur;
        }

        // POST: api/Utilisateurs
        [HttpPost]
        public async Task<ActionResult<Utilisateur>> PostUtilisateur([FromBody] Utilisateur utilisateur)

        {
            await _utilisateurRepository.AddUtilisateurAsync(utilisateur);
            return CreatedAtAction(nameof(GetUtilisateur), new { id = utilisateur.Id }, utilisateur);
        }

        // PUT: api/Utilisateurs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUtilisateur(int id, [FromBody] Utilisateur utilisateur)
        {
            if (id != utilisateur.Id)
            {
                return BadRequest("L'ID dans l'URL ne correspond pas à l'ID du modèle.");
            }

            // Validation du modèle
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Cela retournera toutes les erreurs de validation
            }

            await _utilisateurRepository.UpdateUtilisateurAsync(utilisateur);
            return NoContent();
        }


        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilisateur(int id)
        {
            var result = await _utilisateurRepository.DeleteUtilisateurAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
