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

        public double Value { get => _model.Value; set => _model.Value = value; }
        public double LabourIntensity { get => _model.LabourIntensity; set => _model.LabourIntensity = value; }

        public override string ToString ( )
        {
            return _model.ToString ( );
        }
    }
}