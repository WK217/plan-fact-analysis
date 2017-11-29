using PlanFactAnalysis.Model;
using PlanFactAnalysis.ViewModel.PlanFact;

namespace PlanFactAnalysis.ViewModel
{
    [Magic]
    internal sealed class MainViewModel : ViewModelBase
    {
        public PlanRegistryViewModel PlanRegistry { get; set; }
        public FactRegistryViewModel FactRegistry { get; set; }
        public PlanFactTableViewModel PlanFactTable { get; set; }

        public BudgetItemsCollectionViewModel BudgetItems { get; set; }
        public MeasurementUnitsCollectionViewModel MeasurementUnits { get; set; }
        public ResponsibilityCentersCollectionViewModel ResponsibilityCenters { get; set; }
        public ScenariosCollectionViewModel Scenarios { get; set; }

        public MainViewModel (Configuration configuration)
        {
            #region Единицы измерения
            foreach (var item in configuration.MeasurementUnits)
                new MeasurementUnitViewModel (item);
            #endregion

            #region Статьи бюджета
            foreach (var item in configuration.BudgetItems)
                new BudgetItemViewModel (item);
            #endregion

            #region ЦФО
            foreach (var item in configuration.ResponsibilityCenters)
                new ResponsibilityCenterViewModel (item);
            #endregion

            #region Сценарии
            foreach (var item in configuration.Scenarios)
                new ScenarioViewModel (item);
            #endregion

            PlanRegistry = new PlanRegistryViewModel (configuration.PlannedOperations);
            FactRegistry = new FactRegistryViewModel (configuration.ActualOperations);
            PlanFactTable = new PlanFactTableViewModel (PlanRegistry, FactRegistry);

            BudgetItems = new BudgetItemsCollectionViewModel ( );
            MeasurementUnits = new MeasurementUnitsCollectionViewModel ( );
            ResponsibilityCenters = new ResponsibilityCentersCollectionViewModel ( );
            Scenarios = new ScenariosCollectionViewModel ( );
        }
    }
}
