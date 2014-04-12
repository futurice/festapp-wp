using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using FestApp.Utils;
using System.Windows.Threading;

namespace FestApp.ViewModels
{
    public abstract class ViewModelBase : PropertyChangedBase
    {
        // Returns whether or not the value changed
        public bool SetVMProperty<T>(Expression<Func<T>> propertyExpr, T newValue)
        {
            var oldValue = this.ExtractPropertyValue(propertyExpr);
            if (EqualityComparer<T>.Default.Equals(oldValue, newValue)) { return false; }

            this.SetPropertyValue(propertyExpr, newValue);
            SmartDispatcher.BeginInvoke(() => NotifyChanged(propertyExpr));
            return true;
        }

    }
}
