using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System;
using PlanFactAnalysis.Model;
using System.Collections.Generic;

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
            //Статья бюджета → Плановая операция → Фактическая операция
            //if (groupMode == TableGroupMode.First)
            //{
            ObservableCollection<object> items1 = new ObservableCollection<object> ( );

            foreach (BudgetItemViewModel budgetItem in _context.BudgetItems.Items)
            {
                BudgetItemComparisonViewModel budgetItemComparison = new BudgetItemComparisonViewModel (this, budgetItem, _context);

                if (budgetItemComparison.GetPlannedOperations ( ).Count ( ) > 0)
                    items1.Add (budgetItemComparison);

                foreach (PlannedOperationViewModel plannedOperation in budgetItemComparison.GetPlannedOperations ( ))
                {
                    if (plannedOperation.BeginDate >= BeginDate && plannedOperation.EndDate <= EndDate)
                    {
                        var actualOperations = from operation in _factRegistry.Items
                                               where operation.PlannedOperation == plannedOperation && (operation.Date >= plannedOperation.BeginDate && operation.Date <= plannedOperation.EndDate)
                                               orderby operation.Name
                                               select operation;

                        items1.Add (new PlannedOperationComparisonViewModel (plannedOperation, new ObservableCollection<ActualOperationViewModel> (actualOperations)));

                        foreach (var actualOperation in actualOperations)
                            items1.Add (new ActualOperationComparisonViewModel (actualOperation));
                    }
                }
            }

            ItemsCollectionView1 = CollectionViewSource.GetDefaultView (items1);

            //ЦФО → Статья бюджета → Плановая операция → Фактическая операция
            ObservableCollection<object> items2 = new ObservableCollection<object> ( );
            foreach (var responsibilityCenter in _context.ResponsibilityCenters.Items)
            {
                var ops = from item in _factRegistry.Items
                          where item.ResponsibilityCenter == responsibilityCenter
                          select item;

                if (ops.Count ( ) > 0)
                {
                    items2.Add (new ComparisonViewModel (1) { ResponsibilityCenter = responsibilityCenter.Name });

                    var budgetItems = from item in _context.BudgetItems.Items
                                      select item;

                    foreach (var budgetItem in budgetItems)
                    {
                        var plannedOperations = from plan in _planRegistry.Items
                                                where plan.ResponsibilityCenter == responsibilityCenter && plan.BudgetItem == budgetItem
                                                select plan;

                        var actualOperations = from fact in _factRegistry.Items
                                               where plannedOperations.Contains (fact.PlannedOperation)
                                               select fact;

                        if (actualOperations.Count ( ) > 0)
                        {
                            double plannedValueBI = plannedOperations.Sum (op => op.Value);
                            double actualValueBI = actualOperations.Sum (op => op.Value);

                            items2.Add (new ComparisonViewModel (2)
                            {
                                BudgetItem = budgetItem.Name,
                                PlannedValue = plannedValueBI.ToString ( ),
                                ActualValue = actualValueBI.ToString ( ),
                                AbsoluteDeviation = (actualValueBI - plannedValueBI).ToString ( ),
                                RelativeDeviation = (actualValueBI / plannedValueBI - 1).ToString ( )
                            });

                            foreach (var plannedOperation in plannedOperations)
                            {
                                var actualOperationsInternal = from fact in _factRegistry.Items
                                                               where fact.PlannedOperation == plannedOperation
                                                               select fact;

                                double plannedValuePO = plannedOperation.Value;
                                double actualValuePO = actualOperationsInternal.Sum (op => op.Value);

                                items2.Add (new ComparisonViewModel (3)
                                {
                                    PlannedOperation = plannedOperation.Name,
                                    PlannedValue = plannedValuePO.ToString ( ),
                                    ActualValue = actualValuePO.ToString ( ),
                                    AbsoluteDeviation = (actualValuePO - plannedValuePO).ToString ( ),
                                    RelativeDeviation = (actualValuePO / plannedValuePO - 1).ToString ( )
                                });

                                foreach (var actualOperation in actualOperationsInternal)
                                {
                                    items2.Add (new ComparisonViewModel (4)
                                    {
                                        ActualOperation = actualOperation.Name,
                                        ActualValue = actualOperation.Value.ToString ( )
                                    });
                                }
                            }
                        }                        
                    }
                }
            }

            ItemsCollectionView2 = CollectionViewSource.GetDefaultView (items2);
            //}
            //Статья бюджета → ЦФО → Плановая операция → Фактическая операция.

            ObservableCollection<object> items3 = new ObservableCollection<object> ( );
            foreach (var budgetItem in _context.BudgetItems.Items)
            {
                var ops = from item in _factRegistry.Items
                          where item.BudgetItem == budgetItem
                          select item;

                if (ops.Count ( ) > 0)
                {
                    items3.Add (new ComparisonViewModel (1) { BudgetItem = budgetItem.Name });

                    var responsibilityCenters = from item in _context.ResponsibilityCenters.Items
                                                select item;

                    foreach (var responsibilityCenter in responsibilityCenters)
                    {
                        var plannedOperations = from plan in _planRegistry.Items
                                                where plan.BudgetItem == budgetItem && plan.ResponsibilityCenter == responsibilityCenter
                                                select plan;

                        var actualOperations = from fact in _factRegistry.Items
                                                   where plannedOperations.Contains (fact.PlannedOperation)
                                                   select fact;

                        if (actualOperations.Count ( ) > 0)
                        {
                            double plannedValueBI = plannedOperations.Sum (op => op.Value);
                            double actualValueBI = actualOperations.Sum (op => op.Value);

                            items3.Add (new ComparisonViewModel (2)
                            {
                                ResponsibilityCenter = responsibilityCenter.Name,
                                PlannedValue = plannedValueBI.ToString ( ),
                                ActualValue = actualValueBI.ToString ( ),
                                AbsoluteDeviation = (actualValueBI - plannedValueBI).ToString ( ),
                                RelativeDeviation = (actualValueBI / plannedValueBI - 1).ToString ( )
                            });

                            foreach (var plannedOperation in plannedOperations)
                            {
                                var actualOperationsInternal = from fact in _factRegistry.Items
                                                               where fact.PlannedOperation == plannedOperation
                                                               select fact;

                                double plannedValuePO = plannedOperation.Value;
                                double actualValuePO = actualOperationsInternal.Sum (op => op.Value);

                                items3.Add (new ComparisonViewModel (3)
                                {
                                    PlannedOperation = plannedOperation.Name,
                                    PlannedValue = plannedValuePO.ToString ( ),
                                    ActualValue = actualValuePO.ToString ( ),
                                    AbsoluteDeviation = (actualValuePO - plannedValuePO).ToString ( ),
                                    RelativeDeviation = (actualValuePO / plannedValuePO - 1).ToString ( )
                                });

                                foreach (var actualOperation in actualOperationsInternal)
                                {
                                    items3.Add (new ComparisonViewModel (4)
                                    {
                                        ActualOperation = actualOperation.Name,
                                        ActualValue = actualOperation.Value.ToString ( )
                                    });
                                }
                            }
                        }
                    }
                }
            }

            ItemsCollectionView3 = CollectionViewSource.GetDefaultView (items3);
        }

        public ICollectionView ItemsCollectionView1 { get; set; }
        public ICollectionView ItemsCollectionView2 { get; set; }
        public ICollectionView ItemsCollectionView3 { get; set; }

        DateTime? _beginDate = DateTime.Today,
            _endDate = DateTime.Today;

        public DateTime? BeginDate
        {
            get => _beginDate;
            set
            {
                _beginDate = value;

                if (value > EndDate)
                    EndDate = value;
            }
        }
        public DateTime? EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;

                if (value < BeginDate)
                    BeginDate = value;
            }
        }

        readonly RelayCommand _generateTableCommand;
        public RelayCommand GenerateTableCommand => _generateTableCommand;

        public bool IsEnabled
        {
            get => _context.Authorization.Role == UserRole.Manager;
        }
        
        public IEnumerable<TableGroupMode> TableGroupModes
        {
            get
            {
                yield return TableGroupMode.First;
                yield return TableGroupMode.Second;
                yield return TableGroupMode.Third;
            }
        }

        public TableGroupMode TableGroupMode { get; set; }
    }
}