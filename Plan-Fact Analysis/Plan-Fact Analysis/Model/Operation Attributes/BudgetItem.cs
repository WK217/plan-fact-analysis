namespace PlanFactAnalysis.Model
{
    /// <summary>
    /// Статья бюджета.
    /// </summary>
    public sealed class BudgetItem
    {
        public static BudgetItem Default { get; set; }

        /// <summary>
        /// Название статьи бюджета.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Единица измерения.
        /// </summary>
        public MeasurementUnit MeasurementUnit { get; set; }

        public BudgetItem ( )
        {
            MeasurementUnit = MeasurementUnit.Default;
        }

        public BudgetItem (string name, MeasurementUnit measurementUnit)
        {
            Name = name;
            MeasurementUnit = measurementUnit;
        }

        public override string ToString ( )
        {
            return string.Format (@"{0} ({1})", Name, MeasurementUnit.Designation);
        }
    }
}