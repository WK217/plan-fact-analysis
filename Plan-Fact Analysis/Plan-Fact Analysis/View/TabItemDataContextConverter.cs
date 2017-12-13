using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PlanFactAnalysis.View
{
    [ValueConversion (typeof (TabItem), typeof (object))]
    public sealed class TabItemDataContextConverter : IValueConverter
    {
        public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as TabItem).DataContext;
        }
    }
}
