namespace PlanFactAnalysis.Model
{
    /// <summary>
    /// Сценарий операции.
    /// </summary>
    public sealed class Scenario
    {
        public static readonly Scenario Default = new Scenario (@"По умолчанию");

        /// <summary>
        /// Название сценария.
        /// </summary>
        public string Name { get; set; }

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