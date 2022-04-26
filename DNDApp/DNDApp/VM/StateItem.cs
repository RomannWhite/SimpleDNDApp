using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using DNDApp.Data;
using System.Linq;
using System;

namespace DNDApp.VM
{
    public class StateItem : BaseItem
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
        public string UpgradeСostDescription { get; set; }
        public string Description { get; set; }
        int[] UpgradeСost
        {
            get
            {
                List<int> Result = new List<int>();
                foreach (var item in UpgradeСostDescription)
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
        void OnAddPoint(object obj)
        {
            if (Amount < UpgradeСost.Length)
            {
                Amount++;
                UpdateEvent?.Invoke(this, new NumericEventArgs() { Value = -UpgradeСost[Amount - 1] });
            }
        }
        public ICommand AddPointCommand { get; set; }
        void OnRemovePoint(object obj)
        {
            if (Amount > 0)
            {
                Amount--;
                UpdateEvent?.Invoke(this, new NumericEventArgs() { Value = UpgradeСost[Amount] });
            }
        }
        public ICommand RemovePointCommand { get; set; }
        #endregion
        public StateItemForSerialize GetItemForSerialize() => new StateItemForSerialize()
        {
            Title = Title,
            Amount = Amount
        };
    }
}