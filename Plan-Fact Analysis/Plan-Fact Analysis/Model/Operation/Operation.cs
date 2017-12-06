namespace PlanFactAnalysis.Model
{
    /// <summary>
    /// Базовый класс операции.
    /// </summary>
    public abstract class Operation
    {
        /// <summary>
        /// Показатель операции.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Временные затраты (ч).
        /// </summary>
        public double LabourIntensity { get; set; }

        /// <summary>
        /// Комментарий.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="value">Показатель операции.</param>
        /// <param name="labourIntensity">Временные затраты (ч).</param>
        public Operation (double value, double labourIntensity)
        {
            Value = value;
            LabourIntensity = labourIntensity;
        }

        public Operation ( )
        {

        }
    }
}
