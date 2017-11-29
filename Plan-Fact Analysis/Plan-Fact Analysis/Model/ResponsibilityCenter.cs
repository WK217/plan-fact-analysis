namespace PlanFactAnalysis.Model
{
    /// <summary>
    /// Центр финансовой ответственности (ЦФО).
    /// </summary>
    public sealed class ResponsibilityCenter
    {
        /// <summary>
        /// Название ЦФО.
        /// </summary>
        public string Name { get; set; }

        public ResponsibilityCenter (string name)
        {
            Name = name;
        }
    }
}