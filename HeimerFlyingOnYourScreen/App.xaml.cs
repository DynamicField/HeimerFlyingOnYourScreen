using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using HeimerFlyingOnYourScreen.Properties;

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
            Settings.Default.Upgrade();
            if (!ResourceData.ImplementedSkins.Contains(Settings.Default.Skin))
            {
                Settings.Default.Skin = "Classic";
                Settings.Default.Save();
            }
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var w = new UnhandledExceptionWindow(e.ExceptionObject as Exception);
            w.ShowDialog();
            Environment.Exit(1);
        }
    }
}
