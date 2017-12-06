using PlanFactAnalysis.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PlanFactAnalysis.View
{
    public sealed class PlanFactTableRowStyleSelector : StyleSelector
    {
        readonly ResourceDictionary _dictionary;

        public PlanFactTableRowStyleSelector ( )
        {
            _dictionary = new ResourceDictionary
            {
                Source = new Uri (@"pack://application:,,,/Plan-Fact Analysis;component/View/PlanFactTableRowStyles.xaml")
            };
        }

        public override Style SelectStyle (object item, DependencyObject container)
        {
            if (item is BudgetItemComparisonViewModel)
                return (Style)_dictionary[@"rowLevel1"];

            else if (item is PlannedOperationComparisonViewModel)
                return (Style)_dictionary[@"rowLevel2"];

            return (Style)_dictionary[@"rowLevel3"];
        }
    }
}
