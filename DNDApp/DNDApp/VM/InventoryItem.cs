using System.Collections.Generic;
using System.Windows.Input;
using Newtonsoft.Json;
using Xamarin.Forms;
using System;

namespace DNDApp.VM
{
    public class InventoryItem : CountableItem
    {
        public event EventHandler RemoveEvent;
        public static event EventHandler ChangeEvent;
        public InventoryItem() : base()
        {
            SelectCommand = new Command(OnSelect);
            SaveCommand = new Command(OnSave);
            AddCommand = new Command(OnAdd);
            RemoveCommand = new Command(OnRemove);
        }
        #region Commands
        void OnRemove(object obj)
        {
            if (Amount > 0)
            {
                Amount--;
            }
            else
            {
                RemoveEvent?.Invoke(this, EventArgs.Empty);
            }
        }
        void OnAdd(object obj)
        {
            Amount++;
        }
        void OnSelect(object obj)
        {
            IsEditable = true;
        }
        void OnSave(object obj)
        {
            IsEditable = false;
            ChangeEvent?.Invoke(null, EventArgs.Empty);
        }
        #endregion
        [JsonIgnore]
        int price;
        public int Price
        {
            get => price;
            set
            {
                price = value;
                OnPropertyChanged();
            }
        }
    }
}