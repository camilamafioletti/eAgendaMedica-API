using eAgendaMedica.Dominio.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace eAgendaMedica.Infra.Orm.Compartilhado
{
    public class RepositorioBase<TEntidade> where TEntidade : Entidade
    {
        protected eAgendaMedicaDbContext dbContext;
        protected DbSet<TEntidade> registros;

        public RepositorioBase(IContextoPersistencia ctx)
        {
            this.dbContext = (eAgendaMedicaDbContext)ctx;
            this.registros = dbContext.Set<TEntidade>();
        }

        public async Task<bool> InserirAsync(TEntidade registro)
        {
            await registros.AddAsync(registro);

            return true;
        }

        public void Editar(TEntidade registro)
        {
            registros.Update(registro);
        }

        public void Excluir(TEntidade registro)
        {
            registros.Remove(registro);
        }

        public virtual TEntidade SelecionarPorId(Guid id)
        {
            return registros.SingleOrDefault(x => x.Id == id);
        }

        public async virtual Task<TEntidade> SelecionarPorIdAsync(Guid id)
        {
            return await Task.Run(() => registros.SingleOrDefaultAsync(x => x.Id == id));
        }

        public async virtual Task<List<TEntidade>> SelecionarTodosAsync()
        {
            return await registros.ToListAsync();
        }
    }
}
