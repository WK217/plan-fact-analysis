using System.Collections.ObjectModel;

namespace PlanFactAnalysis.ViewModel
{
    [Magic]
    internal sealed class BudgetItemsCollectionViewModel: ViewModelBase
    {
        public BudgetItemsCollectionViewModel ( )
        {
            _addObjectCommand = new RelayCommand (param =>
            {
                BudgetItemViewModel.Collection.Add (NewObject);
                NewObject = new BudgetItemViewModel ( );
            });
        }

        public BudgetItemViewModel NewObject { get; set; } = new BudgetItemViewModel ( );

        public ObservableCollection<BudgetItemViewModel> Items => BudgetItemViewModel.Collection;

        readonly RelayCommand _addObjectCommand;
        public RelayCommand AddObjectCommand => _addObjectCommand;
    }
}