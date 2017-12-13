using PlanFactAnalysis.Model;
using System.Linq;

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
            set
            {
                var plannedOperations = from op in _context.PlanRegistry.Items
                                        where op.BudgetItem.MeasurementUnit == MeasurementUnit
                                        select op;

                var actualOperations = from op in _context.FactRegistry.Items
                                       where op.BudgetItem.MeasurementUnit == MeasurementUnit
                                       select op;

                _model.MeasurementUnit = _context.MeasurementUnits.GetModelFromViewModel (value);

                foreach (var plannedOperation in plannedOperations)
                    plannedOperation.UpdateAllProperties ( );

                foreach (var actualOperation in actualOperations)
                    actualOperation.UpdateAllProperties ( );

                UpdateAllProperties ( );
            }
        }

        public override string ToString ( ) => string.Format (@"{0} ({1})", Name, MeasurementUnit.Designation);
        public string FullName => ToString ( );
    }
}