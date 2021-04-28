using FriendOrganizer.Model;
using FriendOrganizer.UI.Data;
using FriendOrganizer.UI.Event;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.ViewModel
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private IFriendLookupDataService friendLookupService;
        private IEventAggregator eventAggregator;

        public NavigationViewModel(IFriendLookupDataService friendLookupService, IEventAggregator eventAggregator)
        {
            this.friendLookupService = friendLookupService;
            this.eventAggregator = eventAggregator;
            Friends = new ObservableCollection<LookupItem>();
        }

        public async Task LoadAsync()
        {
            var lookup = await friendLookupService.GetFriendLookupAsync();
            Friends.Clear();
            foreach (var item in lookup)
            {
                Friends.Add(item);
            }
        }

        public void Load()
        {
            var lookup = friendLookupService.GetFriendLookup();
            Friends.Clear();
            foreach (var item in lookup)
            {
                Friends.Add(item);
            }
        }

        public ObservableCollection<LookupItem> Friends { get; }

        private LookupItem selectedFriend;

        public LookupItem SelectedFriend
        {
            get { return selectedFriend; }
            set 
            {
                selectedFriend = value;
                OnPropertyChanged();
                if(selectedFriend != null)
                {
                    eventAggregator.GetEvent<OpenFriendDetailViewEvent>().Publish(selectedFriend.Id);
                }
            }
        }

    }
}
