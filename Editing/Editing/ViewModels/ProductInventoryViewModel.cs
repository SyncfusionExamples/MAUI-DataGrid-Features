using Editing.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Editing.ViewModels
{
    public class ProductInventoryViewModel
    {
        private readonly Random random = new();

        public ObservableCollection<ProductInventoryModel> Products { get; set; }

        // ✅ Domain collections
        private readonly string[] productNames =
        {
            "Wireless Mouse", "Mechanical Keyboard", "LED Monitor",
            "Bluetooth Speaker", "USB-C Hub", "External Hard Drive",
            "Smart Plug", "Noise Cancelling Headphones",
            "Web Camera", "Laptop Stand"
        };

        public string[] ProductNames { get { return productNames; } }
        private readonly string[] categories =
        {
            "Electronics", "Accessories", "Computer Peripherals", "Smart Devices"
        };

        public string[] Categories { get { return categories; } }
        private readonly string[] suppliers =
        {
            "TechSource Ltd", "Global Electronics", "NextGen Supplies",
            "Prime Distributors", "Innova Traders"
        };
        public string[] Suppliers { get { return suppliers; } }
        private readonly string[] warehouses =
        {
            "Chennai-WH", "Bangalore-WH", "Hyderabad-WH", "Mumbai-WH"
        };
        public string[] Warehouses { get { return warehouses; } }

        private readonly string[] skus =
        {
            "WM-AX92", "KB-MX11", "MN-LD45", "SP-BT77",
            "HB-UC10", "HD-EX22", "PL-SM09",
            "HP-NC55", "WC-HD33", "LS-ST88"
        };

        public ProductInventoryViewModel()
        {
            Products = new ObservableCollection<ProductInventoryModel>();
            GenerateProducts(5000);
        }

        private void GenerateProducts(int count)
        {
            for (int i = 0; i < count; i++)
            {
                int quantity = random.Next(0, 400);
                int reorderLevel = random.Next(30, 80);
                double unitPrice = random.Next(800, 4500);

                var product = new ProductInventoryModel
                {
                    ProductId = i + 1,
                    ProductName = productNames[i % productNames.Length],
                    SKU = skus[i % skus.Length],
                    Category = categories[i % categories.Length],
                    Supplier = suppliers[i % suppliers.Length],
                    QuantityInStock = quantity,
                    ReorderLevel = reorderLevel,
                    UnitPrice = unitPrice,
                    Discount = random.Next(0, 20),
                    TotalValue = quantity * unitPrice,
                    IsDiscontinued = random.Next(0, 20) == 5,
                    ManufactureDate = DateTime.Now.AddMonths(-random.Next(1, 18)),
                    ExpiryDate = DateTime.Now.AddMonths(random.Next(6, 36)),
                    WarehouseLocation = warehouses[i % warehouses.Length],
                    StockStatus = GetStockStatus(quantity, reorderLevel)
                };

                Products.Add(product);
            }
        }

        private string GetStockStatus(int quantity, int reorderLevel)
        {
            if (quantity == 0)
                return "Out of Stock";

            if (quantity < reorderLevel)
                return "Low Stock";

            return "In Stock";
        }
    }
}
