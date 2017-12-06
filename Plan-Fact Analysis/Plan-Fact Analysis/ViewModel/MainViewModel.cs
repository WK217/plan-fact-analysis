using Microsoft.Win32;
using PlanFactAnalysis.Model;

namespace PlanFactAnalysis.ViewModel
{
    [Magic]
    internal sealed class MainViewModel : PropertyChangedBase
    {
        readonly DataManager _dataManager;

        readonly AuthorizationViewModel _authorization;
        public AuthorizationViewModel Authorization => _authorization;

        public PlanRegistryViewModel PlanRegistry { get; set; }
        public RegistryViewModel<ActualOperationViewModel, ActualOperation> FactRegistry { get; set; }
        public PlanFactTableViewModel PlanFactTable { get; set; }

        public OperationAttributeRegistryViewModel<BudgetItemViewModel, BudgetItem> BudgetItems { get; set; }
        public OperationAttributeRegistryViewModel<MeasurementUnitViewModel, MeasurementUnit> MeasurementUnits { get; set; }
        public OperationAttributeRegistryViewModel<ResponsibilityCenterViewModel, ResponsibilityCenter> ResponsibilityCenters { get; set; }
        public OperationAttributeRegistryViewModel<ScenarioViewModel, Scenario> Scenarios { get; set; }

        public MainViewModel (DataManager dataManager, Configuration configuration)
        {
            _dataManager = dataManager;

            _authorization = new AuthorizationViewModel (configuration.Users);

            BudgetItems = new OperationAttributeRegistryViewModel<BudgetItemViewModel, BudgetItem> (this, configuration.BudgetItems);
            MeasurementUnits = new OperationAttributeRegistryViewModel<MeasurementUnitViewModel, MeasurementUnit> (this, configuration.MeasurementUnits);
            ResponsibilityCenters = new OperationAttributeRegistryViewModel<ResponsibilityCenterViewModel, ResponsibilityCenter> (this, configuration.ResponsibilityCenters);
            Scenarios = new OperationAttributeRegistryViewModel<ScenarioViewModel, Scenario> (this, configuration.Scenarios);

            PlanRegistry = new PlanRegistryViewModel (this, configuration.PlannedOperations);
            FactRegistry = new RegistryViewModel<ActualOperationViewModel, ActualOperation> (this, configuration.ActualOperations);
            PlanFactTable = new PlanFactTableViewModel (PlanRegistry, FactRegistry, this);

            _exportDatabaseCommand = new RelayCommand (param =>
            {
                OpenFileDialog openDataBaseDialog = new OpenFileDialog
                {
                    Filter = "Базы данных программы «План/факт-анализ» (*.pfa))|*.pfa|Все файлы (*.*)|*.*",
                    CheckFileExists = true,
                };

                if (openDataBaseDialog.ShowDialog ( ) == true)
                {
                    dataManager.EstablishDBConnection (openDataBaseDialog.FileName);
                    dataManager.ExportConfiguration ( );
                }
            });
        }

        readonly RelayCommand _exportDatabaseCommand;
        public RelayCommand ExportDatabaseCommand => _exportDatabaseCommand;
    }
}
