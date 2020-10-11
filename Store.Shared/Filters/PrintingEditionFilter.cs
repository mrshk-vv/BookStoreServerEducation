using System;

namespace Store.Shared.Filters
{
    public class PrintingEditionFilter
    {
        public PrintingEditionFilter()
        {
            MaxPrice = Decimal.MaxValue;
        }

        public string Type { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public string PriceSorting { get; set; }
    }
}
