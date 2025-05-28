using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LoginRegister.Interface;
using LoginRegister.Models;
using LoginRegister.Service;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace LoginRegister.ViewModel
{
    public partial class AddFranjaHorariaViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string _horaInicio;

        [ObservableProperty]
        private string _horaFin;

        [ObservableProperty]
        private bool _activa = true;

        private readonly IFranjaHorarioServiceToApi _franjaHorariaServiceToApi;
        public AddFranjaHorariaViewModel(IFranjaHorarioServiceToApi franjaHorariaServiceToApi)
        {
            _franjaHorariaServiceToApi = franjaHorariaServiceToApi;
        }

        [RelayCommand]
        public async Task Add()
        {
            if (string.IsNullOrEmpty(HoraInicio) ||
                string.IsNullOrEmpty(HoraFin))

            {
                MessageBox.Show("Por favor, rellene todos los campos.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            FranjaHorarioDTO franjaHorarioDTO = new()
            {
                HoraInicio = HoraInicio,
                HoraFin = HoraFin,
                Activa = Activa
            };

            try
            {
                await _franjaHorariaServiceToApi.PostFranja(franjaHorarioDTO);

                MessageBox.Show("Franja Horaria añadida con exito");
                // Cerrar ventana
                foreach (Window window in Application.Current.Windows)
                {
                    if (window is View.AddFranjaHoraria)
                    {
                        window.Close();
                        break;
                    }
                }

                // Recargar vista principal
                var mainVM = App.Current.Services.GetService<MainViewModel>();
                mainVM.SelectedViewModel = App.Current.Services.GetService<FranjaHorariaViewModel>();
                await mainVM.SelectedViewModel.LoadAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error durante el registro: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}


