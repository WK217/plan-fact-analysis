using PlanFactAnalysis.Model;
using System;

namespace PlanFactAnalysis.ViewModel
{
    [Magic]
    internal sealed class ActualOperationViewModel : ViewModelBase<ActualOperation>
    {
        public ActualOperationViewModel (ActualOperation model, MainViewModel context)
            : base (model, context)
        {
            
        }

        public ActualOperationViewModel (MainViewModel context)
            : base (context)
        {

        }

        public string Name { get => _model.Name; set => _model.Name = value; }
        public DateTime Date { get => _model.Date; set => _model.Date = value; }

        public double Value { get => _model.Value; set => _model.Value = value; }
        public double LabourIntensity { get => _model.LabourIntensity; set => _model.LabourIntensity = value; }
        
        public BudgetItemViewModel BudgetItem
        {
            get => _context.BudgetItems.GetViewModelFromModel (_model.BudgetItem);
            set => _model.BudgetItem = _context.BudgetItems.GetModelFromViewModel (value);
        }
        
        public ResponsibilityCenterViewModel ResponsibilityCenter
        {
            get => _context.ResponsibilityCenters.GetViewModelFromModel (_model.ResponsibilityCenter);
            set => _model.ResponsibilityCenter = _context.ResponsibilityCenters.GetModelFromViewModel (value);
        }
        
        public PlannedOperationViewModel PlannedOperation
        {
            get => _context.PlanRegistry.GetViewModelFromModel (_model.PlannedOperation);
            set
            {
                _model.PlannedOperation = _context.PlanRegistry.GetModelFromViewModel (value);
                RaisePropertyChanged (nameof (BudgetItem));
                RaisePropertyChanged (nameof (ResponsibilityCenter));
            }
        }

        public override string ToString ( )
        {
            return Name;
        }
    }
}