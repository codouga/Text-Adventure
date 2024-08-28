using System.Xml.Serialization;

[XmlRoot("Obstacle")]
public class Obstacle : PlayerInteraction
{
    [XmlArray("Items")]
    [XmlArrayItem("Item")]
    public List<Item>? Contents { get; set; }

    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Reaction { get; set; }
    public string? PostDescription { get; set; }
    public bool IsCleared { get; set; } = false;
}