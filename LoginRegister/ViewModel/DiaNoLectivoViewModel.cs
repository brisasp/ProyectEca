using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LoginRegister.Interface;
using LoginRegister.Models;
using LoginRegister.View;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace LoginRegister.ViewModel;

public partial class DiaNoLectivoViewModel : ViewModelBase
{
    private readonly IDiaNoLectivoServiceToApi _diaNoLectivoService;
    private readonly AddDiaNoLectivoViewModel _addViewModel;
    private readonly IHttpJsonProvider<DiaNoLectivoDTO> _httpJsonProvider;

    public DiaNoLectivoViewModel(IDiaNoLectivoServiceToApi diaNoLectivoService,AddDiaNoLectivoViewModel addViewModel,  IHttpJsonProvider<DiaNoLectivoDTO> httpJsonProvider)
    {
        //MessageBox.Show("Constructor DiaNoLectivoViewModel");   
        _diaNoLectivoService = diaNoLectivoService;
        _addViewModel = addViewModel;
        
       _dias = new List<DiaNoLectivoDTO>();
        PagedDiasNoLectivos = new ObservableCollection<DiaNoLectivoDTO>();
      
        _httpJsonProvider = httpJsonProvider;

        ItemsPerPage =8;
        CurrentPage = 0;

     }

    private List<DiaNoLectivoDTO> _dias;

    private DiaNoLectivoDTO _selectedDiaNoLectivo;

    [ObservableProperty]
    private ObservableCollection<DiaNoLectivoDTO> pagedDiasNoLectivos;

    [ObservableProperty]
    private int currentPage;

    [ObservableProperty]
    private int itemsPerPage;

    public int TotalPages => (int)Math.Ceiling((double)_dias.Count / ItemsPerPage);

    //hacer for porque no esta rellenando dias 
    public override async Task LoadAsync()
    {
        try
        {
            _dias.Clear();
            PagedDiasNoLectivos.Clear();

            //var lista = await _diaNoLectivoService.GetDiasNoLectivos();

            //for diasnolectivos recorrer la lista de lista y la añado en lista  por items

            //MessageBox.Show($"Recibidos: {lista.Count()} días"); // 🧠 VERIFICACIÓN 1

            //Dias.AddRange(lista.OrderBy(d => d.ID));


            IEnumerable<DiaNoLectivoDTO> listaDicatadores = await _diaNoLectivoService.GetDiasNoLectivos();
            _dias.AddRange(listaDicatadores.OrderBy(d => d.ID));
            CurrentPage = 0;
            UpdatePagedDias();

           // MessageBox.Show($"Mostrando: {PagedDiasNoLectivos.Count} en la tabla"); // 🧠 VERIFICACIÓN 2
        }
        catch (Exception ex)
        {

            Console.WriteLine($"Error al cargar datos: {ex.Message}");
        }
    }



    private void UpdatePagedDias()
    {
        PagedDiasNoLectivos.Clear();
        var pagedItems = _dias.Skip(CurrentPage * ItemsPerPage).Take(ItemsPerPage).ToList();
            
        foreach (var item in pagedItems)
        {
            PagedDiasNoLectivos.Add(item);
        }
    }



    [RelayCommand]
    public async Task DeleteDiaNoLectivo()
    {
        if (_selectedDiaNoLectivo is null)
        {
            MessageBox.Show("Seleccione un día no lectivo para eliminar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            await _diaNoLectivoService.DeleteDiaNoLectivo(_selectedDiaNoLectivo.ID);
            await LoadAsync();
            MessageBox.Show("Día no lectivo eliminado correctamente.");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al eliminar el día: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
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
            UpdatePagedDias();
        }
    }

    [RelayCommand]
    public async Task AgregarDiaNoLectivo()
    {
        await App.Current.Services.GetService<MainViewModel>()
         .SetAndLoadViewModelAsync(
             App.Current.Services.GetService<MainViewModel>().AddDiaNoLectivoViewModel
         );
    }

    public DiaNoLectivoDTO SelectedDiaNoLectivo
    {
        get { return _selectedDiaNoLectivo; }
        set
        {
            _selectedDiaNoLectivo = value;
            OnPropertyChanged(nameof(SelectedDiaNoLectivo));
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
    public void NextPage()
    {
        if (CurrentPage < TotalPages - 1)
        {
            CurrentPage++;
            UpdatePagedDias();
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

