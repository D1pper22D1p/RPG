using System.Text;
using System.Linq;

using Newtonsoft.Json.Converters;

namespace RPG.Models;

internal abstract class InventoryBase
{
    protected static readonly int DefaultCapacity = 20;
}

internal class Inventory<I, S> : InventoryBase
{
    internal record InventorySlot(I SlotNumber, Item<int, string> Item, I Quantity);
    internal S? State { get; }
    internal int InventoryCapacity 
    {
        get;
        set;
    } = DefaultCapacity;
}

internal abstract class Item<F, S>
{
    internal abstract record ItemProperties([JsonProperty("item_id")]S Id, [JsonProperty("item_name")]S Name, [JsonProperty("item_weight")]F Weight);
}

internal class ConsumableItem : Item<int, string>
{
    internal new record ItemProperties(
        [JsonProperty("item_id")]string Id, 
        [JsonProperty("item_name")]string Name, 
        [JsonProperty("item_weight")]float Weight,
        [JsonConverter(typeof(StringEnumConverter))]PurposeList DefaultPurpose);

    [JsonConverter(typeof(StringEnumConverter))]
    internal enum PurposeList{    
        food,
        drink,
        @throw
    };
    
    static readonly List<ItemProperties> ItemPropertiesList = new()
    {
        { new ItemProperties("bread", "Хлеб", 1.0f, PurposeList.food)},
        { new ItemProperties("apple", "Яблоко", 0.3f, PurposeList.food)}
    };

    [JsonProperty("item_purpose")]
    internal Dictionary<string, PurposeList>? Purpose { get; init; } = StaticPurpose;
    internal static Dictionary<string, PurposeList>? StaticPurpose { get; set; } = 
        ItemPropertiesList.ToDictionary
            (
                x => x.Id,
                g => g.DefaultPurpose
            );
}

internal class SaveConsumableItems : ISaveManageForItems
{
    internal static void SaveItemsList(string filePath)
    {
        try
        {
            string? directory = Path.GetDirectoryName(filePath);

            if(!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                        Directory.CreateDirectory(directory);

            JsonSerializerSettings settings = new() 
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
            };
            ConsumableItem consItem = new()
            {
              Purpose = ConsumableItem.StaticPurpose
            };
            string json = JsonConvert.SerializeObject(consItem, settings);

            File.WriteAllText(filePath, json, Encoding.UTF8);
            Console.WriteLine($"Data about items saved to: {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving files about items: {ex.Message}");
            Console.WriteLine($"Full path: {Path.GetFullPath(filePath)}");
        }
    }
}