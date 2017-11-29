namespace PlanFactAnalysis.Model
{
    /// <summary>
    /// Сценарная плановая операция.
    /// </summary>
    public sealed class PlannedOperationScenario : Operation
    {
        /// <summary>
        /// Сценарий плановой операции.
        /// </summary>
        public Scenario Scenario { get; set; }

        public PlannedOperationScenario (Scenario scenario, double value, double labourIntensity = 0)
            : base (value, labourIntensity)
        {
            Scenario = scenario;
        }
    }
}