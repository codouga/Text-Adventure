using System.Xml.Serialization;

[XmlRoot("Wall")]
public class Wall
{
    [XmlElement("Door")]
    public Door? MyDoor { get; set; }
}