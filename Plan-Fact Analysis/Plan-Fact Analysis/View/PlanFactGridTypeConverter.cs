using PlanFactAnalysis.ViewModel;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PlanFactAnalysis.View
{
    [ValueConversion (typeof (TableGroupMode), typeof (Visibility))]
    public sealed class PlanFactGridTypeConverter : IValueConverter
    {
        public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (TableGroupMode)value + 1 == (TableGroupMode)System.Convert.ToInt32(parameter) ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as TabItem).DataContext;
        }
    }
}
