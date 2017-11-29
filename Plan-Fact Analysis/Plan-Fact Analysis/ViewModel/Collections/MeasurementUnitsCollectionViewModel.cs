using System.Collections.ObjectModel;

namespace PlanFactAnalysis.ViewModel
{
    [Magic]
    internal sealed class MeasurementUnitsCollectionViewModel: ViewModelBase
    {
        public MeasurementUnitsCollectionViewModel ( )
        {
            _addObjectCommand = new RelayCommand (param =>
            {
                Items.Add (NewObject);
                NewObject = new MeasurementUnitViewModel ( );
            });
        }

        public MeasurementUnitViewModel NewObject { get; set; } = new MeasurementUnitViewModel ( );

        public ObservableCollection<MeasurementUnitViewModel> Items => MeasurementUnitViewModel.Collection;

        readonly RelayCommand _addObjectCommand;
        public RelayCommand AddObjectCommand => _addObjectCommand;
    }
}