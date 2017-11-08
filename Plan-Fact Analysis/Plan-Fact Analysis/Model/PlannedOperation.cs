using System;

namespace PlanFactAnalysis.Model
{
    /// <summary>
    /// Запланированная операция.
    /// </summary>
    public sealed class PlannedOperation : Operation
    {
        /// <summary>
        /// Сценарий операции.
        /// </summary>
        public Scenario Scenario { get; set; }

        /// <summary>
        /// Дата начала планирования.
        /// </summary>
        public DateTime BeginDate { get; set; }

        /// <summary>
        /// Дата окончания планирования.
        /// </summary>
        public DateTime EndDate { get; set; }

        public PlannedOperation (string name, BudgetItem budgetItem, ResponsibilityCentre responsibilityCentre, decimal money, double labourIntensity = 0)
            : base (name, budgetItem, responsibilityCentre, money, labourIntensity)
        {

        }

        public PlannedOperation (string name, BudgetItem budgetItem, ResponsibilityCentre responsibilityCentre, uint count, double labourIntensity = 0)
            : base (name, budgetItem, responsibilityCentre, count, labourIntensity)
        {

        }
    }
}
