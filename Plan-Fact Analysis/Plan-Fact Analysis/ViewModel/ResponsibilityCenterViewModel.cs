using PlanFactAnalysis.Model;
using System;
using System.Collections.ObjectModel;

namespace PlanFactAnalysis.ViewModel
{
    [Magic]
    public sealed class ResponsibilityCenterViewModel : ViewModelBase, IEquatable<ResponsibilityCenter>
    {
        readonly ResponsibilityCenter _model;

        public static ObservableCollection<ResponsibilityCenterViewModel> Collection { get; set; } = new ObservableCollection<ResponsibilityCenterViewModel> ( );

        public ResponsibilityCenterViewModel (ResponsibilityCenter model)
            : this (model.Name)
        {
            _model = model;
        }

        public ResponsibilityCenterViewModel (string name)
        {
            Name = name;

            Collection.Add (this);
        }

        public ResponsibilityCenterViewModel ( )
        {

        }

        public string Name { get; set; }

        public bool Equals (ResponsibilityCenter model)
        {
            return _model == null ? false : _model == model;
        }

        public override string ToString ( )
        {
            return Name;
        }
    }
}