using FriendOrganizer.UI.Event;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FriendOrganizer.UI.ViewModel
{
    public class NavigationItemViewModel : ViewModelBase
    {
        private IEventAggregator eventAggregator;
        private string displayMember;

        public NavigationItemViewModel(int id, string displayMember, IEventAggregator eventAggregator)
        {
            Id = id;
            DisplayMember = displayMember;
            this.eventAggregator = eventAggregator;
            OpenFriendDetailViewCommand = new DelegateCommand(OnOpenFriendDetailView);
        }

        public int Id { get; }

        public string DisplayMember
        {
            get { return displayMember; }
            set 
            { 
                displayMember = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenFriendDetailViewCommand { get; }

        private void OnOpenFriendDetailView()
        {
            eventAggregator.GetEvent<OpenFriendDetailViewEvent>().Publish(Id);
        }

    }
}
