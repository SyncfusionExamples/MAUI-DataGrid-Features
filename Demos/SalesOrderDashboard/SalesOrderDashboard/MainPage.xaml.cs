using Syncfusion.Maui.DataGrid;
using Syncfusion.Maui.DataGrid.Exporting;
using Syncfusion.Pdf;
using SalesOrderDashboard.ViewModel;
using SalesOrderDashboard.Model;
using Syncfusion.Maui.Popup;

namespace SalesOrderDashboard
{
    public partial class MainPage : ContentPage
    {
        private OrderInfo? _originalRow;
        private OrderInfo? _editableCopy;
        private SfPopup? _popup;

        private class GridLineOption
        {
            public string Text { get; set; } = string.Empty;
            public string Icon { get; set; } = string.Empty;
            public GridLinesVisibility Value { get; set; }
        }

        public MainPage()
        {
            InitializeComponent();

            pageSizePicker.SelectedIndexChanged += (s, e) =>
            {
                if (int.TryParse(pageSizePicker.SelectedItem?.ToString(), out var size))
                {
                    dataPager.PageSize = size;
                }
            };

            pageSizePicker.SelectedIndex = 0;

            // Populate grid line options combo
            var lineOptions = new List<GridLineOption>
            {
                new GridLineOption { Text = "All borders", Icon = "all.png", Value = GridLinesVisibility.Both },
                new GridLineOption { Text = "Horizontal borders", Icon = "horizontal.png", Value = GridLinesVisibility.Horizontal },
                new GridLineOption { Text = "Vertical borders", Icon = "vertical.png", Value = GridLinesVisibility.Vertical },
                new GridLineOption { Text = "None", Icon = "none.png", Value = GridLinesVisibility.None },
            };

            LinesComboBox.ItemsSource = lineOptions;
            LinesComboBox.SelectedIndex = 0;
        }

        private void OnExportToPdfClicked(object? sender, EventArgs e)
        {
            MemoryStream stream = new MemoryStream();
            DataGridPdfExportingController pdfExport = new DataGridPdfExportingController();
            DataGridPdfExportingOption option = new DataGridPdfExportingOption();
            option.CanFitAllColumnsInOnePage = true;
            var pdfDoc = new PdfDocument();
            pdfDoc = pdfExport.ExportToPdf(this.dataGrid, option);
            pdfDoc.Save(stream);
            pdfDoc.Close(true);
            SaveServices saveService = new();
            saveService.SaveAndView("Order_details.pdf", "application/pdf", stream);

        }

        private void OnExportToExcelClicked(object? sender, EventArgs e)
        {
            DataGridExcelExportingController excelExport = new DataGridExcelExportingController();
            DataGridExcelExportingOption option = new DataGridExcelExportingOption();
            var excelEngine = excelExport.ExportToExcel(dataGrid, option);
            var workbook = excelEngine.Excel.Workbooks[0];
            MemoryStream stream = new MemoryStream();
            workbook.SaveAs(stream);
            workbook.Close();
            excelEngine.Dispose();
            string OutputFilename = "Order_details.xlsx";
            SaveServices saveService = new();
            saveService.SaveAndView(OutputFilename, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", stream);
        }

        private void searchBar_SearchButtonPressed(object? sender, EventArgs e)
        {
            this.dataGrid.SearchController.AllowFiltering = true;
            this.dataGrid.SearchController.Search(searchBar.Text);
        }

        private void searchBar_TextChanged(object? sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                dataGrid.SearchController.ClearSearch();
            }
        }

        private void CustomerComboBox_SelectionChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            dataGrid.SortColumnDescriptions.Clear();
            if (e.AddedItems != null && e.AddedItems.Count > 0 && e.AddedItems[0] != null)
            {
                dataGrid.SortColumnDescriptions.Add(new SortColumnDescription()
                {
                    ColumnName = e.AddedItems[0]!.ToString()!,
                    SortDirection = System.ComponentModel.ListSortDirection.Ascending

                });
            }

        }

