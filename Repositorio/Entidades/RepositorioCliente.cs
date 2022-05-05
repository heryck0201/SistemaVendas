using Dominio.Repositorio;
using Microsoft.EntityFrameworkCore;
using Repositorio.Contexto;
using SistemaVendas.Dominio.Entidades;

namespace Repositorio.Entidades
{
   public class RepositorioCliente : Repositorio<Cliente>, IRepositorioCliente
    {
        public RepositorioCliente(ApplicationDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
