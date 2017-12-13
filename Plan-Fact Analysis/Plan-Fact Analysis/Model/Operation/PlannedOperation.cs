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

        DateTime _beginDate = DateTime.Today, _endDate = DateTime.Today;

        /// <summary>
        /// Дата начала планирования.
        /// </summary>
        public DateTime BeginDate
        {
            get => _beginDate;
            set
            {
                _beginDate = value;

                if (value > _endDate)
                    EndDate = value;
            }
        }

        /// <summary>
        /// Дата окончания планирования.
        /// </summary>
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;

                if (value < _beginDate)
                    BeginDate = value;
            }
        }

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
