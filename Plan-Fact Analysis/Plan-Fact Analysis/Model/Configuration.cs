using System.Collections.Generic;

namespace PlanFactAnalysis.Model
{
    public struct Configuration
    {
        readonly IList<User> _users;
        readonly IList<MeasurementUnit> _measurementUnits;
        readonly IList<BudgetItem> _budgetItems;
        readonly IList<ResponsibilityCenter> _responsibilityCenters;
        readonly IList<Scenario> _scenarios;
        readonly IList<PlannedOperation> _plannedOperations;
        readonly IList<ActualOperation> _actualOperations;

        public IList<User> Users => _users;
        public IList<MeasurementUnit> MeasurementUnits => _measurementUnits;
        public IList<BudgetItem> BudgetItems => _budgetItems;
        public IList<ResponsibilityCenter> ResponsibilityCenters => _responsibilityCenters;
        public IList<Scenario> Scenarios => _scenarios;
        public IList<PlannedOperation> PlannedOperations => _plannedOperations;
        public IList<ActualOperation> ActualOperations => _actualOperations;

        public Configuration (IList<User> users, IList<MeasurementUnit> measurementUnits, IList<BudgetItem> budgetItems, IList<ResponsibilityCenter> responsibilityCenters,
            IList<Scenario> scenarios, IList<PlannedOperation> plannedOperations, IList<ActualOperation> actualOperations)
        {
            _users = users;
            _measurementUnits = measurementUnits;
            _budgetItems = budgetItems;
            _responsibilityCenters = responsibilityCenters;
            _scenarios = scenarios;
            _plannedOperations = plannedOperations;
            _actualOperations = actualOperations;
        }
    }
}