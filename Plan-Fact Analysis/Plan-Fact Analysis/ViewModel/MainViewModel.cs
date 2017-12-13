using Microsoft.Win32;
using PlanFactAnalysis.Model;

namespace PlanFactAnalysis.ViewModel
{
    [Magic]
    internal sealed class MainViewModel : PropertyChangedBase
    {
        readonly IModel _model;

        readonly AuthorizationViewModel _authorization;
        public AuthorizationViewModel Authorization => _authorization;

        public PlanRegistryViewModel PlanRegistry { get; set; }
        public FactRegistryViewModel FactRegistry { get; set; }
        public PlanFactTableViewModel PlanFactTable { get; set; }

        public OperationAttributeRegistryViewModel<BudgetItemViewModel, BudgetItem> BudgetItems { get; set; }
        public OperationAttributeRegistryViewModel<MeasurementUnitViewModel, MeasurementUnit> MeasurementUnits { get; set; }
        public OperationAttributeRegistryViewModel<ResponsibilityCenterViewModel, ResponsibilityCenter> ResponsibilityCenters { get; set; }
        public OperationAttributeRegistryViewModel<ScenarioViewModel, Scenario> Scenarios { get; set; }

        //public MainViewModel (DataManager dataManager, Configuration configuration)
        //{
        //    _model = dataManager;

        //    _authorization = new AuthorizationViewModel (this, configuration.Users);

        //    BudgetItems = new OperationAttributeRegistryViewModel<BudgetItemViewModel, BudgetItem> (this, configuration.BudgetItems);
        //    MeasurementUnits = new OperationAttributeRegistryViewModel<MeasurementUnitViewModel, MeasurementUnit> (this, configuration.MeasurementUnits);
        //    ResponsibilityCenters = new OperationAttributeRegistryViewModel<ResponsibilityCenterViewModel, ResponsibilityCenter> (this, configuration.ResponsibilityCenters);
        //    Scenarios = new OperationAttributeRegistryViewModel<ScenarioViewModel, Scenario> (this, configuration.Scenarios);

        //    PlanRegistry = new PlanRegistryViewModel (this, configuration.PlannedOperations);
        //    FactRegistry = new RegistryViewModel<ActualOperationViewModel, ActualOperation> (this, configuration.ActualOperations);
        //    PlanFactTable = new PlanFactTableViewModel (PlanRegistry, FactRegistry, this);

        //    _exportDatabaseCommand = new RelayCommand (param =>
        //    {
        //        OpenFileDialog openDataBaseDialog = new OpenFileDialog
        //        {
        //            Filter = "Базы данных программы «План/факт-анализ» (*.pfa))|*.pfa|Все файлы (*.*)|*.*",
        //            CheckFileExists = true,
        //        };

        //        if (openDataBaseDialog.ShowDialog ( ) == true)
        //        {
        //            dataManager.EstablishDBConnection (openDataBaseDialog.FileName);
        //            //dataManager.ExportConfiguration ( );
        //        }
        //    });
        //}

        public MainViewModel (IModel model)
        {
            _model = model;

            _authorization = new AuthorizationViewModel (this, _model.Users);

            BudgetItems = new OperationAttributeRegistryViewModel<BudgetItemViewModel, BudgetItem> (this, _model.BudgetItems);
            MeasurementUnits = new OperationAttributeRegistryViewModel<MeasurementUnitViewModel, MeasurementUnit> (this, _model.MeasurementUnits);
            ResponsibilityCenters = new OperationAttributeRegistryViewModel<ResponsibilityCenterViewModel, ResponsibilityCenter> (this, _model.ResponsibilityCenters);
            Scenarios = new OperationAttributeRegistryViewModel<ScenarioViewModel, Scenario> (this, _model.Scenarios);

            PlanRegistry = new PlanRegistryViewModel (this, _model.PlannedOperations);
            FactRegistry = new FactRegistryViewModel (this, _model.ActualOperations);
            PlanFactTable = new PlanFactTableViewModel (PlanRegistry, FactRegistry, this);

            _importDatabaseCommand = new RelayCommand (param =>
            {
                string fileName = ShowFileDialog (title: "Выбор файла для загрузки данных");

                if (!string.IsNullOrWhiteSpace (fileName))
                {
                    _model.ImportConfiguration (fileName);

                    BudgetItems.RefreshViewModelList ( );
                    MeasurementUnits.RefreshViewModelList ( );
                    ResponsibilityCenters.RefreshViewModelList ( );
                    Scenarios.RefreshViewModelList ( );

                    PlanRegistry.RefreshViewModelList ( );
                    FactRegistry.RefreshViewModelList ( );

                    _authorization.Logout ( );
                }
            });

            _exportDatabaseCommand = new RelayCommand (param =>
            {
                string fileName = ShowFileDialog (title: "Выбор файла для выгрузки данных");

                if (!string.IsNullOrWhiteSpace (fileName))
                {
                    _model.EstablishDBConnection (fileName);
                    _model.ExportConfiguration (fileName);
                }
            });

            _logoutCommand = new RelayCommand (param =>
            {
                _authorization.Logout ( );
            },
            param => _authorization.Authorized);
        }

        string ShowFileDialog (string title)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Title = title,
                Filter = "Базы данных программы «План/факт-анализ» (*.pfa))|*.pfa|Все файлы (*.*)|*.*",
                InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory
            };

            if (dialog.ShowDialog ( ) == true)
                return dialog.FileName;

            return string.Empty;
        }

        readonly RelayCommand _importDatabaseCommand;
        public RelayCommand ImportDatabaseCommand => _importDatabaseCommand;

        readonly RelayCommand _exportDatabaseCommand;
        public RelayCommand ExportDatabaseCommand => _exportDatabaseCommand;

        readonly RelayCommand _logoutCommand;
        public RelayCommand LogoutCommand => _logoutCommand;
    }
}
