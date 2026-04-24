using Syncfusion.Maui.DataGrid;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DataOperations
{
    public class ProductInventoryViewModel : INotifyPropertyChanged
    {
        private readonly Random random = new();

        public ObservableCollection<ProductInventoryModel> Products { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));



        private bool _isGroupingEnabled;
        public bool IsGroupingEnabled
        {
            get => _isGroupingEnabled;
            set
            {
                if (_isGroupingEnabled == value) return;
                _isGroupingEnabled = value;
                OnPropertyChanged();
                UpdateGroupingMode();
            }
        }

        private bool _isSortingEnabled;
        public bool IsSortingEnabled
        {
            get => _isSortingEnabled;
            set
            {
                if (_isSortingEnabled == value) return;
                _isSortingEnabled = value;
                OnPropertyChanged();

                UpdateSortingMode();
            }
        }

        private bool _isFilteringEnabled;
        public bool IsFilteringEnabled
        {
            get => _isFilteringEnabled;
            set
            {
                if (_isFilteringEnabled == value) return;
                _isFilteringEnabled = value;
                OnPropertyChanged();
            }
        }

        public SfDataGrid DataGrid { get; set; }

        private void UpdateSortingMode()
        {
            if (DataGrid == null)
                return;

            if (IsSortingEnabled)
            {
                DataGrid.SortingMode = DataGridSortingMode.Multiple;
            }
            else
            {
                DataGrid.SortColumnDescriptions.Clear();
                DataGrid.SortingMode = DataGridSortingMode.None;
            }
        }


        private void UpdateGroupingMode()
        {
            if (DataGrid == null)
                return;

            if (IsGroupingEnabled)
            {
                // ✅ Enable grouping UI
                DataGrid.AllowGrouping = true;
            }
            else
            {
                // ✅ Disable grouping UI
                DataGrid.AllowGrouping = false;

                // ✅ CRITICAL: Clear existing group columns
                DataGrid.GroupColumnDescriptions.Clear();

            }
        }




        // ✅ Domain collections
        private readonly string[] productNames =
        {
            "Wireless Mouse", "Mechanical Keyboard", "LED Monitor",
            "Bluetooth Speaker", "USB-C Hub", "External Hard Drive",
            "Smart Plug", "Noise Cancelling Headphones",
            "Web Camera", "Laptop Stand"
        };

        private readonly string[] categories =
        {
            "Electronics", "Accessories", "Computer Peripherals", "Smart Devices"
        };

        private readonly string[] suppliers =
        {
            "TechSource Ltd", "Global Electronics", "NextGen Supplies",
            "Prime Distributors", "Innova Traders"
        };

        private readonly string[] warehouses =
        {
            "Chennai-WH", "Bangalore-WH", "Hyderabad-WH", "Mumbai-WH"
        };

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
