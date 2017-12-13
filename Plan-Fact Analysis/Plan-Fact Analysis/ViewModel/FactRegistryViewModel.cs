using System.Collections.Generic;
using PlanFactAnalysis.Model;

namespace PlanFactAnalysis.ViewModel
{
    [Magic]
    internal sealed class FactRegistryViewModel : RegistryViewModel<ActualOperationViewModel, ActualOperation>
    {
        public FactRegistryViewModel (MainViewModel context, IList<ActualOperation> modelItems)
            : base (context, modelItems)
        {

        }

        public bool IsEnabled
        {
            get => _context.Authorization.Role == UserRole.Executor || _context.Authorization.Role == UserRole.Manager;
        }
    }
}