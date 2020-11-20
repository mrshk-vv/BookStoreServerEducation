using System.Collections.Generic;
using System.Threading.Tasks;
using Store.DataAccess.Entities;
using Store.Shared.Filters;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IPrintingEditionRepository
    {
        public Task<IEnumerable<PrintingEdition>> GetEditionsAsync();
        public Task<IEnumerable<PrintingEdition>> GetEditionsAsync(int skip, int pageSize);
        public Task<IEnumerable<PrintingEdition>> GetEditionsAsync(int skip, int pageSize, PrintingEditionFilter filter);
        public Task<PrintingEdition> GetEditionByIdAsync(string id);
        public Task<PrintingEdition> CreateEditionAsync(PrintingEdition entity);
        public Task<PrintingEdition> RemoveEditionAsync(PrintingEdition entity);
        public Task DeleteEditionAsync(PrintingEdition entity);
        public Task<PrintingEdition> UpdateEditionAsync(PrintingEdition entity);
        public Task<PrintingEdition> GetEditionByTitle(string title);
        public Task<PrintingEdition> GetEditionByTitle(string title, int peId);
    }
}
