using Microsoft.AspNetCore.Mvc;
using BibliothequeAPI.Models;
using BibliothequeAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BibliothequeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpruntsController : ControllerBase
    {
        private readonly EmpruntRepository _empruntRepository;

        public EmpruntsController(EmpruntRepository empruntRepository)
        {
            _empruntRepository = empruntRepository;
        }

        // GET: api/Emprunts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Emprunt>>> GetEmprunts()
        {
            var emprunts = await _empruntRepository.GetAllEmpruntsAsync();
            return Ok(emprunts);
        }

        // GET: api/Emprunts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Emprunt>> GetEmprunt(int id)
        {
            var emprunt = await _empruntRepository.GetEmpruntByIdAsync(id);

            if (emprunt == null)
            {
                return NotFound(new { message = "Emprunt non trouvé." });
            }

            return Ok(emprunt);
        }

        // GET: api/Emprunts/utilisateur/5
        [HttpGet("utilisateur/{utilisateurId}")]
        public async Task<ActionResult<IEnumerable<Emprunt>>> GetEmpruntsByUtilisateur(int utilisateurId)
        {
            var emprunts = await _empruntRepository.GetEmpruntsByUtilisateurAsync(utilisateurId);
            if (emprunts == null)
            {
                return NotFound(new { message = "Aucun emprunt trouvé pour cet utilisateur." });
            }

            return Ok(emprunts);
        }

        // POST: api/Emprunts
        [HttpPost]
        public async Task<ActionResult<Emprunt>> PostEmprunt([FromBody] Emprunt emprunt)
        {
            if (emprunt == null)
            {
                return BadRequest(new { message = "Les données de l'emprunt sont requises." });
            }

            try
            {
                var createdEmprunt = await _empruntRepository.AddEmpruntAsync(emprunt);
                return CreatedAtAction(nameof(GetEmprunt), new { id = createdEmprunt.Id }, createdEmprunt);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Retourne une erreur générique si une autre exception se produit
                return StatusCode(500, new { message = "Erreur interne du serveur.", details = ex.Message });
            }
        }

        // PUT: api/Emprunts/retour/5
        [HttpPut("retour/{id}")]
        public async Task<IActionResult> RetournerEmprunt(int id)
        {
            try
            {
                var emprunt = await _empruntRepository.RetournerEmpruntAsync(id);

                if (emprunt == null)
                {
                    return NotFound(new { message = "Emprunt non trouvé." });
                }

                return Ok(new { message = "Emprunt retourné avec succès.", emprunt });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur interne du serveur.", details = ex.Message });
            }
        }

        // DELETE: api/Emprunts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmprunt(int id)
        {
            try
            {
                var result = await _empruntRepository.DeleteEmpruntAsync(id);
                if (!result)
                {
                    return NotFound(new { message = "Emprunt non trouvé." });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur interne du serveur.", details = ex.Message });
            }
        }
    }
}
