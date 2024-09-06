using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;

[XmlRoot("Container")]
public class Container : Obstacle
{
/*
    public string? Name { get; set; }
    public string? Description { get; set; }

    [XmlArray("Items")]
    [XmlArrayItem("Item")]
    public List<Item>? Contents { get; set; }
*/
    public void DisplayItems()
    {
        if (Contents != null && Contents.Count > 0)
        {
            Console.WriteLine("-- Available items --");
            foreach (Item item in Contents)
            {
                if (item.RemainingUses > 0)
                {
                    Console.WriteLine($"-- {item.Name} : {item.RemainingUses} Uses Remaining");
                }

                else
                {
                    Console.WriteLine($"-- {item.Name}");
                }
            }
            Console.WriteLine();
        }
    }
    
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