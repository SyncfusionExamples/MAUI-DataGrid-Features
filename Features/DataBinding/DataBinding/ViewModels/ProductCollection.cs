using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Text;

namespace DataBinding.ViewModels
{
    public class ProductCollection
    {
        Random? random = new Random();

        public ProductCollection()
        {
            ProductDetails = GetProductDetails_Dynamic(200);
        }

        #region ItemsSource

        private ObservableCollection<dynamic>? _productDetails;
        public ObservableCollection<dynamic>? ProductDetails
        {
            get { return _productDetails; }
            set
            {
                _productDetails = value;
                RaisePropertyChanged("ProductDetails");
            }
        }

        #endregion

        // -------------------------------
        // Dynamic DataSource
        // -------------------------------
        public ObservableCollection<dynamic> GetProductDetails_Dynamic(int count)
        {
            var products = new ObservableCollection<dynamic>();
            for (int i = 1; i <= count; i++)
            {
                products.Add(GetDynamicProduct(i));
            }
            return products;
        }

        // -------------------------------
        // Dynamic Object
        // -------------------------------
        public dynamic GetDynamicProduct(int i)
        {
            dynamic product = new ExpandoObject();

            product.ProductCode = $"PRD-{i:000}";
            product.ProductName = productNames[random.Next(productNames.Length)];
            product.Category = categories[random.Next(categories.Length)];
            product.Quantity = random.Next(0, 500);

            // Unit price as decimal (currency formatting done in grid)
            product.UnitPrice = Math.Round((decimal)(random.NextDouble() * 900 + 100), 2);

            product.LastUpdated = DateTime.Today.AddDays(-random.Next(1, 60));
            product.IsActive = product.Quantity > 0;

            return product;
        }

        // -------------------------------
        // Lookup Data
        // -------------------------------
        string[] productNames =
        {
        "Wireless Mouse",
        "Laptop Pro 14",
        "Mechanical Keyboard",
        "Noise Cancel Headset",
        "Smart Monitor",
        "Office Chair",
        "Standing Desk"
        };

        string[] categories =
        {
        "Electronics",
        "Accessories",
        "Furniture",
        "Stationery",
        "Appliances"
        };

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string? propertyName)
        {
            PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
    }
}
