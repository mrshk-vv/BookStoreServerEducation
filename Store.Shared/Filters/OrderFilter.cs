using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Shared.Filters
{
    public class OrderFilter
    {
        public OrderFilter()
        {
            
        }

        public List<string> Status { get; set; }
        public string TypeSorting { get; set; }
    }
}
