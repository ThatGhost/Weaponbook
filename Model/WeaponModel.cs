using Newtonsoft.Json;
using System.Windows.Media.Imaging;

using W6Assingment.ViewModel.Commands;

public class Weapon
{
    [JsonProperty("id")]
    public int Id { get; set; }
    [JsonProperty("type")]
    public string type { get; set; }
    [JsonProperty("rarity")]
    public int rarity { get; set; }
    [JsonProperty("attack")]
    public Attack attack { get; set; }
    [JsonProperty("damageType")]
    public string damageType { get; set; }
    [JsonProperty("name")]
    public string name { get; set; }
    [JsonProperty("durability")]
    public Durability[] durability { get; set; }
    [JsonProperty("elements")]
    public Element[] elements { get; set; }
    [JsonProperty("assets")]
    public Assets assets { get; set; }
    [JsonProperty("boostType")]
    public string boostType { get; set; }
    [JsonProperty("specialAmmo")]
    public string specialAmmo { get; set; }
    [JsonProperty("deviation")]
    public string deviation { get; set; }
    [JsonProperty("ammo")]
    public Ammo[] ammo { get; set; }
    [JsonProperty("coatings")]
    public string[] coatings { get; set; }

    [JsonProperty("elderseal")]
    public string elderseal { get; set; }
    [JsonProperty("crafting")]
    public Crafting Crafting { get; set; }

    public RelayCommand OnWeaponCommand { get; set; }
}

public class Attack
{
    [JsonProperty("display")]
    public int display { get; set; }
    [JsonProperty("raw")]
    public int raw { get; set; }
}

public class Crafting
{
    [JsonProperty("upgradeMaterials")]
    public UpgradeMaterials[] UpgradeMaterials { get; set; }
}

public class UpgradeMaterials
{
    [JsonProperty("quantity")]
    public int quantity { get; set; }
    [JsonProperty("item")]
    public Item item { get; set; }
}

public class Item
{
    [JsonProperty("name")]
    public string name { get; set; }
    [JsonProperty("rarity")]
    public int rarity { get; set; }
}

public class Assets
{
    [JsonProperty("icon")]
    public string icon { get; set; }
    [JsonProperty("image")]
    public string image { get; set; }
    public BitmapImage bitmapImage { get; set; }
}

public class Durability
{
    public int red { get; set; }
    public int orange { get; set; }
    public int yellow { get; set; }
    public int green { get; set; }
    public int blue { get; set; }
    public int white { get; set; }
    public int purple { get; set; }
}

public class Element
{
    [JsonProperty("type")]
    public string type { get; set; }
    [JsonProperty("damage")]
    public int damage { get; set; }
}

public class Ammo
{
    [JsonProperty("type")]
    public string type { get; set; }
}
