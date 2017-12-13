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

        protected override bool CanRemoveItem (object obj)
        {
            PlannedOperationViewModel currentItem = ItemsCollectionView.CurrentItem as PlannedOperationViewModel;

            var actualOperations = from op in _context.FactRegistry.Items
                                   where op.PlannedOperation == currentItem
                                   select op;

            return actualOperations.Count ( ) == 0;
        }

        public bool IsEnabled
        {
            get => _context.Authorization.Role == UserRole.Planner || _context.Authorization.Role == UserRole.Manager;
        }
    }
}