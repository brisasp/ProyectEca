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

public partial class EditarFranjaViewModel : ObservableObject
{
    private readonly IFranjaHorarioServiceToApi _franjaService;
    [ObservableProperty] private string horaInicio;
    [ObservableProperty] private string horaFin;
    private readonly FranjaHorarioDTO _franja;

    public EditarFranjaViewModel(FranjaHorarioDTO franja, IFranjaHorarioServiceToApi service)
    {
        _franja = franja;
        _franjaService = service;
        horaInicio = franja.HoraInicio;
        horaFin = franja.HoraFin;
    }

    [RelayCommand]
    public async Task GuardarCambios()
    {
        _franja.HoraInicio = HoraInicio;
        _franja.HoraFin = HoraFin;

        await _franjaService.PutFranja(_franja); // método ya existente
    }
}