using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;

[XmlRoot("Container")]
public class Container : PlayerInteraction
{
    public string? Name { get; set; }
    public string? Description { get; set; }

    [XmlArray("Items")]
    [XmlArrayItem("Item")]
    public List<Item>? Contents { get; set; }

    /*
    public Item? Remove(Item item)
    {
        if (_contents is not null)  
        {
            if (_contents.Contains(item))
            {
                _contents.Remove(item);
                return item;
            }

            else
            {
                return null;
            }
        }

        else
        {
            return null;
        }
    }
    */
}