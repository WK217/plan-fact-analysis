namespace PlanFactAnalysis.ViewModel
{
    [Magic]
    internal sealed class ComparisonViewModel : PropertyChangedBase
    {
        public ComparisonViewModel (int level)
        {
            Level = level;
        }
        
        public string BudgetItem { get; set; }
        public string ResponsibilityCenter { get; set; }
        public string PlannedOperation { get; set; }
        public string ActualOperation { get; set; }
        public string PlannedValue { get; set; }
        public string ActualValue { get; set; }
        public string AbsoluteDeviation { get; set; }
        public string RelativeDeviation { get; set; }
        public int Level { get; private set; }
    }
}
