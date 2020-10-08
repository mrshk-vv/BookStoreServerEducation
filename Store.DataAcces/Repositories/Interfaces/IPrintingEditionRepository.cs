using System.Threading.Tasks;
using Store.DataAccess.Entities;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IPrintingEditionRepository<T> : IBaseRepository<T> where T : class
    {
        public Task<PrintingEdition> GetEdititonByTitle(string title);
    }
}
