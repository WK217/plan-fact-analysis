using System.ComponentModel;

namespace PlanFactAnalysis.Model
{
    public enum UserRole
    {
        [Description("Не задано")]
        None,
        [Description ("Планировщик")]
        Planner,
        [Description ("Исполнитель")]
        Executor,
        [Description ("Руководитель")]
        Manager
    }
}