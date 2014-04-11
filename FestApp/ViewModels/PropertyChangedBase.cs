using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using FestApp.Utils;

namespace FestApp.ViewModels
{
    public abstract class PropertyChangedBase : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        protected PropertyChangedBase()
        {
            PropertyChanged += (_, __) => { };
        }

        protected void NotifyChanged<T>(Expression<Func<T>> propertyExpr)
        {
            var name = propertyExpr.ExtractPropertyName();
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
