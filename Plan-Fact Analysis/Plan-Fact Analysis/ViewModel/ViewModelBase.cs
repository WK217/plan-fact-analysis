using System;

namespace PlanFactAnalysis.ViewModel
{
    [Magic]
    internal abstract class ViewModelBase<M> : SubViewModelBase<MainViewModel>, IEquatable<M>
        where M : new ( )
    {
        protected readonly M _model;

        public ViewModelBase (MainViewModel context)
            : base (context)
        {
            _model = new M ( );
        }

        public ViewModelBase (M model, MainViewModel context)
            : base (context)
        {
            _model = model;
        }

        public bool Equals (M model)
        {
            return _model == null || model == null ? false : _model.GetHashCode ( ) == model.GetHashCode ( );
        }
    }
}