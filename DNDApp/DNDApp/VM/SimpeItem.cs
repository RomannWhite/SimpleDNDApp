using System.Windows.Input;
using Newtonsoft.Json;

namespace DNDApp.VM
{
    public class SimpeItem : BaseViewModel
    {
        #region IsEditable
        public ICommand SelectCommand { get; set; }
        public ICommand SaveCommand { get; set; }
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
        #endregion
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
    }
}
