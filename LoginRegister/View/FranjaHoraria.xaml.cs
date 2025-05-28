using LoginRegister.Interface;
using LoginRegister.Models;
using LoginRegister.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;


namespace LoginRegister.View;
public partial class FranjaHoraria : UserControl
{
    public FranjaHoraria()
    {
        InitializeComponent();
    }

    private async void MyDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
    {
        if (e.Row.Item is FranjaHorarioDTO franja)
        {
            var servicio = App.Current.Services.GetService<IFranjaHorarioServiceToApi>();
            await servicio!.PutFranja(franja);
            MessageBox.Show("Franja actualizada");
        }
    }

    private void Button_Click()
    {

    }

    private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
    {

    }
}
