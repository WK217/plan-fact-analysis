using PlanFactAnalysis.Model;
using System;
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
            if (typeof (T) == typeof (MeasurementUnitViewModel))
            {
                var items = from item in _context.BudgetItems.Items
                            where item.MeasurementUnit == ItemsCollectionView.CurrentItem as MeasurementUnitViewModel
                            select item;

                foreach (var budgetItem in items)
                {
                    budgetItem.MeasurementUnit = _context.MeasurementUnits.GetViewModelFromModel (MeasurementUnit.Default);

                    foreach (var plannedOperation in _context.PlanRegistry.Items)
                        if (plannedOperation.BudgetItem == budgetItem)
                            plannedOperation.BudgetItem.UpdateAllProperties ( );

                    foreach (var actualOperation in _context.FactRegistry.Items)
                        if (actualOperation.BudgetItem == budgetItem)
                            actualOperation.BudgetItem.UpdateAllProperties ( );
                }
            }
            else if (typeof (T) == typeof (BudgetItemViewModel))
            {
                var items = from item in _context.PlanRegistry.Items
                            where item.BudgetItem == ItemsCollectionView.CurrentItem as BudgetItemViewModel
                            select item;

                foreach (var plannedOperation in items)
                {
                    plannedOperation.BudgetItem = _context.BudgetItems.GetViewModelFromModel (BudgetItem.Default);

                    foreach (var actualOperation in _context.FactRegistry.Items)
                        if (actualOperation.PlannedOperation == plannedOperation)
                            actualOperation.UpdateAllProperties ( );
                }
            }
            else if (typeof (T) == typeof (ScenarioViewModel))
            {
                ScenarioViewModel currentItem = ItemsCollectionView.CurrentItem as ScenarioViewModel;

                var plannedOperationsScenariosRegistries = from op in _context.PlanRegistry.Items
                                                           select op.ScenariosRegistry.Items;

                foreach (var item in plannedOperationsScenariosRegistries)
                {
                    foreach (var item2 in item.ToList ( ))
                        if (item2.ScenarioObject == currentItem)
                            item.Remove (item2);
                }

                //presence = plannedOperationsScenariosRegistries.Count ( ) == 0;
            }
            else if (typeof (T) == typeof (ResponsibilityCenterViewModel))
            {
                //ResponsibilityCenterViewModel currentItem = ItemsCollectionView.CurrentItem as ResponsibilityCenterViewModel;

                //var actualOperations = from op in _context.FactRegistry.Items
                //                       where op.ResponsibilityCenter == currentItem
                //                       select op;

                //presence = actualOperations.Count ( ) == 0;

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

        public override void Clear ( )
        {
            foreach (T item in _viewModelItems)
            {
                if (!item.IsDefault)
                {
                    _modelItems.Remove (GetModelFromViewModel (item));
                    _viewModelItems.Remove (item);
                }
            }
        }

        public override void RefreshViewModelList ( )
        {
            foreach (var item in _viewModelItems.ToList ( ))
                if (!item.IsDefault)
                    _viewModelItems.Remove (item);

            foreach (var item in _modelItems)
            {
                if (!_viewModelItems.Contains (GetViewModelFromModel (item)))
                    _viewModelItems.Add ((T)Activator.CreateInstance (typeof (T), item, _context));
            }
        }
    }
}
