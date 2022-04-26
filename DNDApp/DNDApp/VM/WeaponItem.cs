using System;
using System.Collections.Generic;
using System.Text;

namespace DNDApp.VM
{
    class WeaponItem : BaseViewModel
    {
        string title;
        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }
        string value;
        public string Value
        {
            get => value;
            set
            {
                value = value;
                OnPropertyChanged();
            }
        }
    }
}