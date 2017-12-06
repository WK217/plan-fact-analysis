namespace PlanFactAnalysis.Model
{
    /// <summary>
    /// Центр финансовой ответственности (ЦФО).
    /// </summary>
    public sealed class ResponsibilityCenter
    {
        public static ResponsibilityCenter Default { get; set; }

        /// <summary>
        /// Название ЦФО.
        /// </summary>
        public string Name { get; set; }

        public ResponsibilityCenter ( )
        {

        }

        public ResponsibilityCenter (string name)
        {
            Name = name;
        }

        public override string ToString ( )
        {
            return Name;
        }
    }
}