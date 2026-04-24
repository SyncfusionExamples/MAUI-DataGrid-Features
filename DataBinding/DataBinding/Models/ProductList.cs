using System;
using System.Collections.Generic;
using System.Text;

namespace DataBinding.Models
{
    internal class ProductList
    {
        public string SKU { get; set; }
        public string Department { get; set; }
        public string Brand { get; set; }
        public int WarrantyYears { get; set; }
        public string SupportContact { get; set; }
        public int ReleaseYear { get; set; }
        public bool IsPremium { get; set; }
    }
}
