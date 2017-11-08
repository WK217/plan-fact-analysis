namespace PlanFactAnalysis.Model
{
    /// <summary>
    /// Сценарий операции.
    /// </summary>
    public sealed class Scenario
    {
        /// <summary>
        /// Название сценария.
        /// </summary>
        public string Name { get; set; }

        public Scenario (string name)
        {
            Name = name;
        }
    }
}