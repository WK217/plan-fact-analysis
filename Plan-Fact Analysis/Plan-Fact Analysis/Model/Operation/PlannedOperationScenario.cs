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

        public PlannedOperationScenario ( )
        {
            Scenario = Scenario.Default;
        }

        public PlannedOperationScenario (Scenario scenario, double value, double labourIntensity = 0)
            : base (value, labourIntensity)
        {
            Scenario = scenario;
        }

        public override string ToString ( )
        {
            return string.Format(@"{0} — {1} ({2} ч.)", Scenario.Name, Value, LabourIntensity);
        }
    }
}