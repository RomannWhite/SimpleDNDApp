using System.Collections.Generic;
using Xamarin.Essentials;
using Newtonsoft.Json;
using DNDApp.VM;

namespace DNDApp.Data
{
    public class DataKeeper
    {
        public const string FreePointsTag = "FreePoints";
        public const string TotalMoneyTag = "TotalMoney";
        const string AllInventoryItemTag = "AllInventoryItems";
        const string StateItemsTag = "StateItems";
        public static void SaveStates(List<StateItemForSerialize> items)
        {
            string SerializedItems = JsonConvert.SerializeObject(items);
            SecureStorage.SetAsync(StateItemsTag, SerializedItems);
        }
        public static void SaveInventory(List<InventoryItem> items)
        {
            string SerializedItems = JsonConvert.SerializeObject(items);
            SecureStorage.SetAsync(AllInventoryItemTag, SerializedItems);
        }
        public static void SaveData(float data, string key)
        {
            SecureStorage.SetAsync(key, data.ToString());
        }
        public static float LoadData(string key)
        {
            string stringdata = SecureStorage.GetAsync(key).Result;
            if (!string.IsNullOrEmpty(stringdata))
                if (float.TryParse(stringdata, out float result))
                    return result;
            return 0;
        }
        public static List<InventoryItem> LoadInventory()
        {
            string SerializedItems = SecureStorage.GetAsync(AllInventoryItemTag).Result;
            if (!string.IsNullOrEmpty(SerializedItems))
                try
                {
                    return JsonConvert.DeserializeObject<List<InventoryItem>>(SerializedItems);
                }
                catch (System.Exception Ex)
                {
                    return new List<InventoryItem>()
                {
                    new InventoryItem()
                    {
                        Title = "Deserialize error"
                    }
                };
                }
            return new List<InventoryItem>();
        }
        public static List<StateItemForSerialize> LoadStates()
        {
            string SerializedItems = SecureStorage.GetAsync(StateItemsTag).Result;
            if (!string.IsNullOrEmpty(SerializedItems))
                try
                {
                    return JsonConvert.DeserializeObject<List<StateItemForSerialize>>(SerializedItems);
                }
                catch (System.Exception Ex)
                {
                    return new List<StateItemForSerialize>()
                {
                    new StateItemForSerialize()
                    {
                        Title = "Deserialize error"
                    }
                };
                }
            return new List<StateItemForSerialize>();
        }
    }
}