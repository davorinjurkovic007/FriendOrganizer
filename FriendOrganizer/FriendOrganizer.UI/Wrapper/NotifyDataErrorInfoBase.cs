using FriendOrganizer.UI.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace FriendOrganizer.UI.Wrapper
{
    public class NotifyDataErrorInfoBase : ViewModelBase, INotifyDataErrorInfo
    {
        private Dictionary<string, List<string>> errorByPropertyName = new Dictionary<string, List<string>>();

        public bool HasErrors => errorByPropertyName.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            return errorByPropertyName.ContainsKey(propertyName) ? errorByPropertyName[propertyName] : null;
        }

        protected virtual void OnErrorChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            base.OnPropertyChanged(nameof(HasErrors));
        }

        protected void AddError(string propertyName, string error)
        {
            if (!errorByPropertyName.ContainsKey(propertyName))
            {
                errorByPropertyName[propertyName] = new List<string>();
            }
            if (!errorByPropertyName[propertyName].Contains(error))
            {
                errorByPropertyName[propertyName].Add(error);
                OnErrorChanged(propertyName);
            }
        }

        protected void ClearErrors(string propertyName)
        {
            if (errorByPropertyName.ContainsKey(propertyName))
            {
                errorByPropertyName.Remove(propertyName);
                OnErrorChanged(propertyName);
            }
        }
    }
}
