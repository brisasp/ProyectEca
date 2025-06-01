using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LoginRegister.Interface;
using LoginRegister.Models;
using LoginRegister.View;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows;

namespace LoginRegister.ViewModel
{
    public partial class AddDiaNoLectivoViewModel : ViewModelBase
    {
        [ObservableProperty]
        private DateTime _fecha = DateTime.Today;

        [ObservableProperty]
        private string _motivo = string.Empty;


        private readonly IDiaNoLectivoServiceToApi _diaNoLectivoServiceToApi;
        public AddDiaNoLectivoViewModel(IDiaNoLectivoServiceToApi diaNoLectivoServiceToApi)
        {
            _diaNoLectivoServiceToApi = diaNoLectivoServiceToApi;


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
        public async Task Add()
        {
            if (string.IsNullOrEmpty(Motivo))
            {
                MessageBox.Show("Por favor, rellene todos los campos.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DiaNoLectivoDTO dto = new()
            {
                Fecha = Fecha,
                Motivo = Motivo,

            };

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
                MessageBox.Show($"Ocurrió un error durante el registro: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}


