using PlanFactAnalysis.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PlanFactAnalysis.ViewModel
{
    //[Magic]
    //internal sealed class FactRegistryViewModel : PropertyChangedBase
    //{
    //    public ObservableCollection<ActualOperationViewModel> Items { get; set; }

    //    public FactRegistryViewModel (IEnumerable<ActualOperation> operations)
    //    {
    //        ObservableCollection<ActualOperationViewModel> collection = new ObservableCollection<ActualOperationViewModel> ( );
    //        foreach (var item in operations)
    //            collection.Add (new ActualOperationViewModel (item));

    //        //Items = CollectionViewSource.GetDefaultView (collection);
    //        Items = collection;

    //        _addObjectCommand = new RelayCommand (param =>
    //        {
    //            Items.Add (NewObject);
    //            NewObject = new ActualOperationViewModel ( );
    //        });
    //    }

    //    public ActualOperationViewModel NewObject { get; set; } = new ActualOperationViewModel ( );

    //    readonly RelayCommand _addObjectCommand;
    //    public RelayCommand AddObjectCommand => _addObjectCommand;
    //}
}