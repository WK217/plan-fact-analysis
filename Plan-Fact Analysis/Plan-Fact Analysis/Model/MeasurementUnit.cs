namespace PlanFactAnalysis.Model
{
    public sealed class MeasurementUnit
    {
        public string Name { get; set; }
        public string Designation { get; set; }

        public MeasurementUnit (string name, string designation)
        {
            Name = name;
            Designation = designation;
        }
    }
}