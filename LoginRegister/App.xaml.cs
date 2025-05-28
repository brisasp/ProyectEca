using LoginRegister.ViewModel;
using LoginRegister.Service;
using LoginRegister.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using LoginRegister.Services;
using LoginRegister.Models;
using LoginRegister.View;


namespace LoginRegister
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //MessageBox.Show("¡App iniciando!");

            //var mainWindow = Current.Services.GetService<MainWindow>();
            //mainWindow?.Show();
            try
            {
                var mainWindow = Current.Services.GetRequiredService<MainWindow>();
                //MessageBox.Show("Instanciado OK"); // <-- esto DEBE salir
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al abrir MainWindow:\n{ex.Message}\n\n{ex.InnerException?.Message}",
                    "Excepción",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
        public new static App Current => (App)Application.Current;
        public IServiceProvider Services { get; }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            //view principal
            services.AddSingleton<MainWindow>();

            //view viewModels
            services.AddSingleton<MainViewModel>();
            services.AddTransient(provider =>
            {
                return new LoginViewModel(
                    provider.GetRequiredService<IHttpJsonProvider<UserDTO>>(),
                    vm => provider.GetRequiredService<MainViewModel>().SelectedViewModel = vm
                );
            });

            services.AddTransient<DashboardViewModel>();
            services.AddSingleton<AddFranjaHorariaViewModel>();
            services.AddSingleton<AddDiaNoLectivoViewModel>();
            services.AddSingleton<FranjaHorariaViewModel>();

            services.AddSingleton<DiaNoLectivoViewModel>();

            //Services
            services.AddSingleton<LoginDTO>();

            //services a APIS

            services.AddSingleton<IFranjaHorarioServiceToApi, FranjaHorarioServiceToApi>();

            //services.AddSingleton<IDicatadorServiceToApi, DicatadorServiceToApi>();
            services.AddSingleton<IReservaServiceToApi, ReservaServiceToApi>();
            services.AddSingleton<IFranjaHorarioServiceToApi, FranjaHorarioServiceToApi>();
            services.AddSingleton<IDiaNoLectivoServiceToApi, DiaNoLectivoServiceToApi>();
            services.AddSingleton(typeof(IHttpJsonProvider<>), typeof(HttpJsonService<>));
            return services.BuildServiceProvider();
        }
    }
}


