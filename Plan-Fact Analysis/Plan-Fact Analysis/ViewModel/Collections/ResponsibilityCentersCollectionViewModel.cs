using System.Collections.ObjectModel;

namespace PlanFactAnalysis.ViewModel
{
    [Magic]
    internal sealed class ResponsibilityCentersCollectionViewModel: ViewModelBase
    {
        public ResponsibilityCentersCollectionViewModel ( )
        {
            _addObjectCommand = new RelayCommand (param =>
            {
                ResponsibilityCenterViewModel.Collection.Add (NewObject);
                NewObject = new ResponsibilityCenterViewModel ( );
            });
        }

        public ResponsibilityCenterViewModel NewObject { get; set; } = new ResponsibilityCenterViewModel ( );

        public ObservableCollection<ResponsibilityCenterViewModel> Items => ResponsibilityCenterViewModel.Collection;

        readonly RelayCommand _addObjectCommand;
        public RelayCommand AddObjectCommand => _addObjectCommand;
    }
}