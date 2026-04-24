using System;

namespace DataBinding.Models
{
    public class ProductInventoryModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string SKU { get; set; }
        public string Category { get; set; }
        public string Supplier { get; set; }
        public int QuantityInStock { get; set; }
        public int ReorderLevel { get; set; }
        public double UnitPrice { get; set; }
        public double Discount { get; set; }
        public double TotalValue { get; set; }
        public bool IsDiscontinued { get; set; }
        public DateTime ManufactureDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string WarehouseLocation { get; set; }
        public string StockStatus { get; set; }
    }
}