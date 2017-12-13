namespace PlanFactAnalysis.Model
{
    /// <summary>
    /// Сценарий операции.
    /// </summary>
    public sealed class Scenario
    {
        public static Scenario Default { get; } = new Scenario (name: "Реалистичный");

        /// <summary>
        /// Название сценария.
        /// </summary>
        public string Name { get; set; }

        public bool IsDefault
        {
            get => Default == this;
        }

        public Scenario ( )
        {

        }

        public Scenario (string name)
        {
            Name = name;
        }

        public string GenerateSQLInsertQuery ( )
        {
            if (!IsDefault)
                return string.Format (@"INSERT INTO scenario (name)
                    VALUES ('{0}')", Name);
            else
                return string.Format (@"INSERT INTO scenario (id, name)
                    VALUES ('0', '{0}')", Name);
        }

        public override string ToString ( )
        {
            return Name;
        }
    }
}