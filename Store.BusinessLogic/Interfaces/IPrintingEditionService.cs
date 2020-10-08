using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Store.BusinessLogic.Models.PrintingEdition;

namespace Store.BusinessLogic.Interfaces
{
    public interface IPrintingEditionService
    {
        public Task<PrintingEditionModel> CreateEditionAsync(PrintingEditionModel model);
        public Task<IEnumerable<PrintingEditionModel>> GetAllEditionsAsync();
        public Task<PrintingEditionModel> GetEditionAsync(string id);
        public Task<PrintingEditionModel> UpdateEditionAsync(PrintingEditionModel model);
        public Task<PrintingEditionModel> RemoveEditionAsync(string id);

    }
}
