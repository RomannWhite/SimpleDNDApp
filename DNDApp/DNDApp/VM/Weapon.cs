using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DNDApp.VM
{
    class Weapon : SimpeItem
    {
        #region Propertyes

        string cartridgetype;
        public string CartridgeType
        {
            get => cartridgetype;
            set
            {
                cartridgetype = value;
                OnPropertyChanged();
            }
        }
        int range;
        public int Range
        {
            get => range;
            set
            {
                range = value;
                OnPropertyChanged();
            }
        }
        bool haskolimator;
        public bool HasKolimator
        {
            get => haskolimator;
            set
            {
                haskolimator = value;
                OnPropertyChanged();
            }
        }
        bool hasunderbarrelgl;
        public bool HasUnderbarrelGL
        {
            get => hasunderbarrelgl;
            set
            {
                hasunderbarrelgl = value;
                OnPropertyChanged();
            }
        }
        int opticalsight;
        public int OpticalSight
        {
            get => opticalsight;
            set
            {
                opticalsight = value;
                OnPropertyChanged();
            }
        }
        ObservableCollection<WeaponItem> weaponitems;
        public ObservableCollection<WeaponItem> WeaponItems
        {
            get => weaponitems;
            set
            {
                weaponitems = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Commands

        #endregion
    }
}