        private void LinesComboBox_SelectionChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            if (LinesComboBox.SelectedItem is GridLineOption opt)
            {
                dataGrid.GridLinesVisibility = opt.Value;
                dataGrid.HeaderGridLinesVisibility = opt.Value;
            }
        }

        private void OnDeleteRowClicked(object? sender, EventArgs e)
        {
            if (sender is BindableObject bindable && bindable.BindingContext is OrderInfo row)
            {
                if (this.BindingContext is OrderInfoRepository vm && vm.OrderInfoCollection.Contains(row))
                {
                    vm.OrderInfoCollection.Remove(row);
                }
            }
        }

        private View BuildEditContent()
        {
            var grid = new Grid
            {
                Padding = new Thickness(16),
                RowSpacing = 10,
                ColumnSpacing = 10,
                RowDefinitions =
                {
                    new RowDefinition(GridLength.Auto),
                    new RowDefinition(GridLength.Auto),
                    new RowDefinition(GridLength.Auto),
                    new RowDefinition(GridLength.Auto),
                    new RowDefinition(GridLength.Auto),
                    new RowDefinition(GridLength.Auto),
                    new RowDefinition(GridLength.Auto),
                    new RowDefinition(GridLength.Auto),
                    new RowDefinition(GridLength.Auto),
                    new RowDefinition(GridLength.Auto),
                    new RowDefinition(GridLength.Auto),
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition(GridLength.Auto),
                    new ColumnDefinition(GridLength.Star)
                }
            };

            int r = 0;
            grid.Add(new Label { Text = "Order ID", VerticalOptions = LayoutOptions.Center }, 0, r);
            var orderIdEntry = new Entry { Keyboard = Keyboard.Numeric };
            orderIdEntry.SetBinding(Entry.TextProperty, nameof(OrderInfo.OrderID));
            grid.Add(orderIdEntry, 1, r++);

            grid.Add(new Label { Text = "Customer name", VerticalOptions = LayoutOptions.Center }, 0, r);
            var customerNameEntry = new Entry();
            customerNameEntry.SetBinding(Entry.TextProperty, nameof(OrderInfo.CustomerName));
            grid.Add(customerNameEntry, 1, r++);

            grid.Add(new Label { Text = "Customer ID", VerticalOptions = LayoutOptions.Center }, 0, r);
            var customerIdEntry = new Entry();
            customerIdEntry.SetBinding(Entry.TextProperty, nameof(OrderInfo.CustomerID));
            grid.Add(customerIdEntry, 1, r++);

            grid.Add(new Label { Text = "Product ID", VerticalOptions = LayoutOptions.Center }, 0, r);
            var productIdEntry = new Entry();
            productIdEntry.SetBinding(Entry.TextProperty, nameof(OrderInfo.ProductID));
            grid.Add(productIdEntry, 1, r++);

            grid.Add(new Label { Text = "Order Date", VerticalOptions = LayoutOptions.Center }, 0, r);
            var datePicker = new DatePicker();
            datePicker.SetBinding(DatePicker.DateProperty, nameof(OrderInfo.OrderDate));
            grid.Add(datePicker, 1, r++);

            grid.Add(new Label { Text = "Quantity", VerticalOptions = LayoutOptions.Center }, 0, r);
            var qtyEntry = new Entry { Keyboard = Keyboard.Numeric };
            qtyEntry.SetBinding(Entry.TextProperty, nameof(OrderInfo.Quantity));
            grid.Add(qtyEntry, 1, r++);

            grid.Add(new Label { Text = "Freight", VerticalOptions = LayoutOptions.Center }, 0, r);
            var freightEntry = new Entry { Keyboard = Keyboard.Numeric };
            freightEntry.SetBinding(Entry.TextProperty, nameof(OrderInfo.Freight));
            grid.Add(freightEntry, 1, r++);

            grid.Add(new Label { Text = "Ship Country", VerticalOptions = LayoutOptions.Center }, 0, r);
            var shipCountryEntry = new Entry();
            shipCountryEntry.SetBinding(Entry.TextProperty, nameof(OrderInfo.ShipCountry));
            grid.Add(shipCountryEntry, 1, r++);

            grid.Add(new Label { Text = "Ship City", VerticalOptions = LayoutOptions.Center }, 0, r);
            var shipCityEntry = new Entry();
            shipCityEntry.SetBinding(Entry.TextProperty, nameof(OrderInfo.ShipCity));
            grid.Add(shipCityEntry, 1, r++);

            grid.Add(new Label { Text = "Payment Status", VerticalOptions = LayoutOptions.Center }, 0, r);
            var statusPicker = new Picker();
            statusPicker.Items.Add("Paid");
            statusPicker.Items.Add("Not Paid");
            statusPicker.SetBinding(Picker.SelectedItemProperty, nameof(OrderInfo.PaymentStatus));
            grid.Add(statusPicker, 1, r++);

            var buttons = new HorizontalStackLayout { Spacing = 12, HorizontalOptions = LayoutOptions.End, Margin = new Thickness(0, 10, 0, 0) };
            var cancelBtn = new Button { Text = "Cancel" };
            var saveBtn = new Button { Text = "Save" };
            buttons.Children.Add(cancelBtn);
            buttons.Children.Add(saveBtn);
            grid.Add(buttons, 0, r);
            Grid.SetColumnSpan(buttons, 2);

            cancelBtn.Clicked += (s, e) =>
            {
                if (_popup != null) _popup.IsOpen = false;
                _editableCopy = null;
                _originalRow = null;
            };

            saveBtn.Clicked += (s, e) =>
            {
                if (_originalRow != null && _editableCopy != null)
                {
                    _originalRow.OrderID = _editableCopy.OrderID;
                    _originalRow.CustomerID = _editableCopy.CustomerID;
                    _originalRow.CustomerName = _editableCopy.CustomerName;
                    _originalRow.ProductID = _editableCopy.ProductID;
                    _originalRow.OrderDate = _editableCopy.OrderDate;
                    _originalRow.Quantity = _editableCopy.Quantity;
                    _originalRow.Freight = _editableCopy.Freight;
                    _originalRow.ShipCountry = _editableCopy.ShipCountry;
                    _originalRow.ShipCity = _editableCopy.ShipCity;
                    _originalRow.PaymentStatus = _editableCopy.PaymentStatus;
                }

                if (_popup != null) _popup.IsOpen = false;
                _editableCopy = null;
                _originalRow = null;
            };

            var scroll = new ScrollView { Content = grid };
            return scroll;
        }

        private void OpenEditPopup(OrderInfo row)
        {
            _originalRow = row;
            _editableCopy = new OrderInfo
            {
                OrderID = row.OrderID,
                CustomerID = row.CustomerID,
                CustomerName = row.CustomerName,
                CustomerImage = row.CustomerImage,
                ProductID = row.ProductID,
                OrderDate = row.OrderDate,
                Quantity = row.Quantity,
                Freight = row.Freight,
                ShipCountry = row.ShipCountry,
                ShipCity = row.ShipCity,
                PaymentStatus = row.PaymentStatus,
                Rating = row.Rating,
                IsSelected = row.IsSelected
            };

            _popup = new SfPopup
            {
                HeaderTitle = "Edit Order",
                ShowCloseButton = true,
                WidthRequest = 420,
                HeightRequest = 420,
                ContentTemplate = new DataTemplate(() => BuildEditContent())
            };

            _popup.BindingContext = _editableCopy;
            _popup.IsOpen = true;
        }

        private void OnEditRowClicked(object? sender, EventArgs e)
        {
            if (sender is BindableObject bindable && bindable.BindingContext is OrderInfo row)
            {
                OpenEditPopup(row);
            }
        }
    }
}
