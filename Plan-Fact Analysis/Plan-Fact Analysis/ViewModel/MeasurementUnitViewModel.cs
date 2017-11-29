using PlanFactAnalysis.Model;
using System;
using System.Collections.ObjectModel;

namespace PlanFactAnalysis.ViewModel
{
    [Magic]
    public sealed class MeasurementUnitViewModel : ViewModelBase, IEquatable<MeasurementUnit>
    {
        readonly MeasurementUnit _model;

        public static ObservableCollection<MeasurementUnitViewModel> Collection { get; set; } = new ObservableCollection<MeasurementUnitViewModel> ( );

        public MeasurementUnitViewModel ( )
        {

        }

        public MeasurementUnitViewModel (string name, string designation)
        {
            Name = name;
            Designation = designation;

            Collection.Add (this);
        }

        public MeasurementUnitViewModel (MeasurementUnit model)
            : this (model.Name, model.Designation)
        {
            _model = model;
        }

        public string Name { get; set; }
        public string Designation { get; set; }

        public bool Equals (MeasurementUnit model)
        {
            return _model == null ? false : _model == model;
        }

        public override string ToString ( )
        {
            return string.Format(@"{0} ({1})", Name, Designation);
        }
    }
}