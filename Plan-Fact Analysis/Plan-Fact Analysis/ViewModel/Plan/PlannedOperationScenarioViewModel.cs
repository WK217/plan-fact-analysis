using System.Linq;
using PlanFactAnalysis.Model;

namespace PlanFactAnalysis.ViewModel
{
    [Magic]
    internal sealed class PlannedOperationScenarioViewModel: ViewModelBase<PlannedOperationScenario>
    {
        public PlannedOperationScenarioViewModel (MainViewModel context)
            : base (context)
        {

        }

        public PlannedOperationScenarioViewModel (PlannedOperationScenario model, MainViewModel context)
            : base (model, context)
        {
            ScenarioObject = context.Scenarios.GetViewModelFromModel(model.Scenario);
        }
        
        public ScenarioViewModel ScenarioObject
        {
            get => _context.Scenarios.GetViewModelFromModel (_model.Scenario);
            set => _model.Scenario = _context.Scenarios.GetModelFromViewModel (value);
        }

        public double Value
        {
            get => _model.Value;
            set
            {
                _model.Value = value;

                var plannedOperation = (from op in _context.PlanRegistry.Items
                                        where (op as PlannedOperationViewModel).SelectedPlannedOperationScenario == this
                                        select op).FirstOrDefault ( );

                plannedOperation.UpdateAllProperties ( );
            }
        }
        public double LabourIntensity
        {
            get => _model.LabourIntensity;
            set
            {
                _model.LabourIntensity = value;

                var plannedOperation = (from op in _context.PlanRegistry.Items
                                        where (op as PlannedOperationViewModel).SelectedPlannedOperationScenario == this
                                        select op).FirstOrDefault ( );

                plannedOperation.UpdateAllProperties ( );
            }
        }

        public override string ToString ( )
        {
            return _model.ToString ( );
        }
    }
}