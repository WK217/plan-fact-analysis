using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PlanFactAnalysis.ViewModel
{
    class MagicAttribute : Attribute { }
    class NoMagicAttribute : Attribute { }

    [Magic]
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public virtual void RaisePropertyChanged ([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void UpdateAllProperties ( )
        {
            var properties = GetType ( ).GetProperties ( );

            foreach (var property in properties)
            {
                if (property.CanRead)
                    RaisePropertyChanged (property.Name);
            }
        }
    }
}
