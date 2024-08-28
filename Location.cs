using System.Xml.Serialization;

[XmlRoot("Location")]
public class Location : Obstacle
{
    private static int _currentID = 0;
    public int LocationID { get; set; } = _currentID++;
    public bool DisplayAlt { get; set; } = false;

    [XmlArray("Obstacles")]
    [XmlArrayItem("Obstacle")]
    public List<Obstacle>? RoomObstacles { get; set;}
    [XmlArray("Containers")]
    [XmlArrayItem("Container")]
    public List<Container>? RoomContainers { get; set; }
    [XmlArray("Walls")]
    [XmlArrayItem("Wall")]
    public List<Wall>? RoomWalls { get; set;}

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
}