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

            Model.DataManager model;
            //e.Args[0] — путь к открываемому файлу базы данных.
            if (e.Args.Length > 0)
                model = new Model.DataManager (e.Args[0]);
            else
                model = new Model.DataManager ( );

            ViewModel.MainViewModel mainViewModel = new ViewModel.MainViewModel (model);

            View.MainView mainView = new View.MainView ( )
            {
                DataContext = mainViewModel
            };

            mainView.Show ( );

            //try
            //{
            //    OpenFileDialog openDataBaseDialog = new OpenFileDialog
            //    {
            //        Filter = "Базы данных программы «План/факт-анализ» (*.pfa))|*.pfa|Все файлы (*.*)|*.*",
            //        CheckFileExists = true,
            //    };

            //    if (openDataBaseDialog.ShowDialog ( ) == true)
            //        model.EstablishDBConnection (openDataBaseDialog.FileName);

            //    mainViewModel = new ViewModel.MainViewModel (model, model.GetConfiguration ( ));
            //}
            //catch (System.Exception)
            //{
            //    mainViewModel = new ViewModel.MainViewModel (model, model.GetDefaultConfiguration ( ));
            //}
        }
    }
}
