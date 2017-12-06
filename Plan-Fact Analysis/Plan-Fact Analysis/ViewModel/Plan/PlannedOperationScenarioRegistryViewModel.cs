using PlanFactAnalysis.Model;
using System.Collections.Generic;
using System.Linq;

namespace PlanFactAnalysis.ViewModel
{
    [Magic]
    internal sealed class PlannedOperationScenarioRegistryViewModel : RegistryViewModel<PlannedOperationScenarioViewModel, PlannedOperationScenario>
    {
        readonly PlannedOperationViewModel _plannedOperation;

        public PlannedOperationScenarioRegistryViewModel (PlannedOperationViewModel plannedOperation, MainViewModel context, IList<PlannedOperationScenario> modelItems)
            : base (context, modelItems)
        {
            _plannedOperation = plannedOperation;
        }

        protected override bool CanAddItem (object obj)
        {
            var usedScenarios = from sc in Items
                                select sc.ScenarioObject;

            return !usedScenarios.Contains (NewItem.ScenarioObject);
        }

        protected override bool CanRemoveItem (object obj)
        {
            var usedScenarios = from sc in Items
                                select sc.ScenarioObject;

            return usedScenarios.Contains (_plannedOperation.SelectedScenarioObject);
        }
    }
}