using PlanFactAnalysis.Model;

namespace PlanFactAnalysis.ViewModel
{
    [Magic]
    internal sealed class BudgetItemViewModel : ViewModelBase<BudgetItem>, IOperationAttributeViewModel
    {
        public BudgetItemViewModel (MainViewModel context)
            : base (context)
        {

        }

        public BudgetItemViewModel (BudgetItem model, MainViewModel context)
            : base (model, context)
        {

        }

        public bool IsDefault => BudgetItem.Default == _context.BudgetItems.GetModelFromViewModel (this);

        public string Name { get => _model.Name; set => _model.Name = value; }

        public MeasurementUnitViewModel MeasurementUnit
        {
            get => _context.MeasurementUnits.GetViewModelFromModel (_model.MeasurementUnit);
            set => _model.MeasurementUnit = _context.MeasurementUnits.GetModelFromViewModel (value);
        }

        public override string ToString ( ) => string.Format (@"{0} ({1})", Name, MeasurementUnit.Designation);
        public string FullName => ToString ( );
    }
}