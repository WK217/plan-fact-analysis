using PlanFactAnalysis.Model;

namespace PlanFactAnalysis.ViewModel
{
    [Magic]
    internal sealed class ResponsibilityCenterViewModel : ViewModelBase<ResponsibilityCenter>, IOperationAttributeViewModel
    {
        public ResponsibilityCenterViewModel (MainViewModel context)
            : base (context)
        {

        }

        public ResponsibilityCenterViewModel (ResponsibilityCenter model, MainViewModel context)
            : base (model, context)
        {

        }

        public bool IsDefault => ResponsibilityCenter.Default == _context.ResponsibilityCenters.GetModelFromViewModel (this);

        public string Name { get => _model.Name; set => _model.Name = value; }

        public override string ToString ( ) => Name;
    }
}