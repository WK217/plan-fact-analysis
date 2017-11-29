namespace PlanFactAnalysis.ViewModel.PlanFact
{
    [Magic]
    internal sealed class ActualOperationComparisonViewModel : ViewModelBase
    {
        readonly ActualOperationViewModel _operation;

        public ActualOperationComparisonViewModel (ActualOperationViewModel operation)
        {
            _operation = operation;
        }

        public ActualOperationViewModel ActualOperation => _operation;
        public ResponsibilityCenterViewModel ResponsibilityCenter => _operation.ResponsibilityCenter;
        public double ActualValue => _operation.Value;
    }
}