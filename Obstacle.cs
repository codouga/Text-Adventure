using System.Xml.Serialization;

[XmlRoot("Obstacle")]
public class Obstacle 
{
    [XmlArray("Items")]
    [XmlArrayItem("Item")]
    public List<Item>? Contents { get; set; }

    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Reaction { get; set; }
    public string? PostDescription { get; set; }
    public bool IsCleared { get; set; } = false;

    public bool CanBurn { get; set; }
    public bool CanCut { get; set; }
    public bool CanLightUp { get; set; }
    public bool CanSmash { get; set; }
    public bool CanShoot { get; set; }
    public bool CanUnlock { get; set; }
}