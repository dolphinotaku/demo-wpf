﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace demo_mah_wpf.Entity
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T fieldReference, T newValue, Expression<Func<T>> property)
        {
            bool valueIsDifferent = false;
            if (!object.Equals(fieldReference, newValue))
            {
                valueIsDifferent = true;
                fieldReference = newValue;

                var memberExpression = property.Body as MemberExpression;
                OnPropertyChanged(memberExpression.Member.Name);
            }
            return valueIsDifferent;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        public int GetRandomInt(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return random.Next(min, max);
            }
        }
    }
}
