using DataBinding.Models;
using DataBinding.ViewModels;
using Syncfusion.Maui.DataGrid;
using System.Collections.ObjectModel;
using System.Data;
using System.Dynamic;

namespace DataBinding
{
    public partial class MainPage : ContentPage
    {
        private readonly DataTable businessTable = new();

        public MainPage()
        {
            InitializeComponent();
            InitializeBusinessDataTable();
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (((Picker)sender).SelectedIndex)
            {
                case 0:
                    LoadObservableCollection();
                    break;

                case 1:
                    LoadBusinessDataTable();
                    break;

                case 2:
                    LoadDynamicProducts();
                    break;

                case 3:
                    LoadUniqueProductList();
                    break;
            }
        }

        private void ResetGrid()
        {
            dataGrid.Columns.Clear();
            dataGrid.AutoGenerateColumnsMode = AutoGenerateColumnsMode.None;
            dataGrid.ColumnWidthMode = ColumnWidthMode.Auto;
        }

        private void LoadObservableCollection()
        {
            ResetGrid();

            var vm = new ProductInventoryViewModel();
            dataGrid.ItemsSource = vm.Products;

            dataGrid.Columns.Add(new DataGridNumericColumn
            {
                MappingName = "ProductId",
                HeaderText = "Product ID",
                Format = "N0",
                HeaderTextAlignment = TextAlignment.Center,
                CellTextAlignment = TextAlignment.Center
            });

            AddText("ProductName", "Product Name", 150);
            AddText("SKU", "SKU");
            AddText("Category", "Category", 120);
            AddText("Supplier", "Supplier", 150);

            AddNumeric("QuantityInStock", "Quantity");
            AddNumeric("ReorderLevel", "Reorder Level", 120);
            AddText("StockStatus", "Stock Status", 125);
            AddCurrency("UnitPrice", "Unit Price");
            AddNumeric("Discount", "Discount");
            AddCurrency("TotalValue", "Total Value", 130);

            AddDate("ManufactureDate", "Manufactured On", 150);
            AddDate("ExpiryDate", "Expiry Date", 130);

            dataGrid.Columns.Add(new DataGridCheckBoxColumn
            {
                MappingName = "IsDiscontinued",
                HeaderText = "Discontinued",
                Width = 120
            });

            AddText("WarehouseLocation", "Warehouse", 140);
        }

        private void LoadBusinessDataTable()
        {
            ResetGrid();
            dataGrid.ItemsSource = businessTable;

            AddText("ProductCode", "Product Code");
            AddText("ProductName", "Product Name");
            AddText("Category", "Category");
            AddText("Supplier", "Supplier");
            AddText("StockStatus", "Stock Status");

            dataGrid.Columns.Add(new DataGridNumericColumn
            {
                MappingName = "Rating",
                HeaderText = "Rating",
                Format = "N1",
                CellTextAlignment = TextAlignment.End
            });

            AddDate("LaunchDate", "Launch Date");

            dataGrid.Columns.Add(new DataGridCheckBoxColumn
            {
                MappingName = "IsFeatured",
                HeaderText = "Featured"
            });
        }

        private void InitializeBusinessDataTable()
        {
            businessTable.Columns.Add("ProductCode", typeof(string));
            businessTable.Columns.Add("ProductName", typeof(string));
            businessTable.Columns.Add("Category", typeof(string));
            businessTable.Columns.Add("Supplier", typeof(string));
            businessTable.Columns.Add("StockStatus", typeof(string));
            businessTable.Columns.Add("Rating", typeof(double));
            businessTable.Columns.Add("LaunchDate", typeof(DateTime));
            businessTable.Columns.Add("IsFeatured", typeof(bool));

            string[] suppliers = { "Contoso", "Fabrikam", "Northwind", "Litware", "Tailspin" };
            string[] categories = { "Electronics", "Furniture", "Accessories", "Appliances" };
            string[] status = { "In Stock", "Low Stock", "Out of Stock" };

            var random = new Random();

            for (int i = 1; i <= 25; i++)
            {
                businessTable.Rows.Add(
                    $"DT-{i:000}",
                    $"Business Product {i}",
                    categories[random.Next(categories.Length)],
                    suppliers[random.Next(suppliers.Length)],
                    status[random.Next(status.Length)],
                    Math.Round(random.NextDouble() * 2 + 3, 1),
                    DateTime.Today.AddMonths(-random.Next(1, 24)),
                    random.Next(0, 2) == 1
                );
            }
        }

