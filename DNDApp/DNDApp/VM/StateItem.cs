using System.Collections.Generic;
using System.Windows.Input;
using Newtonsoft.Json;
using Xamarin.Forms;
using DNDApp.Data;
using System;

namespace DNDApp.VM
{
    public class StateItem : CountableItem
    {
        public event EventHandler UpdateEvent;
        public StateItem()
        {
            SelectCommand = new Command(OnSelect);
            SaveCommand = new Command(OnSave);
            AddPointCommand = new Command(OnAddPoint);
            RemovePointCommand = new Command(OnRemovePoint);
        }
        #region Propertyes
        [JsonIgnore]
        string upgradecostdescription;
        public string UpgradeCostDescription
        {
            get => upgradecostdescription;
            set
            {
                upgradecostdescription = value;
                OnPropertyChanged();
            }
        }
        [JsonIgnore]
        string description;
        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged();
            }
        }
        [JsonIgnore]
        int[] UpgradeCost
        {
            get
            {
                List<int> Result = new List<int>();
                foreach (var item in UpgradeCostDescription)
                {
                    if (int.TryParse(item.ToString(), out int value))
                        Result.Add(value);
                    else
                        Result.Add(0);
                }
                return Result.ToArray();
            }
        }
        #endregion
        #region Commands
        void OnSelect(object obj)
        {
            IsEditable = true;
        }
        void OnSave(object obj)
        {
            IsEditable = false;
        }
        [JsonIgnore]
        public ICommand AddPointCommand { get; set; }
        void OnAddPoint(object obj)
        {
            if (Amount < UpgradeCost.Length)
            {
                Amount++;
                UpdateEvent?.Invoke(this, new NumericEventArgs() { Value = -UpgradeCost[Amount - 1] });
            }
        }
        [JsonIgnore]
        public ICommand RemovePointCommand { get; set; }
        void OnRemovePoint(object obj)
        {
            if (Amount > 0)
            {
                Amount--;
                UpdateEvent?.Invoke(this, new NumericEventArgs() { Value = UpgradeCost[Amount] });
            }
        }
        #endregion
    }
}