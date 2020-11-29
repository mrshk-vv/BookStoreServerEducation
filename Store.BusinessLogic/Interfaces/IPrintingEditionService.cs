using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Store.BusinessLogic.Models.PrintingEdition;
using Store.DataAccess.Entities;
using Store.Shared.Filters;
using Store.Shared.Pagination;

namespace Store.BusinessLogic.Interfaces
{
    public interface IPrintingEditionService
    {
        public Task<PrintingEditionModel> CreateEditionAsync(PrintingEditionItemModel model);
        public Task<IEnumerable<PrintingEditionModel>> GetAllEditionsAsync();
        public Task<IEnumerable<PrintingEditionModel>> GetAllEditionsAsync(PaginationQuery paginationFilter, PrintingEditionFilter filter);
        public Task<PrintingEditionModel> GetEditionAsync(string id);
        public Task<PrintingEditionModel> UpdateEditionAsync(PrintingEditionItemModel model);
        public Task<PrintingEditionModel> RemoveEditionAsync(string id);
        public Task DeleteEditionAsync(string id);

    }
}
