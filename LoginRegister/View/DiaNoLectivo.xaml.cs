using LoginRegister.Models;
using LoginRegister.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;


namespace LoginRegister.View;
public partial class DiaNoLectivo : UserControl
{
    public DiaNoLectivo()
    {
        InitializeComponent();
        //DataContext = App.Current.Services.GetService<DiaNoLectivoViewModel>();
        Loaded += DiaNoLectivo_Loaded;
    }


    private void MyDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
    {
        var dia = e.Row.Item as DiaNoLectivoDTO;
        if (dia != null)
        {
            MessageBox.Show($"Editado: {dia.Fecha.ToShortDateString()} - {dia.Motivo}");
        }
    }

    private async void DiaNoLectivo_Loaded(object sender, RoutedEventArgs e)
    {
        var vm = App.Current.Services.GetService<DiaNoLectivoViewModel>();
        DataContext = vm;

       // MessageBox.Show("✅ ViewModel enlazado correctamente");

        await vm.LoadAsync();
    }

    private void Button_Click()
    {

    }

    private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
    {

    }
}
