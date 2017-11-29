using PlanFactAnalysis.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace PlanFactAnalysis.ViewModel
{
    [Magic]
    internal sealed class ActualOperationViewModel : ViewModelBase
    {
        public static ObservableCollection<ActualOperationViewModel> Collection { get; set; } = new ObservableCollection<ActualOperationViewModel> ( );

        public ActualOperationViewModel (ActualOperation model)
        {
            Name = model.Name;

            Date = model.Date;

            Value = model.Value;
            LabourIntensity = model.LabourIntensity;

            PlannedOperation = PlannedOperationViewModel.Collection.Cast<PlannedOperationViewModel> ( ).FirstOrDefault (op => op.Equals(model.PlannedOperation));

            BudgetItem = BudgetItemViewModel.Collection.Cast<BudgetItemViewModel> ( ).FirstOrDefault (bi => bi.Equals (model.BudgetItem));
            ResponsibilityCenter = ResponsibilityCenterViewModel.Collection.Cast<ResponsibilityCenterViewModel> ( ).FirstOrDefault (rc => rc.Equals (model.ResponsibilityCenter));

            Collection.Add (this);
        }

        public ActualOperationViewModel ( )
        {

        }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public double Value { get; set; }
        public double LabourIntensity { get; set; }

        BudgetItemViewModel _budgetItem;
        public BudgetItemViewModel BudgetItem
        {
            get => PlannedOperation == null ? _budgetItem : PlannedOperation.BudgetItem;
            set => _budgetItem = value;
        }

        ResponsibilityCenterViewModel _responsibilityCenter;
        public ResponsibilityCenterViewModel ResponsibilityCenter
        {
            get => PlannedOperation == null ? _responsibilityCenter : PlannedOperation.ResponsibilityCenter;
            set => _responsibilityCenter = value;
        }

        PlannedOperationViewModel _plannedOperation;
        public PlannedOperationViewModel PlannedOperation
        {
            get => _plannedOperation;
            set
            {
                _plannedOperation = value;
                RaisePropertyChanged (nameof (BudgetItem));
                RaisePropertyChanged (nameof (ResponsibilityCenter));
            }
        }

        public override string ToString ( )
        {
            return Name;
        }
    }
}