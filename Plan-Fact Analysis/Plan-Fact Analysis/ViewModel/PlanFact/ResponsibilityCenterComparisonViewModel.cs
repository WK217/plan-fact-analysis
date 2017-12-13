namespace PlanFactAnalysis.ViewModel
{
    internal class ResponsibilityCenterComparisonViewModel
    {
        private PlanFactTableViewModel planFactTableViewModel;
        private ResponsibilityCenterViewModel responsibilityCenter;
        private MainViewModel _context;

        public ResponsibilityCenterComparisonViewModel (PlanFactTableViewModel planFactTableViewModel, ResponsibilityCenterViewModel responsibilityCenter, MainViewModel context)
        {
            this.planFactTableViewModel = planFactTableViewModel;
            this.responsibilityCenter = responsibilityCenter;
            _context = context;
        }
    }
}