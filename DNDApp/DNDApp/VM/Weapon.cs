using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System;
using Newtonsoft.Json;

namespace DNDApp.VM
{
    public class Weapon : SimpeItem
    {
        public static event EventHandler RemoveEvent;
        public static event EventHandler SaveEvent;
        public Weapon() : base()
        {
            SelectCommand = new Command(OnSelect);
            SaveCommand = new Command(OnSave);
            AddClipCommand = new Command(OnAddClip);
            RemoveClipCommand = new Command(OnRemoveClip);
            AddModifierCommand = new Command(OnAddModifier);
            RemoveModifierCommand = new Command(OnRemoveModifier);
            AddAmmoCommand = new Command(OnAddAmmo);
            RemoveAmmoCommand = new Command(OnRemoveAmmo);
            RemoveCommand = new Command(OnRemove);
        }
        #region Propertyes
        [JsonIgnore]
        string ammotype;
        public string AmmoType
        {
            get => ammotype;
            set
            {
                ammotype = value;
                OnPropertyChanged();
            }
        }
        [JsonIgnore]
        int bonus;
        public int Bonus
        {
            get => bonus;
            set
            {
                bonus = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(BonusString));
            }
        }
        [JsonIgnore]
        public string BonusString => (bonus > 0 ? "+" : "") + bonus.ToString();
        [JsonIgnore]
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
        [JsonIgnore]
        ObservableCollection<WeaponItem> weaponmodifiers;
        public ObservableCollection<WeaponItem> WeaponModifiers
        {
            get => weaponmodifiers;
            set
            {
                weaponmodifiers = value;
                OnPropertyChanged();
            }
        }
        [JsonIgnore]
        ObservableCollection<WeaponItem> ammotems;
        public ObservableCollection<WeaponItem> AmmoItems
        {
            get => ammotems;
            set
            {
                ammotems = value;
                OnPropertyChanged();
            }
        }
        [JsonIgnore]
        ObservableCollection<ClipsItem> clipstems;
        public ObservableCollection<ClipsItem> ClipsItems
        {
            get => clipstems;
            set
            {
                clipstems = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Commands
        [JsonIgnore]
        public ICommand AddClipCommand { get; set; }
        void OnAddClip(object obj) => ClipsItems.Add(new ClipsItem());
        [JsonIgnore]
        public ICommand RemoveClipCommand { get; set; }
        void OnRemoveClip(object obj) => ClipsItems.Remove(ClipsItems.LastOrDefault());
        [JsonIgnore]
        public ICommand AddModifierCommand { get; set; }
        void OnAddModifier(object obj) => WeaponModifiers.Add(new WeaponItem() { Value = 1 });
        [JsonIgnore]
        public ICommand RemoveModifierCommand { get; set; }
        void OnRemoveModifier(object obj) => WeaponModifiers.Remove(WeaponModifiers.LastOrDefault());
        [JsonIgnore]
        public ICommand AddAmmoCommand { get; set; }
        void OnAddAmmo(object obj) => AmmoItems.Add(new WeaponItem());
        [JsonIgnore]
        public ICommand RemoveAmmoCommand { get; set; }
        void OnRemoveAmmo(object obj) => AmmoItems.Remove(AmmoItems.LastOrDefault());
        void OnSave(object obj)
        {
            IsEditable = false;
            SaveEvent?.Invoke(this, EventArgs.Empty);
        }
        void OnSelect(object obj)
        {
            IsEditable = true;
        }
        void OnRemove(object obj)
        {
            RemoveEvent?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}