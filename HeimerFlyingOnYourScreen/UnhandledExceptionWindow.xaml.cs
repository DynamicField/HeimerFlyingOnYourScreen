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

namespace HeimerFlyingOnYourScreen
{
    /// <summary>
    /// Logique d'interaction pour UnhandledExceptionWindow.xaml
    /// </summary>
    public partial class UnhandledExceptionWindow : Window
    {
        public UnhandledExceptionWindow(Exception ex)
        {
            InitializeComponent();
            stackTraceInnerRun.Text = ex.StackTrace;
            messageParagraph.Inlines.Add(new Run(ex.Message));
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
