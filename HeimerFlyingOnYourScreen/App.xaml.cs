using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HeimerFlyingOnYourScreen
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var w = new UnhandledExceptionWindow(e.ExceptionObject as Exception);
            w.ShowDialog();
            Environment.Exit(1);
        }
    }
}
