using System;

namespace PlanFactAnalysis.Model
{
    /// <summary>
    /// Фактическая операция.
    /// </summary>
    public sealed class ActualOperation : Operation, IOperationInfo
    {
        /// <summary>
        /// Название операции.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дата осуществления операции.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Статья бюджета.
        /// </summary>
        BudgetItem _budgetItem;

        /// <summary>
        /// Статья бюджета.
        /// </summary>
        public BudgetItem BudgetItem
        {
            get => PlannedOperation == null ? _budgetItem : PlannedOperation.BudgetItem;
            set => _budgetItem = value;
        }

        /// <summary>
        /// Центр финансовой ответственности (ЦФО).
        /// </summary>
        ResponsibilityCenter _responsibilityCenter;

        /// <summary>
        /// Центр финансовой ответственности (ЦФО).
        /// </summary>
        public ResponsibilityCenter ResponsibilityCenter
        {
            get => PlannedOperation == null ? _responsibilityCenter : PlannedOperation.ResponsibilityCenter;
            set => _responsibilityCenter = value;
        }

        /// <summary>
        /// Соответствующая запланированная операция.
        /// </summary>
        public PlannedOperationCore PlannedOperation { get; set; }

        public ActualOperation (string name, DateTime date, BudgetItem budgetItem, ResponsibilityCenter responsibilityCenter, double value, double labourIntensity = 0)
            : base (value, labourIntensity)
        {
            Name = name;
            Date = date;

            BudgetItem = budgetItem;
            ResponsibilityCenter = responsibilityCenter;
        }

        public ActualOperation (string name, DateTime date, PlannedOperationCore plannedOperation, double value, double labourIntensity = 0)
            : base (value, labourIntensity)
        {
            Name = name;
            Date = date;

            PlannedOperation = plannedOperation;
        }
    }
}
