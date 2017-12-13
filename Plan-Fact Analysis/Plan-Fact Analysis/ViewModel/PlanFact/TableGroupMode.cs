using System.ComponentModel;

namespace PlanFactAnalysis.ViewModel
{
    internal enum TableGroupMode
    {
        [Description("Статья бюджета → Плановая операция → Фактическая операция")]
        First,
        [Description ("ЦФО → Статья бюджета → Плановая операция → Фактическая операция")]
        Second,
        [Description ("Статья бюджета → ЦФО → Плановая операция → Фактическая операция")]
        Third,
    }
}