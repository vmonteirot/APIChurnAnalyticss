using ChallengeChurnAnalytics.Models;

namespace ChallengeChurnAnalytics.Repository.Interface
{
    public interface ICadastroEmpresaRepository
    {
        Task<IEnumerable<CadastroEmpresa>> GetCadastroEmpresas(); 
        Task<CadastroEmpresa> GetCadastroEmpresaById(int id); 
        Task<CadastroEmpresa> AddCadastroEmpresa(CadastroEmpresa empresa); 
        Task<CadastroEmpresa> UpdateCadastroEmpresa(CadastroEmpresa empresa); 
        void DeleteCadastroEmpresaById(int id); 
    }
}
