using CommunityToolkit.Mvvm.Input;
using LoginRegister.Interface;
using LoginRegister.Models;
using LoginRegister.Helpers;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection.Metadata;

namespace LoginRegister.ViewModel
{
    public partial class LoginViewModel : ViewModelBase
    {
        private readonly IHttpJsonProvider<UserDTO> _httpJsonProvider;
        //private readonly IDicatadorServiceToApi _dicatadorServiceToApi;
        private readonly LoginDTO _loginDto;
        private readonly MainViewModel _mainViewModel;
        private readonly Action<ViewModelBase> _navegar;



        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _passwordView;

        public LoginViewModel(IHttpJsonProvider<UserDTO> httpJsonProvider, Action<ViewModelBase> navegar)
        {
            _httpJsonProvider = httpJsonProvider;
            _loginDto = App.Current.Services.GetService<LoginDTO>()!;
            _navegar = navegar;
            // _mainViewModel = App.Current.Services.GetService<MainViewModel>()!;

        }

        [RelayCommand]
        public async Task Login()
        {

            App.Current.Services.GetService<LoginDTO>().UserName = Name;
            App.Current.Services.GetService<LoginDTO>().Password = PasswordView;


            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(PasswordView))
            {
                MessageBox.Show("Por favor, rellene ambos campos.", "Error de Inicio de Sesión", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
               
                UserDTO user = await _httpJsonProvider.LoginPostAsync(Constants.LOGIN_PATH, App.Current.Services.GetService<LoginDTO>());

                if (user != null && user.IsSuccess)
                {
                    var mainViewModel = App.Current.Services.GetService<MainViewModel>();
                    mainViewModel.SelectedViewModel = mainViewModel.DashboardViewModel;
                    await mainViewModel.LoadAsync(); 
                     // _navegar.Invoke(App.Current.Services.GetService<DashboardViewModel>());
                    //App.Current.Services.GetService<MainViewModel>().SelectedViewModel = App.Current.Services.GetService<MainViewModel>().DashboardViewModel;
                    //App.Current.Services.GetService<MainViewModel>().LoadAsync();
                    Name = string.Empty;
                    PasswordView = String.Empty;
                 
                }
                else
                {
                  
                    MessageBox.Show("Credenciales incorrectas. Intente de nuevo.", "Error de Inicio de Sesión", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show($"Ocurrió un error durante el inicio de sesión: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}

