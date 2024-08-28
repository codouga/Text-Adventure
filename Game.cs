using System.Xml;
using System.Xml.Serialization;

sealed class Game
{
    public static readonly Game Instance = new Game();
    public static List<Location> locations = new List<Location>();
    private static string _gameXml = "Game.xml";


    public static void Create()
    {
        using (XmlReader xmlReader = XmlReader.Create(_gameXml))
        {
            xmlReader.MoveToContent();
            XmlSerializer serializer = new XmlSerializer(typeof(Location));

            while (xmlReader.Read())
            {
                Location temp = (Location)serializer.Deserialize(xmlReader);
                if (temp != null)
                {
                    locations.Add(temp);
                }
            }

            xmlReader.Dispose();
        }

        ConnectDoors();
        SetupPlayer();
    }
    // rethink structure; infinitely refers to itself
    private static void ConnectDoors()
    {
        foreach (Location location in locations)
        {
            if (location.RoomWalls != null)
            {
                foreach (Wall wall in location.RoomWalls)
                {
                    if (wall.MyDoor != null)
                    {
                        int next = wall.MyDoor.NextID;
                        int previous = wall.MyDoor.PreviousID;

                        if (next >= 0)
                        {
                            Location? nextTemp = locations.Where(nextTemp => nextTemp.LocationID == wall.MyDoor.NextID).FirstOrDefault();
                            wall.MyDoor.Previous = nextTemp;
                        }

                        if (previous >= 0)
                        {
                            Location? prevTemp = locations.Where(prevTemp => prevTemp.LocationID == wall.MyDoor.PreviousID).FirstOrDefault();
                            wall.MyDoor.Next = prevTemp;
                        }               
                    }
                }
            }
        }
    }

    private static void SetupPlayer()
    {
        Player.Current = locations[0];
        Player.Focus = Player.Current;
    }

}