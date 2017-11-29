using System.Collections.ObjectModel;

namespace PlanFactAnalysis.ViewModel
{
    [Magic]
    internal sealed class ScenariosCollectionViewModel: ViewModelBase
    {
        public ScenariosCollectionViewModel ( )
        {
            _addObjectCommand = new RelayCommand (param =>
            {
                ScenarioViewModel.Collection.Add (NewObject);
                NewObject = new ScenarioViewModel ( );
            });
        }

        public ScenarioViewModel NewObject { get; set; } = new ScenarioViewModel ( );

        public ObservableCollection<ScenarioViewModel> Items => ScenarioViewModel.Collection;

        readonly RelayCommand _addObjectCommand;
        public RelayCommand AddObjectCommand => _addObjectCommand;
    }
}