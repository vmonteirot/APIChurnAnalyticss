using ChallengeChurnAnalytics.Models;
using ChallengeChurnAnalytics.Data;
using ChallengeChurnAnalytics.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeChurnAnalytics.Repository
{
    public class CadastroEmpresaRepository : ICadastroEmpresaRepository
    {
        private readonly DataContext _dbContext;

        public CadastroEmpresaRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CadastroEmpresa>> GetCadastroEmpresas()
        {
            return await _dbContext.CadastroEmpresas.ToListAsync();
        }

        public async Task<CadastroEmpresa> GetCadastroEmpresaById(int id)
        {
            return await _dbContext.CadastroEmpresas.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<CadastroEmpresa> AddCadastroEmpresa(CadastroEmpresa empresa)
        {
            var result = await _dbContext.CadastroEmpresas.AddAsync(empresa);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<CadastroEmpresa> UpdateCadastroEmpresa(CadastroEmpresa empresa)
        {
            var existingEmpresa = await _dbContext.CadastroEmpresas.FirstOrDefaultAsync(e => e.Id == empresa.Id);
            if (existingEmpresa == null)
            {
                return null;
            }

            existingEmpresa.RazaoSocial = empresa.RazaoSocial;
            existingEmpresa.CNPJ = empresa.CNPJ;

            await _dbContext.SaveChangesAsync();
            return existingEmpresa;
        }

        public async void DeleteCadastroEmpresaById(int id)
        {
            var result = await _dbContext.CadastroEmpresas.FirstOrDefaultAsync(e => e.Id == id);
            if (result != null)
            {
                _dbContext.CadastroEmpresas.Remove(result);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}

