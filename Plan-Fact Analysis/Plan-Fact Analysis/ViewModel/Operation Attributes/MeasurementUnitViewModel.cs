using PlanFactAnalysis.Model;

namespace PlanFactAnalysis.ViewModel
{
    [Magic]
    internal sealed class MeasurementUnitViewModel : ViewModelBase<MeasurementUnit>, IOperationAttributeViewModel
    {
        public MeasurementUnitViewModel (MainViewModel context)
            : base (context)
        {

        }

        public MeasurementUnitViewModel (MeasurementUnit model, MainViewModel context)
            : base (model, context)
        {

        }

        public bool IsDefault => MeasurementUnit.Default == _context.MeasurementUnits.GetModelFromViewModel (this);

        public string Name { get => _model.Name; set => _model.Name = value; }
        public string Designation { get => _model.Designation; set => _model.Designation = value; }

        public override string ToString ( ) => string.Format (@"{0} ({1})", Name, Designation);
        public string FullName => ToString ( );
    }
}