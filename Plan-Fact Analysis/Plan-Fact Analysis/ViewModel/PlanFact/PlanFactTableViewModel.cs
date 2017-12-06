using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System;
using PlanFactAnalysis.Model;

namespace PlanFactAnalysis.ViewModel
{
    [Magic]
    internal sealed class PlanFactTableViewModel : SubViewModelBase<MainViewModel>
    {
        readonly PlanRegistryViewModel _planRegistry;
        readonly RegistryViewModel<ActualOperationViewModel, ActualOperation> _factRegistry;

        public PlanFactTableViewModel (PlanRegistryViewModel planRegistry,
            RegistryViewModel<ActualOperationViewModel, ActualOperation> factRegistry,
            MainViewModel context)
            : base (context)
        {
            _planRegistry = planRegistry;
            _factRegistry = factRegistry;
            
            _generateTableCommand = new RelayCommand (param => GenerateTable ( ));
        }

        void GenerateTable ( )
        {
            ObservableCollection<object> items = new ObservableCollection<object> ( );

            foreach (BudgetItemViewModel budgetItem in _context.BudgetItems.Items)
            {                
                BudgetItemComparisonViewModel budgetItemComparison = new BudgetItemComparisonViewModel (this, budgetItem, _context);

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

            ItemsCollectionView = CollectionViewSource.GetDefaultView (items);

            //Items.GroupDescriptions.Add (new PropertyGroupDescription (nameof (PlannedOperationComparisonViewModel.BudgetItem)));
            //Items.GroupDescriptions.Add (new PropertyGroupDescription (nameof (PlanFactComparisonViewModel.PlannedOperation)));

            //Items.SortDescriptions.Add (new SortDescription (nameof (PlanFactComparisonViewModel.BudgetItem), ListSortDirection.Ascending));
            //Items.SortDescriptions.Add (new SortDescription (nameof (PlanFactComparisonViewModel.PlannedOperation), ListSortDirection.Ascending));

            RaisePropertyChanged (nameof (ItemsCollectionView));
        }

        public ICollectionView ItemsCollectionView { get; set; }

        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

        readonly RelayCommand _generateTableCommand;
        public RelayCommand GenerateTableCommand => _generateTableCommand;
    }
}