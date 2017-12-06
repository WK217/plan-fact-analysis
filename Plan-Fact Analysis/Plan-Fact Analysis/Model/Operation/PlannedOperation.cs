using System;
using System.Collections.Generic;

namespace PlanFactAnalysis.Model
{
    /// <summary>
    /// Запланированная операция.
    /// </summary>
    public sealed class PlannedOperation : IOperationInfo
    {
        /// <summary>
        /// Название операции.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Сценарий операции.
        /// </summary>
        public IList<PlannedOperationScenario> Scenarios { get; set; }

        /// <summary>
        /// Дата начала планирования.
        /// </summary>
        public DateTime BeginDate { get; set; }

        /// <summary>
        /// Дата окончания планирования.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Статья бюджета.
        /// </summary>
        public BudgetItem BudgetItem { get; set; }

        /// <summary>
        /// Центр финансовой ответственности (ЦФО).
        /// </summary>
        public ResponsibilityCenter ResponsibilityCenter { get; set; }

        public PlannedOperation ( )
        {
            BudgetItem = BudgetItem.Default;
            ResponsibilityCenter = ResponsibilityCenter.Default;

            Scenarios = new List<PlannedOperationScenario>
            {
                new PlannedOperationScenario ( )
            };
        }

        public PlannedOperation (string name, DateTime beginDate, DateTime endDate, BudgetItem budgetItem, ResponsibilityCenter responsibilityCenter, IEnumerable<PlannedOperationScenario> plannedOperationScenarios)
        {
            Name = name;

            BeginDate = beginDate;
            EndDate = endDate;

            BudgetItem = budgetItem;
            ResponsibilityCenter = responsibilityCenter;

            Scenarios = new List<PlannedOperationScenario> (plannedOperationScenarios);
        }

        public PlannedOperation (string name, DateTime beginDate, DateTime endDate, BudgetItem budgetItem, ResponsibilityCenter responsibilityCenter)
        {
            Name = name;

            BeginDate = beginDate;
            EndDate = endDate;

            BudgetItem = budgetItem;
            ResponsibilityCenter = responsibilityCenter;

            Scenarios = new List<PlannedOperationScenario>
            {
                new PlannedOperationScenario ( )
            };
        }

        public PlannedOperation (string name, DateTime beginDate, DateTime endDate, BudgetItem budgetItem, ResponsibilityCenter responsibilityCenter,
            Scenario scenario, double value, double labourIntensity = 0)
        {
            Name = name;

            BeginDate = beginDate;
            EndDate = endDate;

            BudgetItem = budgetItem;
            ResponsibilityCenter = responsibilityCenter;

            Scenarios = new List<PlannedOperationScenario>
            {
                new PlannedOperationScenario (scenario, value, labourIntensity)
            };
        }

        public override string ToString ( )
        {
            return Name;
        }
    }
}
