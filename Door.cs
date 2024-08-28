using System.Xml.Serialization;

[XmlRoot("Door")]
public class Door : Obstacle
{
    public int PreviousID { get; set; }
    public int NextID { get; set; }
    public Location? Previous { get; set; }
    public Location? Next { get; set; }
}