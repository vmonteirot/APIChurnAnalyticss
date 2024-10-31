using ChallengeChurnAnalytics.Data;
using ChallengeChurnAnalytics.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeChurnAnalytics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CadastroEmpresaController : ControllerBase
    {
        private readonly DataContext _context;

        public CadastroEmpresaController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Recupera todas as empresas do banco de dados.
        /// </summary>
        /// <returns>Uma lista de empresas.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CadastroEmpresa>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CadastroEmpresa>>> GetEmpresas()
        {
            return await _context.CadastroEmpresas.ToListAsync();
        }

        /// <summary>
        /// Recupera uma empresa específica pelo ID.
        /// </summary>
        /// <param name="id">O ID da empresa a ser recuperada.</param>
        /// <returns>A empresa com o ID especificado.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CadastroEmpresa))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CadastroEmpresa>> GetEmpresa(int id)
        {
            var empresa = await _context.CadastroEmpresas.FindAsync(id);
            if (empresa == null)
            {
                return NotFound();
            }
            return empresa;
        }

        /// <summary>
        /// Cria uma nova empresa.
        /// </summary>
        /// <param name="empresa">A empresa a ser criada.</param>
        /// <returns>A empresa criada.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CadastroEmpresa))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CadastroEmpresa>> PostEmpresa(CadastroEmpresa empresa)
        {
            _context.CadastroEmpresas.Add(empresa);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetEmpresa", new { id = empresa.Id }, empresa);
        }

        /// <summary>
        /// Atualiza uma empresa existente.
        /// </summary>
        /// <param name="id">O ID da empresa a ser atualizada.</param>
        /// <param name="empresa">Os dados atualizados da empresa.</param>
        /// <returns>Sem conteúdo se a atualização for bem-sucedida.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutEmpresa(int id, CadastroEmpresa empresa)
        {
            if (id != empresa.Id)
            {
                return BadRequest();
            }

            _context.Entry(empresa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpresaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        /// <summary>
        /// Remove uma empresa específica pelo ID.
        /// </summary>
        /// <param name="id">O ID da empresa a ser removida.</param>
        /// <returns>Sem conteúdo se a remoção for bem-sucedida.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteEmpresa(int id)
        {
            var empresa = await _context.CadastroEmpresas.FindAsync(id);
            if (empresa == null)
            {
                return NotFound();
            }
            _context.CadastroEmpresas.Remove(empresa);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool EmpresaExists(int id)
        {
            return _context.CadastroEmpresas.Any(e => e.Id == id);
        }
    }
}