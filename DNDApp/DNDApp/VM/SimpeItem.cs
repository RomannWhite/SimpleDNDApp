using System.Windows.Input;
using Newtonsoft.Json;

namespace DNDApp.VM
{
    public class SimpeItem : BaseViewModel
    {
        delegate void EditHandler();
        static event EditHandler OnEditStarted;
        #region IsEditable
        public SimpeItem()
        {
            OnEditStarted += () => IsEditable = false;
        }
        [JsonIgnore]
        bool iseditable;
        [JsonIgnore]
        public bool IsEditable
        {
            get => iseditable;
            set
            {
                if(value)
                    OnEditStarted?.Invoke();
                iseditable = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Commands
        [JsonIgnore]
        public ICommand SelectCommand { get; set; }
        [JsonIgnore]
        public ICommand RemoveCommand { get; set; }
        [JsonIgnore]
        public ICommand SaveCommand { get; set; }
        [JsonIgnore]
        public ICommand AddCommand { get; set; }
        #endregion
        [JsonIgnore]
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
        [JsonIgnore]
        int weight;
        public int Weight
        {
            get => weight;
            set
            {
                weight = value;
                OnPropertyChanged();
            }
        }
    }
}
