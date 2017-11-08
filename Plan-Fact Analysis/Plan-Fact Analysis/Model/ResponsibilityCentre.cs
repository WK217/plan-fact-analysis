namespace PlanFactAnalysis.Model
{
    /// <summary>
    /// Центр финансовой ответственности (ЦФО).
    /// </summary>
    public sealed class ResponsibilityCentre
    {
        /// <summary>
        /// Название ЦФО.
        /// </summary>
        public string Name { get; set; }

        public ResponsibilityCentre (string name)
        {
            Name = name;
        }
    }
}