using PlanFactAnalysis.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace PlanFactAnalysis.ViewModel
{
    [Magic]
    internal sealed class PlannedOperationViewModel : ViewModelBase<PlannedOperation>
    {
        public PlannedOperationViewModel (PlannedOperation model, MainViewModel context)
            : base(model, context)
        {
            ScenariosRegistry = new RegistryViewModel<PlannedOperationScenarioViewModel, PlannedOperationScenario> (context, model.Scenarios);
            
            ScenariosRegistry.AddItemCommand.CanExecutePredicate = (param) =>
            {
                var usedScenarios = from sc in ScenariosRegistry.Items
                                    select sc.ScenarioObject;

                return !usedScenarios.Contains (SelectedScenarioObject);
            };

            ScenariosRegistry.RemoveItemCommand.CanExecutePredicate = (param) =>
            {
                var usedScenarios = from sc in ScenariosRegistry.Items
                                    select sc.ScenarioObject;

                return usedScenarios.Contains (SelectedScenarioObject);
            };

            SelectedScenarioObject = _context.Scenarios.GetViewModelFromModel (Scenario.Default);
        }

        public PlannedOperationViewModel (MainViewModel context)
            : base (context)
        {
            ScenariosRegistry = new RegistryViewModel<PlannedOperationScenarioViewModel, PlannedOperationScenario> (
                context,
                new List<PlannedOperationScenario>
                {
                    new PlannedOperationScenario ()
                });
            
            ScenariosRegistry.AddItemCommand.CanExecutePredicate = (param) =>
            {
                var usedScenarios = from sc in ScenariosRegistry.Items
                                    select sc.ScenarioObject;

                return !usedScenarios.Contains (SelectedScenarioObject);
            };

            ScenariosRegistry.RemoveItemCommand.CanExecutePredicate = (param) =>
            {
                var usedScenarios = from sc in ScenariosRegistry.Items
                                    select sc.ScenarioObject;

                return usedScenarios.Contains (SelectedScenarioObject);
            };

            SelectedScenarioObject = _context.Scenarios.GetViewModelFromModel (Scenario.Default);
        }

        public string Name { get => _model.Name; set => _model.Name = value; }

        RegistryViewModel<PlannedOperationScenarioViewModel, PlannedOperationScenario> _scenariosRegistry;
        public RegistryViewModel<PlannedOperationScenarioViewModel, PlannedOperationScenario> ScenariosRegistry
        {
            get => _scenariosRegistry;
            set
            {
                _scenariosRegistry = value;

                ScenariosRegistryCollectionView = CollectionViewSource.GetDefaultView (value.Items);
                ScenariosRegistryCollectionView.CurrentChanged += (s, e) =>
                {
                    RaisePropertyChanged (nameof (Value));
                    RaisePropertyChanged (nameof (LabourIntensity));
                };

                ScenariosRegistry.Items.CollectionChanged += (s, e) =>
                {
                    RaisePropertyChanged (nameof (SelectedPlannedOperationScenario));
                    RaisePropertyChanged (nameof (Value));
                    RaisePropertyChanged (nameof (LabourIntensity));
                };
            }
        }

        public ICollectionView ScenariosRegistryCollectionView { get; private set; }

        ScenarioViewModel _selectedScenarioObject;
        public ScenarioViewModel SelectedScenarioObject
        {
            get => _selectedScenarioObject;
            set
            {
                _selectedScenarioObject = value;
                RaisePropertyChanged (nameof (SelectedPlannedOperationScenario));
            }
        }

        public PlannedOperationScenarioViewModel SelectedPlannedOperationScenario
        {
            get
            {
                var usedScenarios = from sc in _scenariosRegistry.Items
                                    select sc.ScenarioObject;

                if (usedScenarios.Contains (SelectedScenarioObject))
                    return ScenariosRegistry.Items.FirstOrDefault (sc => sc.ScenarioObject == SelectedScenarioObject);
                else
                {
                    ScenariosRegistry.NewItem.ScenarioObject = SelectedScenarioObject;
                    return ScenariosRegistry.NewItem;
                }
            }
        }

        public DateTime BeginDate
        {
            get => _model.BeginDate;
            set
            {
                _model.BeginDate = value;
                RaisePropertyChanged (nameof (EndDate));
            }
        }

        public DateTime EndDate
        {
            get => _model.EndDate;
            set
            {
                _model.EndDate = value;
                RaisePropertyChanged (nameof (BeginDate));
            }
        }

        PlannedOperationScenarioViewModel SelectedScenario => ScenariosRegistryCollectionView.CurrentItem as PlannedOperationScenarioViewModel;

        public double Value
        {
            get => SelectedScenario == null ? 0 : SelectedScenario.Value;
            set
            {
                if (SelectedScenario == null)
                    return;
                else
                    SelectedScenario.Value = value;
            }
        }

        public double LabourIntensity
        {
            get => SelectedScenario == null ? 0 : SelectedScenario.LabourIntensity;
            set
            {
                if (SelectedScenario == null)
                    return;
                else
                    SelectedScenario.LabourIntensity = value;
            }
        }

        public BudgetItemViewModel BudgetItem
        {
            get => _context.BudgetItems.GetViewModelFromModel (_model.BudgetItem);
            set => _model.BudgetItem = _context.BudgetItems.GetModelFromViewModel (value);
        }

        public ResponsibilityCenterViewModel ResponsibilityCenter
        {
            get => _context.ResponsibilityCenters.GetViewModelFromModel (_model.ResponsibilityCenter);
            set => _model.ResponsibilityCenter = _context.ResponsibilityCenters.GetModelFromViewModel (value);
        }

        public override string ToString ( )
        {
            return Name;
        }
    }
}