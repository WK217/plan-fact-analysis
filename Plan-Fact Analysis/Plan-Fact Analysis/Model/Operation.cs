namespace PlanFactAnalysis.Model
{
    /// <summary>
    /// Базовый класс операции.
    /// </summary>
    public abstract class Operation
    {
        /// <summary>
        /// Название операции.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Статья бюджета.
        /// </summary>
        public BudgetItem BudgetItem { get; set; }

        /// <summary>
        /// Статья бюджета.
        /// </summary>
        public ResponsibilityCentre ResponsibilityCentre { get; set; }

        /// <summary>
        /// Сумма операции (руб).
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// Физический показатель.
        /// </summary>
        public uint Count { get; set; }

        /// <summary>
        /// Трудоёмкость (ч).
        /// </summary>
        public double LabourIntensity { get; set; }

        /// <summary>
        /// Комментарий.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Приватный конструктор, инкапсулирующий присвоение названия операции и её трудоёмкости.
        /// </summary>
        /// <param name="name">Название операции.</param>
        /// <param name="budgetItem">Статья бюджета.</param>
        /// <param name="responsibilityCentre">Центр финансовой ответственности.</param>
        /// <param name="labourIntensity">Трудоёмкость (ч).</param>
        Operation (string name, BudgetItem budgetItem, ResponsibilityCentre responsibilityCentre, double labourIntensity = 0.0)
        {
            Name = name;
            BudgetItem = budgetItem;
            ResponsibilityCentre = responsibilityCentre;
            LabourIntensity = labourIntensity;
        }

        /// <summary>
        /// Конструктор для денежных операций.
        /// </summary>
        /// <param name="name">Название операции.</param>
        /// <param name="budgetItem">Статья бюджета.</param>
        /// <param name="responsibilityCentre">Центр финансовой ответственности.</param>
        /// <param name="money">Сумма операции (руб).</param>
        /// <param name="labourIntensity">Трудоёмкость (ч).</param>
        public Operation (string name, BudgetItem budgetItem, ResponsibilityCentre responsibilityCentre, decimal money, double labourIntensity = 0.0)
            : this(name, budgetItem, responsibilityCentre, labourIntensity)
        {
            Money = money;
        }

        /// <summary>
        /// Конструктор для штучных операций.
        /// </summary>
        /// <param name="name">Название операции.</param>
        /// <param name="budgetItem">Статья бюджета.</param>
        /// <param name="responsibilityCentre">Центр финансовой ответственности.</param>
        /// <param name="count">Физический показатель.</param>
        /// <param name="labourIntensity">Трудоёмкость (ч).</param>
        public Operation (string name, BudgetItem budgetItem, ResponsibilityCentre responsibilityCentre, uint count, double labourIntensity = 0.0)
            : this (name, budgetItem, responsibilityCentre, labourIntensity)
        {
            Count = count;
        }
    }
}
