namespace PlanFactAnalysis.ViewModel
{
    [Magic]
    public sealed class MainViewModel: ViewModelBase
    {
        public PlanRegistryViewModel PlanRegistry { get; set; }
        public FactRegistryViewModel FactRegistry { get; set; }
        public PlanFactTableViewModel PlanFactTable { get; set; }
    }
}
