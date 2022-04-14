using System.Collections.Generic;
using System.Windows.Input;
using Newtonsoft.Json;
using Xamarin.Forms;
using System;

namespace DNDApp.VM
{
    public class InventoryItem : BaseItem
    {
        static List<InventoryItem> AllItems = new List<InventoryItem>();
        public event EventHandler RemoveEvent;
        public static event EventHandler ChangeEvent;
        public InventoryItem()
        {
            AllItems.Add(this);
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
                AllItems.Remove(this);
                Data.DataKeeper.SaveInventory(AllItems);
                ChangeEvent?.Invoke(null, EventArgs.Empty);
            }
        }
        void OnAdd(object obj)
        {
            Amount++;
        }
        void OnSelect(object obj)
        {
            foreach (InventoryItem item in AllItems)
                item.IsEditable = false;
            IsEditable = true;
        }
        void OnSave(object obj)
        {
            IsEditable = false;
            Data.DataKeeper.SaveInventory(AllItems);
            ChangeEvent?.Invoke(null, EventArgs.Empty);
        }
        #endregion
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