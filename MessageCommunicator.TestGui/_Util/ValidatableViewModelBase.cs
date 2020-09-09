﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MessageCommunicator.TestGui
{
    public class ValidatableViewModelBase : PropertyChangedBase, INotifyDataErrorInfo
    {
        private static readonly string[] NO_ERRORS = new string[0];

        private Dictionary<string, List<string>> _errorsByPropertyName = new Dictionary<string, List<string>>();

        /// <inheritdoc />
        public virtual IEnumerable GetErrors(string propertyName)
        {
            if (_errorsByPropertyName.TryGetValue(propertyName, out var errorList))
            {
                return errorList;
            }
            return NO_ERRORS;
        }

        /// <inheritdoc />
        public bool HasErrors => _errorsByPropertyName.Count > 0;

        /// <inheritdoc />
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        protected virtual void SetError(string propertyName, string error)
        {
            if (_errorsByPropertyName.TryGetValue(propertyName, out var errorList))
            {
                if (!errorList.Contains(error))
                {
                    errorList.Add(error);
                }
            }
            else
            {
                _errorsByPropertyName.Add(propertyName, new List<string>{ error });
            }

            this.ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        protected virtual void RemoveErrors(string propertyName)
        {
            if (_errorsByPropertyName.ContainsKey(propertyName))
            {
                _errorsByPropertyName.Remove(propertyName);
                this.ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }
    }
}
