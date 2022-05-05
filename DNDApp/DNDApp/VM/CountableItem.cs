using Newtonsoft.Json;

namespace DNDApp.VM
{
    public class CountableItem : SimpeItem
    {
        [JsonIgnore]
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
    }
}
