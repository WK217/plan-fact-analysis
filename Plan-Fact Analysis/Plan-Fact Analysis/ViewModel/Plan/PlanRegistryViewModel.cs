using PlanFactAnalysis.Model;
using System.Collections.Generic;
using System.Linq;

namespace PlanFactAnalysis.ViewModel
{
    [Magic]
    internal sealed class PlanRegistryViewModel : RegistryViewModel<PlannedOperationViewModel, PlannedOperation>
    {
        public PlanRegistryViewModel (MainViewModel context, IList<PlannedOperation> modelItems)
            : base(context, modelItems)
        {
            SelectedScenario = _context.Scenarios.GetViewModelFromModel (Scenario.Default);
        }

        ScenarioViewModel _selectedScenario;
        public ScenarioViewModel SelectedScenario
        {
            get => _selectedScenario;
            set
            {
                _selectedScenario = value;
                foreach (PlannedOperationViewModel item in Items)
                {
                    PlannedOperationScenarioViewModel newSelectedObject = item.ScenariosRegistryCollectionView.Cast<PlannedOperationScenarioViewModel> ( ).FirstOrDefault (sc => sc.ScenarioObject == value);

                    if (newSelectedObject != null)
                        item.ScenariosRegistryCollectionView.MoveCurrentTo (newSelectedObject);
                }
            }
        }
    }
}