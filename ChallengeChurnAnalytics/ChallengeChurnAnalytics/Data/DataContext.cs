using ChallengeChurnAnalytics.Models;
using Microsoft.EntityFrameworkCore;

namespace ChallengeChurnAnalytics.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<CadastroEmpresa> CadastroEmpresas { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<InfoAdicional> InfoAdicionais { get; set; }


    }
}
