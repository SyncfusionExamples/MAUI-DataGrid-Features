using SalesOrderDashboard.Model;
using System.Collections.ObjectModel;

namespace SalesOrderDashboard.ViewModel
{
    public class OrderInfoRepository
    {
        public ObservableCollection<OrderInfo> OrderInfoCollection { get; set; }

        public ObservableCollection<string> SortableProperties { get; set; } = new ObservableCollection<string>
        {
            "OrderID",
            "CustomerName",
            "CustomerID",
            "ProductID",
            "OrderDate",
            "Quantity",
            "Freight",
            "ShipCountry",
            "ShipCity",
            "PaymentStatus",
            "Rating"
        };


        public OrderInfoRepository()
        {
            OrderInfoCollection = new ObservableCollection<OrderInfo>();
            GenerateOrders();
        }

        private static readonly string[] Names = new[]
        {
            "Emma", "Olivia", "Charlotte", "Amelia", "Sophia",
            "William", "Isabella", "Emily", "George", "Grace",
            "Lily","Ava","Chloe","Ella","Mia","James","Sophie",
            "Isla","Henry","Lucy"
        };

        private static readonly string[] Countries = new[]
        {
            "Spain","Brazil","Switzerland","France","Germany","UK","Canada","Mexico","USA","Italy"
        };

        private static readonly string[] Cities = new[]
        {
            "Madrid","Rio","Bern","Paris","Berlin","London","Toronto","Mexico D.F.","Seattle","Rome"
        };

        private static readonly string[] PaymentStatuses = new[] { "Paid", "Not Paid" };

        private void GenerateOrders()
        {
            var rnd = new Random(42);
            int baseOrderId = 20251001;
            for (int i = 0; i < 100; i++)
            {
                int orderId = baseOrderId + i;
                var name = Names[i % Names.Length];
                var custId = $"C{100 + (i % 50):D3}";
                string productId = $"PRD-{(char)('A' + i % 5)}X{100 + i:D3}";
                DateTime orderDate = DateTime.Today.AddDays(-rnd.Next(1, 90));
                int quantity = rnd.Next(1, 101);
                var freight = Math.Round(rnd.NextDouble() * 120 + 5, 2);
                var country = Countries[i % Countries.Length];
                var city = Cities[i % Cities.Length];
                var status = PaymentStatuses[rnd.Next(PaymentStatuses.Length)];
                var rating = Math.Round(rnd.NextDouble() * 5, 1);
                var isSelected = i % 10 == 0;
                var image = $"people_circle{i % 10}.png";

                OrderInfoCollection.Add(new OrderInfo(
                    orderId: orderId,
                    customerName: name,
                    customerId: custId,
                    productId: productId,
                    orderDate: orderDate,
                    quantity: quantity,
                    freight: freight,
                    shipCountry: country,
                    shipCity: city,
                    paymentStatus: status,
                    rating: rating,
                    customerImage: image,
                    isSelected: false));
            }
        }
    }
}
