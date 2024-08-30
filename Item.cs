using System.Xml.Serialization;

[XmlRoot("Item")]
public class Item : ItemInteraction
{
    private static int _currentID = 0;
    public int ItemID { get; set; } = _currentID++;
    public int RemainingUses { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    //[XmlElement(IsNullable = true)]
    public string? ConsumeMessage { get; set; }
    public bool Consumable { get; set; }
    
/*
    public void DisplayItem()
    {
        Console.WriteLine($"Name: {Name}\nItem ID: {ItemID}\nDescription: {Description}\nMessage: {ConsumeMessage}\nConsumable: {Consumable}\nUses: {RemainingUses}\nCan burn: {CanBurn}\nCan cut: {CanCut}\nCan light up: {CanLightUp}\nCan smash: {CanSmash}\nCan shoot: {CanShoot}\nCan unlock: {CanUnlock}\n");
    }
*/
}