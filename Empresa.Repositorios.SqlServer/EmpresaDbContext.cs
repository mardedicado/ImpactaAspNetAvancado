using Empresa.Dominio;
using Microsoft.EntityFrameworkCore;

namespace Empresa.Repositorios.SqlServer
{
    public class EmpresaDbContext: DbContext
    {
        public EmpresaDbContext(DbContextOptions options): base(options)               //Herança. Parâmetro
        {
            Database.EnsureCreated();                   //É o que vai garantir a criação das tabelas
        }

        public DbSet <Contato> Contatos { get; set; }   //Propriedade no plural. Tudo isso para criar. Está
    }
}
