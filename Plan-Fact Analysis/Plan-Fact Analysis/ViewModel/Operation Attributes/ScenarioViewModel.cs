using PlanFactAnalysis.Model;

namespace PlanFactAnalysis.ViewModel
{
    [Magic]
    internal sealed class ScenarioViewModel : ViewModelBase<Scenario>, IOperationAttributeViewModel
    {
        public ScenarioViewModel (MainViewModel context)
            : base (context)
        {

        }

        public ScenarioViewModel (Scenario model, MainViewModel context)
            : base (model, context)
        {

        }

        public bool IsDefault => Scenario.Default == _context.Scenarios.GetModelFromViewModel (this);

        public string Name { get => _model.Name; set => _model.Name = value; }

        public override string ToString ( ) => Name;
    }
}