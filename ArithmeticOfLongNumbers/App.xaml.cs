using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using ArithmeticOfLongNumbers.ViewModel;

namespace ArithmeticOfLongNumbers
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var dataContext = new MainViewModel();
            var view = new View() { DataContext = dataContext };
            view.ShowDialog();
        }

    }
}
