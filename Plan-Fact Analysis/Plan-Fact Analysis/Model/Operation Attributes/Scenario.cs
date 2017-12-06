namespace PlanFactAnalysis.Model
{
    /// <summary>
    /// Сценарий операции.
    /// </summary>
    public sealed class Scenario
    {
        public static Scenario Default { get; set; }

        /// <summary>
        /// Название сценария.
        /// </summary>
        public string Name { get; set; }

        public Scenario ( )
        {

        }

        public Scenario (string name)
        {
            Name = name;
        }

        public override string ToString ( )
        {
            return Name;
        }
    }
}