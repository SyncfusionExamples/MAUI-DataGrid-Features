using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Editing.Models
{
    public class ProductInventoryModel : IDataErrorInfo
    {
        [Range(1, int.MaxValue, ErrorMessage = "Product ID must be greater than 0")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is required")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "SKU is required")]
        [RegularExpression(@"^[A-Z0-9-]{7}$", ErrorMessage = "The SKU format should be XX-XXXX")]
        public string SKU { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Supplier is required")]
        public string Supplier { get; set; }

        [Range(0, 300, ErrorMessage = "Quantity should not be more than 300")]
        public int QuantityInStock { get; set; }

        [Range(35, int.MaxValue, ErrorMessage = "Reorder Level should not be less than 35")]
        public int ReorderLevel { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Unit Price must be greater than 0")]
        public double UnitPrice { get; set; }

        [Range(1, 20, ErrorMessage = "Discount must be between 1 and 20")]
        public double Discount { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Total Value cannot be negative")]
        public double TotalValue { get; set; }

        public bool IsDiscontinued { get; set; }

        [Required(ErrorMessage = "Manufacture Date is required")]
        [DataType(DataType.Date)]
        public DateTime ManufactureDate { get; set; }

        [Required(ErrorMessage = "Expiry Date is required")]
        [DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }

        [Required(ErrorMessage = "Warehouse Location is required")]
        public string WarehouseLocation { get; set; }

        public string StockStatus { get; set; }

        [Display(AutoGenerateField = false)]
        public string Error
        {
            get
            {
                return string.Empty;
            }
        }

        public string this[string columnName]
        {
            get
            {
                if (!columnName.Equals("StockStatus"))
                    return string.Empty;

                if (this.StockStatus == "Low Stock")
                    return "Stock is Low. Please restock";

                if (this.StockStatus == "Out of Stock")
                    return "Out of Stock. Please restock immediately";

                return string.Empty;
            }
        }
    }
}