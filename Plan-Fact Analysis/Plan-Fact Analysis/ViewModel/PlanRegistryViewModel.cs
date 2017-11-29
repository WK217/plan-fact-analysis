using PlanFactAnalysis.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PlanFactAnalysis.ViewModel
{
    [Magic]
    internal sealed class PlanRegistryViewModel : ViewModelBase
    {
        public ObservableCollection<PlannedOperationViewModel> Items { get; set; }
        
        public PlanRegistryViewModel (IEnumerable<PlannedOperationCore> operations)
        {
            ObservableCollection<PlannedOperationViewModel> collection = new ObservableCollection<PlannedOperationViewModel> ( );
            foreach (var item in operations)
                collection.Add (new PlannedOperationViewModel (item));

            //Items = CollectionViewSource.GetDefaultView (collection);
            Items = collection;

            _addObjectCommand = new RelayCommand (param =>
            {
                Items.Add (NewObject);
                NewObject = new PlannedOperationViewModel ( );
            });
        }

        ScenarioViewModel _selectedScenario;
        public ScenarioViewModel SelectedScenario
        {
            get => _selectedScenario;
            set
            {
                _selectedScenario = value;
                foreach (PlannedOperationViewModel item in Items)
                {
                    PlannedOperationScenarioViewModel newSelectedObject = item.Scenarios.Cast<PlannedOperationScenarioViewModel> ( ).FirstOrDefault (sc => sc.ScenarioObject == value);

                    if (newSelectedObject != null)
                        item.Scenarios.MoveCurrentTo (newSelectedObject);
                }
            }
        }

        public PlannedOperationViewModel NewObject { get; set; } = new PlannedOperationViewModel ( );

        readonly RelayCommand _addObjectCommand;
        public RelayCommand AddObjectCommand => _addObjectCommand;
    }
}