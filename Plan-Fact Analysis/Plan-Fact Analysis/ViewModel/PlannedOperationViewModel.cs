using PlanFactAnalysis.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace PlanFactAnalysis.ViewModel
{
    [Magic]
    internal sealed class PlannedOperationViewModel : ViewModelBase, IEquatable<PlannedOperationCore>
    {
        readonly PlannedOperationCore _model;

        public static ObservableCollection<PlannedOperationViewModel> Collection { get; set; } = new ObservableCollection<PlannedOperationViewModel> ( );

        public PlannedOperationViewModel (PlannedOperationCore model)
        {
            _model = model;

            Name = model.Name;

            ObservableCollection<PlannedOperationScenarioViewModel> scenarios = new ObservableCollection<PlannedOperationScenarioViewModel> ( );
            foreach (var scenario in model.Scenarios)
                scenarios.Add (new PlannedOperationScenarioViewModel (scenario));

            Scenarios = CollectionViewSource.GetDefaultView (scenarios);
            Scenarios.CurrentChanged += (s, e) =>
            {
                RaisePropertyChanged (nameof (Value));
                RaisePropertyChanged (nameof (LabourIntensity));
            };

            _beginDate = model.BeginDate;
            _endDate = model.EndDate;

            BudgetItem = BudgetItemViewModel.Collection.Cast<BudgetItemViewModel> ( ).FirstOrDefault (bi => bi.Equals (model.BudgetItem));
            ResponsibilityCenter = ResponsibilityCenterViewModel.Collection.Cast<ResponsibilityCenterViewModel> ( ).FirstOrDefault (rc => rc.Equals (model.ResponsibilityCenter));

            Collection.Add (this);
        }

        public PlannedOperationViewModel (string name, ObservableCollection<PlannedOperationScenarioViewModel> scenarios,
            DateTime beginDate, DateTime endDate, BudgetItemViewModel budgetItem, ResponsibilityCenterViewModel responsibilityCenter)
        {
            Name = name;

            Scenarios = CollectionViewSource.GetDefaultView (scenarios);
            Scenarios.CurrentChanged += (s, e) =>
            {
                RaisePropertyChanged (nameof (Value));
                RaisePropertyChanged (nameof (LabourIntensity));
            };

            _beginDate = beginDate;
            _endDate = endDate;

            BudgetItem = budgetItem;
            ResponsibilityCenter = responsibilityCenter;

            Collection.Add (this);
        }

        public PlannedOperationViewModel ( )
        {
        }

        public string Name { get; set; }

        public ICollectionView Scenarios { get; private set; }

        DateTime _beginDate, _endDate;

        public DateTime BeginDate
        {
            get => _beginDate;
            set
            {
                if (value <= EndDate)
                    _beginDate = value;

                RaisePropertyChanged (nameof (BeginDate));
            }
        }

        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                if (value >= BeginDate)
                    _endDate = value;

                RaisePropertyChanged (nameof (EndDate));
            }
        }

        public double Value
        {
            get => (Scenarios.CurrentItem as PlannedOperationScenarioViewModel).Value;
            set => (Scenarios.CurrentItem as PlannedOperationScenarioViewModel).Value = value;
        }

        public double LabourIntensity
        {
            get => (Scenarios.CurrentItem as PlannedOperationScenarioViewModel).LabourIntensity;
            set => (Scenarios.CurrentItem as PlannedOperationScenarioViewModel).LabourIntensity = value;
        }

        public BudgetItemViewModel BudgetItem { get; set; }
        public ResponsibilityCenterViewModel ResponsibilityCenter { get; set; }

        public bool Equals (PlannedOperationCore model)
        {
            return _model == null ? false : _model == model;
        }

        public override string ToString ( )
        {
            return Name;
        }
    }
}