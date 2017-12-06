namespace PlanFactAnalysis.ViewModel
{
    internal abstract class SubViewModelBase<T> : PropertyChangedBase
    {
        protected readonly T _context;

        public SubViewModelBase (T context)
        {
            _context = context;
        }
    }
}
