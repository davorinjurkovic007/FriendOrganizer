using FriendOrganizer.Model;
using FriendOrganizer.UI.Data;
using FriendOrganizer.UI.Data.Repositories;
using FriendOrganizer.UI.Event;
using FriendOrganizer.UI.Wrapper;
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
    public class FriendDetailViewModel : ViewModelBase, IFriendDetailViewModel
    {
        private IFriendRepository friendRepository;
        private IEventAggregator eventAggregator;
        private FriendWrapper friend;
        private bool hasChanges;

        public FriendDetailViewModel(IFriendRepository friendRepository, IEventAggregator eventAggregator)
        {
            this.friendRepository = friendRepository;
            this.eventAggregator = eventAggregator;

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
        }

        public async Task LoadAsync(int friendId)
        {
            var friend = await friendRepository.GetByIdAsync(friendId);

            Friend = new FriendWrapper(friend);
            Friend.PropertyChanged += (s, e) =>
            {
                if(!HasChanges)
                {
                    HasChanges = friendRepository.HasChanges();
                }
                if(e.PropertyName == nameof(Friend.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        public FriendWrapper Friend
        {
            get { return friend; }
            private set
            {
                friend = value;
                OnPropertyChanged();
            }
        }

        public bool HasChanges
        {
            get { return hasChanges; }
            set 
            {
                if (hasChanges != value)
                {
                    hasChanges = value;
                    OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public ICommand SaveCommand { get; }

        private async void OnSaveExecute()
        {
            await friendRepository.SaveAsync();
            HasChanges = friendRepository.HasChanges();
            eventAggregator.GetEvent<AfterFriendSaveEvent>().Publish(
                new AfterFriendSavedEventArgs
                {
                    Id = Friend.Id, 
                    DisplayMember = $"{Friend.FirstName} {Friend.LastName}"
                });
        }

        private bool OnSaveCanExecute()
        {
            return Friend!=null && !Friend.HasErrors && HasChanges;
        }
    }
}
