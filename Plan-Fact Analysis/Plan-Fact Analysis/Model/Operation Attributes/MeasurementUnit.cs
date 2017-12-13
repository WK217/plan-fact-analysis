namespace PlanFactAnalysis.Model
{
    /// <summary>
    /// Единица измерения.
    /// </summary>
    public sealed class MeasurementUnit
    {
        public static MeasurementUnit Default { get; } = new MeasurementUnit (name: "Рубли", designation: "руб.");

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

        public bool IsDefault
        {
            get => Default == this;
        }

        public string GenerateSQLInsertQuery ( )
        {
            if (!IsDefault)
                return string.Format (@"INSERT INTO measurement_unit (name, designation)
                    VALUES ('{0}', '{1}')", Name, Designation);
            else
                return string.Format (@"INSERT INTO measurement_unit (id, name, designation)
                    VALUES ('0', '{0}', '{1}')", Name, Designation);
        }

        public override string ToString ( )
        {
            return string.Format (@"{0} ({1})", Name, Designation);
        }
    }
}