using Newtonsoft.Json;

namespace DNDApp.VM
{
    public class ClipsItem : WeaponItem
    {
        [JsonIgnore]
        int capacity;
        public int Capacity
        {
            get => capacity;
            set
            {
                capacity = value;
                OnPropertyChanged();
            }
        }
    }
}
