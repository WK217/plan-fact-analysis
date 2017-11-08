using System;

namespace PlanFactAnalysis.Model
{
    /// <summary>
    /// Фактическая операция.
    /// </summary>
    public sealed class ActualOperation : Operation
    {
        /// <summary>
        /// Дата осуществления операции.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Соответствующая запланированная операция.
        /// </summary>
        public PlannedOperation PlannedOperation { get; set; }

        public ActualOperation (string name, BudgetItem budgetItem, ResponsibilityCentre responsibilityCentre, decimal money, double labourIntensity = 0)
            : base (name, budgetItem, responsibilityCentre, money, labourIntensity)
        {

        }

        public ActualOperation (string name, BudgetItem budgetItem, ResponsibilityCentre responsibilityCentre, uint count, double labourIntensity = 0)
            : base (name, budgetItem, responsibilityCentre, count, labourIntensity)
        {

        }
    }
}
