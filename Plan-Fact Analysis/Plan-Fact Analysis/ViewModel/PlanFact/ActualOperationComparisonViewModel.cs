namespace PlanFactAnalysis.ViewModel
{
    [Magic]
    internal sealed class ActualOperationComparisonViewModel : PropertyChangedBase
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