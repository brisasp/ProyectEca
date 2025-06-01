using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LoginRegister.Interface;
using LoginRegister.Models;
using LoginRegister.Service;
using LoginRegister.View;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace LoginRegister.ViewModel;

public partial class FranjaHorariaViewModel : ViewModelBase
{
    private readonly IFranjaHorarioServiceToApi _franjaService;
    private List<FranjaHorarioDTO> _todasLasFranjas;

    public FranjaHorariaViewModel(IFranjaHorarioServiceToApi franjaService)
    {
        _franjaService = franjaService;
        _todasLasFranjas = new List<FranjaHorarioDTO>();
        PagedFranjas = new ObservableCollection<FranjaHorarioDTO>();
        ItemsPerPage = 8;
        CurrentPage = 0;
    }

    [ObservableProperty]
    private ObservableCollection<FranjaHorarioDTO> pagedFranjas;

    [ObservableProperty]
    private FranjaHorarioDTO? selectedFranja;

    [ObservableProperty]
    private int currentPage;

    [ObservableProperty]
    private int itemsPerPage;

    public int TotalPages => (int)Math.Ceiling((double)_todasLasFranjas.Count / ItemsPerPage);


    public override async Task LoadAsync()
    {
        try
        {

            _todasLasFranjas.Clear();
            PagedFranjas.Clear();


            var franjas = await _franjaService.GetFranjas();
            _todasLasFranjas.AddRange(franjas.OrderBy(f => f.HoraInicio));


            CurrentPage = 0;
            UpdatePagedFranjas();
        }
        catch (Exception ex)
        {

            Console.WriteLine($"Error al cargar datos: {ex.Message}");
        }
    }


    private void UpdatePagedFranjas()
    {

        PagedFranjas.Clear();

        var pagedItems = _todasLasFranjas.Skip(CurrentPage * ItemsPerPage).Take(ItemsPerPage).ToList();
        foreach (var item in pagedItems)
        {
            PagedFranjas.Add(item);
        }
    }

    //[RelayCommand]
   // public async Task AddFranja()
   // {
      //  var ventana = new AddFranjaHoraria();

      //  var vm = App.Current.Services.GetService<AddFranjaHorariaViewModel>();
       // ventana.DataContext = vm;

      //  ventana.ShowDialog(); // o Show() si no quieres que sea modal

      //  await LoadAsync(); // recarga la tabla después de cerrarse
  //  }

    [RelayCommand]
    public async Task AddFranja()
    {
        var ventana = new AddFranjaHoraria(); // esto carga el XAML y asigna el ViewModel
        ventana.ShowDialog();
    }


    [RelayCommand]
    public async Task DeleteFranja()
    {
        if (SelectedFranja == null)
        {
            MessageBox.Show("Selecciona una franja para eliminar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var confirm = MessageBox.Show(
            $"¿Estás segura de que quieres eliminar la franja {SelectedFranja.HoraInicio} - {SelectedFranja.HoraFin}?",
            "Confirmar eliminación",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question
        );

        if (confirm == MessageBoxResult.Yes)
        {
            try
            {
                await _franjaService.DeleteFranja(SelectedFranja.ID); // Ajusta si el campo se llama distinto
                await LoadAsync(); // Recarga la lista tras borrar
                MessageBox.Show("Franja eliminada correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar la franja: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }


    [RelayCommand]
    public async Task VolverDashboard()
    {
        await App.Current.Services.GetService<MainViewModel>()
            .SetAndLoadViewModelAsync(
                App.Current.Services.GetService<MainViewModel>().DashboardViewModel
            );
    }

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
            UpdatePagedFranjas();
        }
    }

    [RelayCommand]
    public async Task EditarFranja()
    {
        foreach (var franja in PagedFranjas)
        {
            try
            {
                await _franjaService.PutFranja(franja);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar franja {franja.ID}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        MessageBox.Show("Cambios guardados correctamente.");
    }



    [RelayCommand]
    public void NextPage()
    {
        if (CurrentPage < TotalPages - 1)
        {
            CurrentPage++;
            UpdatePagedFranjas();
        }
    }
}


 

