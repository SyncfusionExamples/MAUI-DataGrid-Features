using Syncfusion.Maui.DataGrid;
using Syncfusion.Maui.Inputs;

namespace Selection
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void modeComboBox_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            var comboBox = sender as SfComboBox;

            switch (comboBox?.SelectedIndex)
            {
                case 0:
                    dataGrid.SelectionMode = Syncfusion.Maui.DataGrid.DataGridSelectionMode.Single;
                    break;

                case 1:
                    dataGrid.SelectionMode = Syncfusion.Maui.DataGrid.DataGridSelectionMode.Multiple;
                    break;

                case 2:
                    dataGrid.SelectionMode = Syncfusion.Maui.DataGrid.DataGridSelectionMode.SingleDeselect;
                    break;

                case 3:
                    dataGrid.SelectionMode = Syncfusion.Maui.DataGrid.DataGridSelectionMode.Extended;
                    break;

                case 4:
                    dataGrid.SelectionMode = Syncfusion.Maui.DataGrid.DataGridSelectionMode.None;
                    break;

                case -1:
                    dataGrid.SelectionMode = Syncfusion.Maui.DataGrid.DataGridSelectionMode.None;
                    break;
            }
        }

        private void unitComboBox_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            var comboBox = sender as SfComboBox;

            switch (comboBox?.SelectedIndex)
            {
                case 0:
                    dataGrid.SelectionUnit = DataGridSelectionUnit.Any;
                    break;

                case 1:
                    dataGrid.SelectionUnit = DataGridSelectionUnit.Cell;
                    break;

                case 2:
                    dataGrid.SelectionUnit = DataGridSelectionUnit.Row;
                    break;

                case -1:
                    dataGrid.SelectionUnit = DataGridSelectionUnit.Any;
                    break;
            }
        }
    }
}
