﻿using LoginRegister.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LoginRegister.View
{
    /// <summary>
    /// Lógica de interacción para AddDicatadorView.xaml
    /// </summary>
    public partial class AddFranjaHoraria : Window
    {
        public AddFranjaHoraria()
        {
            InitializeComponent();
         this.DataContext = App.Current.Services.GetService<AddFranjaHorariaViewModel>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
