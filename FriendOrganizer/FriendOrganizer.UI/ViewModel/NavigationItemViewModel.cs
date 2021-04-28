﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.ViewModel
{
    public class NavigationItemViewModel : ViewModelBase
    {
        private string displayMember;

        public NavigationItemViewModel(int id, string displayMember)
        {
            Id = id;
            DisplayMember = displayMember;
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

    }
}
