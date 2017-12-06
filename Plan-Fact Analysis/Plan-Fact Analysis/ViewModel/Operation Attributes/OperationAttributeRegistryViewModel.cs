using PlanFactAnalysis.Model;
using System.Collections.Generic;
using System.Linq;

namespace PlanFactAnalysis.ViewModel
{
    [Magic]
    internal sealed class OperationAttributeRegistryViewModel<T, M> : RegistryViewModel<T, M>
        where T : ViewModelBase<M>, IOperationAttributeViewModel
        where M : class, new ( )
    {
        public OperationAttributeRegistryViewModel (MainViewModel context, IList<M> modelItems)
            : base (context, modelItems)
        {

        }

        protected override void RemoveItem (object obj)
        {
            if (typeof(T) == typeof (MeasurementUnitViewModel))
            {
                var items = from item in _context.BudgetItems.Items
                            where item.MeasurementUnit == ItemsCollectionView.CurrentItem as MeasurementUnitViewModel
                            select item;

                foreach (var item in items)
                    item.MeasurementUnit = _context.MeasurementUnits.GetViewModelFromModel (MeasurementUnit.Default);
            }
            else if (typeof (T) == typeof (BudgetItemViewModel))
            {
                var items = from item in _context.PlanRegistry.Items
                            where item.BudgetItem == ItemsCollectionView.CurrentItem as BudgetItemViewModel
                            select item;

                foreach (var item in items)
                    item.BudgetItem = _context.BudgetItems.GetViewModelFromModel (BudgetItem.Default);
            }
            else if (typeof (T) == typeof (ScenarioViewModel))
            {
                //var scenariosRegistries = from item in _context.PlanRegistry.Items
                //            select item.ScenariosRegistry;

                //var scenariosRegistriesItems = from item in scenariosRegistries
                //             select item.Items;

                //foreach (var scenariosRegistriesItem in scenariosRegistriesItems)
                //{
                //    foreach (var item in scenariosRegistriesItem)
                //    {
                //        if (item.ScenarioObject == ItemsCollectionView.CurrentItem as ScenarioViewModel)
                //            scenariosRegistriesItem.Remove (item);
                //    }
                //}
            }
            else if (typeof (T) == typeof (ResponsibilityCenterViewModel))
            {
                var items = from item in _context.PlanRegistry.Items
                            where item.ResponsibilityCenter == ItemsCollectionView.CurrentItem as ResponsibilityCenterViewModel
                            select item;

                foreach (var item in items)
                    item.ResponsibilityCenter = _context.ResponsibilityCenters.GetViewModelFromModel (ResponsibilityCenter.Default);
            }

            base.RemoveItem (obj);
        }

        protected override bool CanRemoveItem (object obj)
        {
            return !(ItemsCollectionView.CurrentItem as T).IsDefault;
        }
    }
}
