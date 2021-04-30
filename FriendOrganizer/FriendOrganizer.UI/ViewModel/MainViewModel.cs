using FriendOrganizer.UI.Event;
using FriendOrganizer.UI.View.Services;
using Prism.Events;
using System;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private IEventAggregator eventAggregator;
        private IMessageDialogService messageDialogService;
        private Func<IFriendDetailViewModel> friendDetailViewModelCreator;
        private IFriendDetailViewModel friendDetailViewModel;

        public MainViewModel(INavigationViewModel navigationViewModel, 
            Func<IFriendDetailViewModel> friendDetailViewModelCreator, 
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            this.friendDetailViewModelCreator = friendDetailViewModelCreator;
            this.eventAggregator = eventAggregator;
            this.messageDialogService = messageDialogService;
            this.eventAggregator.GetEvent<OpenFriendDetailViewEvent>().Subscribe(OnOpenFriendDetailView);

            NavigationViewModel = navigationViewModel;
        }

        public INavigationViewModel NavigationViewModel { get; }

        public IFriendDetailViewModel FriendDetailViewModel
        {
            get { return friendDetailViewModel; }
            private set 
            { 
                friendDetailViewModel = value;
                OnPropertyChanged();
            }
        }


        public void Load()
        {
            NavigationViewModel.Load();
        }

        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

        private async void OnOpenFriendDetailView(int friendId)
        {
            if(FriendDetailViewModel != null && FriendDetailViewModel.HasChanges)
            {
                var result = messageDialogService.ShowOkCancelDialog("You've made changes. Navigate away?", "Question");
                if(result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }
            FriendDetailViewModel = friendDetailViewModelCreator();

            await FriendDetailViewModel.LoadAsync(friendId);
        }
    }
}
