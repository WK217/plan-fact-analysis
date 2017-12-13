using System.Collections.Generic;
using System.Linq;

namespace PlanFactAnalysis.ViewModel
{
    [Magic]
    internal sealed class BudgetItemComparisonViewModel : PropertyChangedBase
    {
        readonly MainViewModel _context; 

        readonly PlanFactTableViewModel _planFactTable;
        readonly BudgetItemViewModel _budgetItem;

        public BudgetItemComparisonViewModel (PlanFactTableViewModel planFactTable, BudgetItemViewModel budgetItem, MainViewModel context)
        {
            _context = context;

            _planFactTable = planFactTable;
            _budgetItem = budgetItem;

            UpdateAllProperties ( );
        }

        public BudgetItemViewModel BudgetItem => _budgetItem;

        public double PlannedValue => GetPlannedOperations ( ).Sum (op => op.Value);
        public double ActualValue
        {
            get
            {
                double sum = 0.0;

                foreach (var plannedOperation in GetPlannedOperations())
                {
                    var actualValues = from operation in _context.FactRegistry.Items
                                       where operation.PlannedOperation == plannedOperation && (operation.Date >= plannedOperation.BeginDate && operation.Date <= plannedOperation.EndDate)
                                       orderby operation.Name
                                       select operation.Value;

                    sum += actualValues.Sum ( );
                }

                return sum;
            }
        }

        public double Deviation => ActualValue - PlannedValue;

        public IEnumerable<PlannedOperationViewModel> GetPlannedOperations ( )
        {
            return from operation in _context.PlanRegistry.Items
                   where operation.BudgetItem == _budgetItem && (operation.BeginDate >= _planFactTable.BeginDate && operation.EndDate <= _planFactTable.EndDate)
                   orderby operation.Name
                   select operation;
        }
    }
}