using DNDApp.Data;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace DNDApp.VM
{
    class WeaponPageViewModel : BaseViewModel
    {
        public WeaponPageViewModel()
        {
            AddItemCommand = new Command(OnAddItem);
            Weapons = new ObservableCollection<Weapon>(DataKeeper.LoadWeapons());
            Weapons.CollectionChanged += (s, e) => OnPropertyChanged(nameof(TotalInfo));
            Weapon.RemoveEvent += Weapon_RemoveEvent;
            Weapon.SaveEvent += Weapon_SaveEvent;
        }
        public ICommand AddItemCommand { get; set; }
        void OnAddItem(object obj)
        {
            Weapons.Add(new Weapon()
            {
                WeaponModifiers = new ObservableCollection<WeaponItem>(),
                ClipsItems = new ObservableCollection<ClipsItem>()
                {
                    new ClipsItem()
                },
                AmmoItems = new ObservableCollection<WeaponItem>()
                {
                    new WeaponItem()
                }
            });
            Weapons.LastOrDefault().IsEditable = true;
            DataKeeper.SaveWeapons(Weapons.ToList());
        }
        ObservableCollection<Weapon> weapons = new ObservableCollection<Weapon>();
        public ObservableCollection<Weapon> Weapons
        {
            get => weapons;
            set
            {
                weapons = value;
                OnPropertyChanged();
            }
        }
        void Weapon_RemoveEvent(object sender, EventArgs e)
        {
            if (sender is Weapon Weapon)
                Weapons.Remove(Weapon);
            DataKeeper.SaveWeapons(Weapons.ToList());
        }
        void Weapon_SaveEvent(object sender, EventArgs e) => DataKeeper.SaveWeapons(Weapons.ToList());
        public string TotalInfo => $"Общий вес: {Weapons.Select(i => i.Weight).Sum()}кг";
        public int TotalMoney
        {
            get => DataKeeper.LoadData(DataKeeper.TotalMoneyTag);
            set
            {
                DataKeeper.SaveData(value, DataKeeper.TotalMoneyTag);
                OnPropertyChanged();
            }
        }
    }
}
