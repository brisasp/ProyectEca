using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LoginRegister.Interface;
using LoginRegister.Models;
using LoginRegister.View;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Windows;

namespace LoginRegister.ViewModel
{
    // ViewModel para añadir un nuevo día no lectivo
    public partial class AddDiaNoLectivoViewModel : ViewModelBase
    {
        // Fecha del día no lectivo, inicializada con la fecha actual
        [ObservableProperty]
        private DateTime _fecha = DateTime.Today;
        // Motivo o descripción del día no lectivo
        [ObservableProperty]
        private string _motivo = string.Empty;

        // Servicio para comunicación con la API que gestiona días no lectivos
        private readonly IDiaNoLectivoServiceToApi _diaNoLectivoServiceToApi;
        // Constructor que recibe el servicio por inyección de dependencias
        public AddDiaNoLectivoViewModel(IDiaNoLectivoServiceToApi diaNoLectivoServiceToApi)
        {
            _diaNoLectivoServiceToApi = diaNoLectivoServiceToApi;


        }
        // Comando para volver al Dashboard principal
        [RelayCommand]
        public async Task VolverDashboard()
        {
            await App.Current.Services.GetService<MainViewModel>()
                .SetAndLoadViewModelAsync(
                    App.Current.Services.GetService<MainViewModel>().DashboardViewModel
                );
        }
        // Comando para añadir un nuevo día no lectivo
        [RelayCommand]
        public async Task Add()
        {
            // Validar que el motivo no esté vacío
            if (string.IsNullOrEmpty(Motivo))
            {
                MessageBox.Show("Por favor, rellene todos los campos.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            // Crear DTO con la información del día no lectivo
            DiaNoLectivoDTO dto = new()
            {
                Fecha = Fecha,
                Motivo = Motivo,

            };
            // Enviar el DTO a la API para guardar el día no lectivo
            try
            {
                await _diaNoLectivoServiceToApi.PostDiaNoLectivo(dto);

                // ✅ Volver a la vista de lista y recargar datos
                var mainViewModel = App.Current.Services.GetService<MainViewModel>();
                await mainViewModel.SetAndLoadViewModelAsync(mainViewModel.DiaNoLectivoViewModel);


                MessageBox.Show("Día no lectivo añadido con éxito.");
            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error si falla la operación
                MessageBox.Show($"Ocurrió un error durante el registro: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}