        private void LoadDynamicProducts()
        {
            ResetGrid();

            var vm = new ProductCollection();
            dataGrid.ItemsSource = vm.ProductDetails;

            dataGrid.Columns.Add(new DataGridTextColumn { MappingName = "[ProductCode]", HeaderText = "Product Code" });
            dataGrid.Columns.Add(new DataGridTextColumn { MappingName = "[ProductName]", HeaderText = "Product Name" });
            dataGrid.Columns.Add(new DataGridTextColumn { MappingName = "[Category]", HeaderText = "Category" });
            dataGrid.Columns.Add(new DataGridNumericColumn { MappingName = "[Quantity]", HeaderText = "Quantity", Format = "N0" });
            dataGrid.Columns.Add(new DataGridNumericColumn { MappingName = "[UnitPrice]", HeaderText = "Unit Price ($)", Format = "C" });
            dataGrid.Columns.Add(new DataGridDateColumn { MappingName = "[LastUpdated]", HeaderText = "Last Updated", Format = "dd-MMM-yyyy" });
            dataGrid.Columns.Add(new DataGridCheckBoxColumn { MappingName = "[IsActive]", HeaderText = "Active" });
        }

        private void LoadUniqueProductList()
        {
            ResetGrid();
            dataGrid.ItemsSource = GetUniqueProductList();

            AddText("SKU", "SKU Code");
            AddText("Department", "Department");
            AddText("Brand", "Brand");

            AddNumeric("WarrantyYears", "Warranty (Years)");
            AddText("SupportContact", "Support Contact");

            dataGrid.Columns.Add(new DataGridNumericColumn
            {
                MappingName = "ReleaseYear",
                HeaderText = "Release Year",
                Format = "N0",
                CellTextAlignment = TextAlignment.Center
            });

            dataGrid.Columns.Add(new DataGridCheckBoxColumn
            {
                MappingName = "IsPremium",
                HeaderText = "Premium Product"
            });
        }

        private List<ProductList> GetUniqueProductList()
        {
            var list = new List<ProductList>();
            var random = new Random();

            string[] departments = { "IT Assets", "Office Supplies", "Retail", "Logistics" };
            string[] brands = { "Dell", "HP", "Lenovo", "Apple", "Samsung" };

            for (int i = 1; i <= 25; i++)
            {
                list.Add(new ProductList
                {
                    SKU = $"SKU-LST-{i:000}",
                    Department = departments[random.Next(departments.Length)],
                    Brand = brands[random.Next(brands.Length)],
                    WarrantyYears = random.Next(1, 5),
                    SupportContact = $"support{i}@company.com",
                    ReleaseYear = random.Next(2018, 2026),
                    IsPremium = random.Next(0, 2) == 1
                });
            }

            return list;
        }

        private void AddText(string mapping, string header, double width = double.NaN)
        {
            dataGrid.Columns.Add(new DataGridTextColumn
            {
                MappingName = mapping,
                HeaderText = header,
                Width = width
            });
        }

        private void AddNumeric(string mapping, string header, double width = double.NaN)
        {
            dataGrid.Columns.Add(new DataGridNumericColumn
            {
                MappingName = mapping,
                HeaderText = header,
                Format = "N0",
                Width = width,
                CellTextAlignment = TextAlignment.End
            });
        }

        private void AddCurrency(string mapping, string header, double width = double.NaN)
        {
            dataGrid.Columns.Add(new DataGridNumericColumn
            {
                MappingName = mapping,
                HeaderText = header,
                Format = "C",
                Width = width,
                CellTextAlignment = TextAlignment.End
            });
        }

        private void AddDate(string mapping, string header, double width = double.NaN)
        {
            dataGrid.Columns.Add(new DataGridDateColumn
            {
                MappingName = mapping,
                HeaderText = header,
                Format = "dd-MMM-yyyy",
                Width = width
            });
        }
    }
}