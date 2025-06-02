using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;


namespace LoginRegister.ViewModel;

public partial class MainViewModel : ViewModelBase 
{
       private ViewModelBase? _selectedViewModel;

        public MainViewModel(DashboardViewModel dashboardViewModel, LoginViewModel loginViewModel, AddFranjaHorariaViewModel addFranjaHorariaViewModel, AddDiaNoLectivoViewModel  addDiaNoLectivoViewModel, DiaNoLectivoViewModel diaNoLectivoViewModel, FranjaHorariaViewModel franjaHorariaViewModel)
        {
            DashboardViewModel = dashboardViewModel;
            LoginViewModel = loginViewModel;
            AddFranjaHorariaViewModel = addFranjaHorariaViewModel;
            AddDiaNoLectivoViewModel = addDiaNoLectivoViewModel;
            FranjaHorariaViewModel = franjaHorariaViewModel;

            DiaNoLectivoViewModel = diaNoLectivoViewModel;
        _selectedViewModel = loginViewModel;
    }

        public ViewModelBase? SelectedViewModel
        {
            get => _selectedViewModel;
            set => SetProperty(ref _selectedViewModel, value);
        }

        public DashboardViewModel DashboardViewModel { get; }
        public LoginViewModel LoginViewModel { get; }
        public DiaNoLectivoViewModel DiaNoLectivoViewModel { get; }
        public FranjaHorariaViewModel FranjaHorariaViewModel { get; }

    public AddFranjaHorariaViewModel AddFranjaHorariaViewModel { get; }

        public AddDiaNoLectivoViewModel AddDiaNoLectivoViewModel { get; }
    public override async Task LoadAsync()
        {
            if (SelectedViewModel is not null)
            {
                await SelectedViewModel.LoadAsync();
            }
        }


    [RelayCommand]
     public async Task SelectViewModelAsync(object? parameter)
     {
    if (parameter is ViewModelBase viewModel)
     {
      SelectedViewModel = viewModel;
       await LoadAsync(); // Esto es lo que lanza LoadAsync()
      }
    }


    public async Task SetAndLoadViewModelAsync(ViewModelBase viewModel)
    {
        SelectedViewModel = viewModel;
        await viewModel.LoadAsync()
     ;

    }

}
