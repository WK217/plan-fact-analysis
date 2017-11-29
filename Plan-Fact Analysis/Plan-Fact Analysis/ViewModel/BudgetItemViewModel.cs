using PlanFactAnalysis.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace PlanFactAnalysis.ViewModel
{
    [Magic]
    public sealed class BudgetItemViewModel : ViewModelBase, IEquatable<BudgetItem>
    {
        readonly BudgetItem _model;

        public static ObservableCollection<BudgetItemViewModel> Collection { get; set; } = new ObservableCollection<BudgetItemViewModel> ( );

        public BudgetItemViewModel ( )
        {

        }

        public BudgetItemViewModel (BudgetItem model)
            : this (model.Name, MeasurementUnitViewModel.Collection.FirstOrDefault (unit => unit.Equals (model.MeasurementUnit)))
        {
            Name = Name;
            _model = model;
        }

        public BudgetItemViewModel (string name, MeasurementUnitViewModel measurementUnit)
        {
            Name = name;
            MeasurementUnit = measurementUnit;
            Collection.Add (this);
        }

        public string Name { get; set; }
        public MeasurementUnitViewModel MeasurementUnit { get; set; }

        public string FullName => ToString ( );

        public bool Equals (BudgetItem model)
        {
            return _model == null ? false : _model == model;
        }

        public override string ToString ( )
        {
            return string.Format(@"{0} ({1})", Name, MeasurementUnit.Designation);
        }
    }
}