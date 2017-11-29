using PlanFactAnalysis.Model;
using System;
using System.Collections.ObjectModel;

namespace PlanFactAnalysis.ViewModel
{
    [Magic]
    public sealed class ScenarioViewModel: ViewModelBase, IEquatable<Scenario>
    {
        readonly Scenario _model;

        public static ObservableCollection<ScenarioViewModel> Collection { get; set; } = new ObservableCollection<ScenarioViewModel> ( );

        public ScenarioViewModel ( )
        {

        }

        public ScenarioViewModel (Scenario model)
            : this (model.Name)
        {
            _model = model;
        }

        public ScenarioViewModel (string name)
        {
            Name = name;

            Collection.Add (this);
        }

        public string Name { get; set; }

        public bool Equals (Scenario model)
        {
            return _model == null ? false : _model == model;
        }

        public override string ToString ( )
        {
            return Name;
        }
    }
}