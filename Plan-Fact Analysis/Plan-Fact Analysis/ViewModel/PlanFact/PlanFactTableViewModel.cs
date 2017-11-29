using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System;

namespace PlanFactAnalysis.ViewModel.PlanFact
{
    [Magic]
    internal sealed class PlanFactTableViewModel : ViewModelBase
    {
        readonly PlanRegistryViewModel _planRegistry;
        readonly FactRegistryViewModel _factRegistry;

        public PlanFactTableViewModel (PlanRegistryViewModel planRegistry, FactRegistryViewModel factRegistry)
        {
            _planRegistry = planRegistry;
            _factRegistry = factRegistry;
            
            _generateTableCommand = new RelayCommand (param => GenerateTable ( ));
        }

        void GenerateTable ( )
        {
            ObservableCollection<object> items = new ObservableCollection<object> ( );

            foreach (BudgetItemViewModel budgetItem in BudgetItemViewModel.Collection)
            {                
                BudgetItemComparisonViewModel budgetItemComparison = new BudgetItemComparisonViewModel (this, budgetItem);

                if (budgetItemComparison.GetPlannedOperations().Count() > 0)
                    items.Add (budgetItemComparison);

                foreach (PlannedOperationViewModel plannedOperation in budgetItemComparison.GetPlannedOperations ( ))
                {
                    if (plannedOperation.BeginDate >= BeginDate && plannedOperation.EndDate <= EndDate)
                    {
                        var actualOperations = from operation in _factRegistry.Items
                                               where operation.PlannedOperation == plannedOperation && (operation.Date >= plannedOperation.BeginDate && operation.Date <= plannedOperation.EndDate)
                                               orderby operation.Name
                                               select operation;

                        items.Add (new PlannedOperationComparisonViewModel (plannedOperation, new ObservableCollection<ActualOperationViewModel> (actualOperations)));

                        foreach (var actualOperation in actualOperations)
                            items.Add (new ActualOperationComparisonViewModel (actualOperation));
                    }
                }
            }

            Items = CollectionViewSource.GetDefaultView (items);

            //Items.GroupDescriptions.Add (new PropertyGroupDescription (nameof (PlannedOperationComparisonViewModel.BudgetItem)));
            //Items.GroupDescriptions.Add (new PropertyGroupDescription (nameof (PlanFactComparisonViewModel.PlannedOperation)));

            //Items.SortDescriptions.Add (new SortDescription (nameof (PlanFactComparisonViewModel.BudgetItem), ListSortDirection.Ascending));
            //Items.SortDescriptions.Add (new SortDescription (nameof (PlanFactComparisonViewModel.PlannedOperation), ListSortDirection.Ascending));

            RaisePropertyChanged (nameof (Items));
        }

        public ICollectionView Items { get; set; }

        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

        readonly RelayCommand _generateTableCommand;
        public RelayCommand GenerateTableCommand => _generateTableCommand;
    }
}