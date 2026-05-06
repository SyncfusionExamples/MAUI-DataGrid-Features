using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Maui.DataGrid;
using System.Globalization;
using System.Resources;

namespace Localization
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            CultureInfo.CurrentUICulture = new CultureInfo("fr-FR");

            SfDataGridResources.ResourceManager = new ResourceManager("Localization.Resources.SfDataGrid", Application.Current.GetType().Assembly);
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}