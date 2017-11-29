using PlanFactAnalysis.Model;
using System.Linq;

namespace PlanFactAnalysis.ViewModel
{
    [Magic]
    internal sealed class PlannedOperationScenarioViewModel: ViewModelBase
    {
        public PlannedOperationScenarioViewModel (PlannedOperationScenario model)
        {
            ScenarioObject = ScenarioViewModel.Collection.Cast<ScenarioViewModel> ( ).FirstOrDefault (sc => sc.Name == model.Scenario.Name);

            if (ScenarioObject == null)
                ScenarioObject = ScenarioViewModel.Collection.Cast<ScenarioViewModel> ( ).First ( );

            Value = model.Value;
            LabourIntensity = model.LabourIntensity;
        }

        public PlannedOperationScenarioViewModel (ScenarioViewModel scenarioObject, double value, double labourIntensity)
        {
            ScenarioObject = scenarioObject;
            Value = value;
            LabourIntensity = labourIntensity;
        }

        public ScenarioViewModel ScenarioObject { get; set; }

        public double Value { get; set; }
        public double LabourIntensity { get; set; }
    }
}