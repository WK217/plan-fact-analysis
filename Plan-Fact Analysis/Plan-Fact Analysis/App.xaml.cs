using Microsoft.Win32;
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

            ViewModel.MainViewModel mainViewModel;

            DataManager dataManager = new DataManager ( );

            try
            {
                OpenFileDialog openDataBaseDialog = new OpenFileDialog
                {
                    Filter = "Базы данных программы «План/факт-анализ» (*.pfa))|*.pfa|Все файлы (*.*)|*.*",
                    CheckFileExists = true,
                };

                if (openDataBaseDialog.ShowDialog ( ) == true)
                    dataManager.EstablishDBConnection (openDataBaseDialog.FileName);

                mainViewModel = new ViewModel.MainViewModel (dataManager, dataManager.GetConfiguration ( ));
            }
            catch (System.Exception)
            {
                mainViewModel = new ViewModel.MainViewModel (dataManager, dataManager.GetDefaultConfiguration ( ));
            }

            View.MainView mainView = new View.MainView ( )
            {
                DataContext = mainViewModel
            };

            mainView.Show ( );
        }
    }
}
