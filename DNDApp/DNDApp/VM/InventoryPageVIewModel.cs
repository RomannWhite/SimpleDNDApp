using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using DNDApp.Data;
using System.Linq;

namespace DNDApp.VM
{
    public class InventoryPageVIewModel : BaseViewModel
    {
        public InventoryPageVIewModel()
        {
            InventoryList = new ObservableCollection<InventoryItem>(DataKeeper.LoadInventory());
            foreach (InventoryItem item in InventoryList)
                item.RemoveEvent += (s, e) => InventoryList.Remove((InventoryItem)s);
            AddItemCommand = new Command(OnAddItem);
            InventoryItem.ChangeEvent += (s, e) => OnPropertyChanged(nameof(TotalCost));
        }
        void OnAddItem(object obj)
        {
            InventoryItem NewItem = new InventoryItem()
            {
                Amount = 1
            };
            NewItem.RemoveEvent += (s, e) => InventoryList.Remove((InventoryItem)s);
            NewItem.IsEditable = true;
            InventoryList.Add(NewItem);
        }
        public ICommand AddItemCommand { get; set;}
        ObservableCollection<InventoryItem> inventorylist;
        public ObservableCollection<InventoryItem> InventoryList
        {
            get => inventorylist;
            set
            {
                inventorylist = value;
                OnPropertyChanged();
            }
        }
        public int TotalCost => InventoryList.Select(i => i.Price * i.Amount).Sum();
        public int TotalMoney
        {
            get => (int)DataKeeper.LoadData(DataKeeper.TotalMoneyTag);
            set
            {
                DataKeeper.SaveData(value, DataKeeper.TotalMoneyTag);
                OnPropertyChanged();
            }
        }
    }
}
