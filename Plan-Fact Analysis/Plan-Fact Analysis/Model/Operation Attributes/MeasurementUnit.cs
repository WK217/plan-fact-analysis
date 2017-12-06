namespace PlanFactAnalysis.Model
{
    /// <summary>
    /// Единица измерения.
    /// </summary>
    public sealed class MeasurementUnit
    {
        public static MeasurementUnit Default { get; set; }

        /// <summary>
        /// Название единицы измерения.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Сокращение единицы измерения.
        /// </summary>
        public string Designation { get; set; }

        public MeasurementUnit ( )
        {

        }

        public MeasurementUnit (string name, string designation)
        {
            Name = name;
            Designation = designation;
        }

        public override string ToString ( )
        {
            return string.Format (@"{0} ({1})", Name, Designation);
        }
    }
}