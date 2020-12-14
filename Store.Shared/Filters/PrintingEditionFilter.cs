using System;

namespace Store.Shared.Filters
{
    public class PrintingEditionFilter
    {
        public string SearchString { get; set; }
        public Enums.Enums.Edition[] Types { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public Enums.Enums.Currency? Currency { get; set; }
        public bool? Sort { get; set; }
    }
}
