namespace PlanFactAnalysis.Model
{
    /// <summary>
    /// Статья бюджета.
    /// </summary>
    public sealed class BudgetItem
    {
        /// <summary>
        /// Название статьи бюджета.
        /// </summary>
        public string Name { get; set; }

        public BudgetItem (string name)
        {
            Name = name;
        }
    }
}