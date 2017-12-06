using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Data;

namespace PlanFactAnalysis.View
{
    [ValueConversion (typeof (object), typeof (string))]
    public sealed class DescriptionConverter : IValueConverter
    {
        public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return DependencyProperty.UnsetValue;

            return GetDescription ((Enum)value);
        }

        public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Enum.ToObject (targetType, value);
        }

        public static string GetDescription (object obj)
        {
            Type type = obj.GetType ( );
            MemberInfo[ ] memberInfo = type.GetMember (obj.ToString ( ));

            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[ ] attributes = memberInfo[0].GetCustomAttributes (typeof (DescriptionAttribute), false);
                if (attributes != null && attributes.Length > 0)
                    return ((DescriptionAttribute)attributes[0]).Description;
            }

            return obj.ToString ( );
        }
    }
}
