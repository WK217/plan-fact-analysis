using System.Collections.Generic;
using System.Linq;

namespace PlanFactAnalysis.ViewModel.PlanFact
{
    [Magic]
    internal sealed class BudgetItemComparisonViewModel : ViewModelBase
    {
        readonly PlanFactTableViewModel _planFactTable;
        readonly BudgetItemViewModel _budgetItem;

        public BudgetItemComparisonViewModel (PlanFactTableViewModel planFactTable, BudgetItemViewModel budgetItem)
        {
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
                    var actualValues = from operation in ActualOperationViewModel.Collection
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
            return from operation in PlannedOperationViewModel.Collection
                   where operation.BudgetItem == _budgetItem && (operation.BeginDate >= _planFactTable.BeginDate && operation.EndDate <= _planFactTable.EndDate)
                   orderby operation.Name
                   select operation;
        }
    }
}