namespace DataOperations
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            if (BindingContext is ProductInventoryViewModel vm)
            {
                vm.DataGrid = dataGrid;
            }
        }
    }
}
