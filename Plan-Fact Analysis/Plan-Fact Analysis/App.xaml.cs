using PlanFactAnalysis.Model;
using System.Windows;

namespace PlanFactAnalysis
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup (StartupEventArgs e)
        {
            base.OnStartup (e);

            View.MainView mainView = new View.MainView ( )
            {
                DataContext = new ViewModel.MainViewModel (new FileManager ( ).GetConfiguration ( ))
            };

            mainView.Show ( );
        }
    }
}
