using Newtonsoft.Json;
using System.Windows.Input;

namespace DNDApp.VM
{
    public class BaseItem : BaseViewModel
    {
        bool iseditable;
        [JsonIgnore]
        public bool IsEditable
        {
            get => iseditable;
            set
            {
                iseditable = value;
                OnPropertyChanged();
            }
        }
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
        public ICommand SelectCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand SaveCommand { get; set; }
    }
}
