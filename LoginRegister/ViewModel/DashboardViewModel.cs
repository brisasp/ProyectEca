using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LoginRegister.Interface;
using LoginRegister.Models;
using LoginRegister.View;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace LoginRegister.ViewModel;

public partial class DashboardViewModel : ViewModelBase
{
    private readonly IReservaServiceToApi _reservaServiceToApi;
   

    public DashboardViewModel(IReservaServiceToApi reservaService)
    {
        _reservaServiceToApi = reservaService;


        Reservas = new List<ReservaDTO>();
        PagedReservas = new ObservableCollection<ReservaDTO>();
        ItemsPerPage = 8;
        CurrentPage = 0;
    }

    private List<ReservaDTO> Reservas;

    [ObservableProperty]
    private ObservableCollection<ReservaDTO> pagedReservas;

    [ObservableProperty]
    private int currentPage;

    [ObservableProperty]
    private ReservaDTO? selectedReserva;


    [ObservableProperty]
    private int itemsPerPage;

    public int TotalPages => (int)Math.Ceiling((double)Reservas.Count / ItemsPerPage);


    public override async Task LoadAsync()
     {
      try
       {

     Reservas.Clear();
       PagedReservas.Clear();

      IEnumerable<ReservaDTO> listaDicatadores = await _reservaServiceToApi.GetReservas();


       // Reservas.AddRange(listaDicatadores.OrderBy(r => r.Fecha));
            var soloPendientes = listaDicatadores
            .Where(r => r.Estado?.Equals("Pendiente", StringComparison.OrdinalIgnoreCase) == true)
            .OrderBy(r => r.Fecha);
            Reservas.AddRange(soloPendientes);
            CurrentPage = 0;
      UpdatePagedReservas();
     }
     catch (Exception ex)
     {

      Console.WriteLine($"Error al cargar datos: {ex.Message}");
     }
     }


    private void UpdatePagedReservas()
    {

        PagedReservas.Clear();

        var pagedItems = Reservas.Skip(CurrentPage * ItemsPerPage).Take(ItemsPerPage).ToList();
        foreach (var item in pagedItems)
        {
            PagedReservas.Add(item);
        }
    }

    [RelayCommand]
    public async Task AprobarReserva(ReservaDTO reserva)
    {
        await CambiarEstadoReservaAsync(reserva, "Aprobada");
    }

    [RelayCommand]
    public async Task RechazarReserva(ReservaDTO reserva)
    {
        reserva.Estado = "Rechazada";
        await _reservaServiceToApi.PutReserva(reserva);
        await LoadAsync();
    }

    private async Task CambiarEstadoReservaAsync(ReservaDTO reserva, string nuevoEstado)
    {
        try
        {
            reserva.Estado = nuevoEstado;
            reserva.NombreProfesor ??= "Brisa";
            reserva.CorreoProfesor ??= "brisa@iescomercio.com";

            await _reservaServiceToApi.PutReserva(reserva);

            MessageBox.Show($"Estado actualizado a: {reserva.Estado}"); // 🔍 Añadido para confirmar
            await LoadAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al actualizar reserva: {ex.Message}");
        }
    }



    // [RelayCommand]
    // public async Task AddDicatador()
    // {
    //    var addDicatadorWindow = new AddDicatadorView();
    //
    //    var addDicatadorViewModel = App.Current.Services.GetService<AddDicatadorViewModel>();
    //   addDicatadorWindow.DataContext = addDicatadorViewModel;
    //   addDicatadorWindow.ShowDialog();       
    //    await LoadAsync();
    // }


    [RelayCommand]
    public async Task Logout()
    {
        App.Current.Services.GetService<LoginDTO>().Token = "";
        App.Current.Services.GetService<MainViewModel>().SelectedViewModel = App.Current.Services.GetService<MainViewModel>().LoginViewModel;
    }

    [RelayCommand]
    public void PreviousPage()
    {
        if (CurrentPage > 0)
        {
            CurrentPage--;
            UpdatePagedReservas();
        }
    }
    [RelayCommand]
    public async Task IrADiasNoLectivos()
    {
        await App.Current.Services.GetService<MainViewModel>()
         .SelectViewModelAsync(
             App.Current.Services.GetService<MainViewModel>().DiaNoLectivoViewModel
         );
    }
    [RelayCommand]
    public async Task IrAFranjasHorarias()
    {
        await App.Current.Services.GetService<MainViewModel>()
             .SelectViewModelAsync(
                 App.Current.Services.GetService<MainViewModel>().FranjaHorariaViewModel
             );
    }


    [RelayCommand]
    public void NextPage()
    {
        if (CurrentPage < TotalPages - 1)
        {
            CurrentPage++;
            UpdatePagedReservas();
        }
    }
}
  //  public async void  MyDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
  //  {
  //      if (e.Row.Item is FranjaHorarioDTO dicatadorDTO)
   //     {
    //       await _dicatadorServiceToApi.PutDicatador(dicatadorDTO);
   //     }
   // }
  // private bool CanGoToPreviousPage() => CurrentPage > 0;

   // private bool CanGoToNextPage() => CurrentPage < TotalPages - 1;
//}

