using System.Windows.Input;

namespace DNDApp.VM
{
    public class BaseItem : SimpeItem
    {
        int amount;
        public int Amount
        {
            get => amount;
            set
            {
                amount = value;
                OnPropertyChanged();
            }
        }
        public ICommand RemoveCommand { get; set; }
        public ICommand AddCommand { get; set; }
    }
}
