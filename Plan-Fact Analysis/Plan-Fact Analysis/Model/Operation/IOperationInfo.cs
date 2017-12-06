namespace PlanFactAnalysis.Model
{
    public interface IOperationInfo
    {
        /// <summary>
        /// Название операции.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Статья бюджета.
        /// </summary>
        BudgetItem BudgetItem { get; set; }

        /// <summary>
        /// Центр финансовой ответственности (ЦФО).
        /// </summary>
        ResponsibilityCenter ResponsibilityCenter { get; set; }
    }
}