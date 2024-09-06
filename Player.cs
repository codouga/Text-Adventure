sealed class Player
{
    private static List<Item> _inventory = [];
    private static Location? _current;
    private static Obstacle? _focus;
    //private int _health = 5;
    //private int _sanity = 5;

    public static readonly Player Instance = new Player();

    public static Item? UseItem(string name)
    {
        Item? temp = _inventory.Where(temp => temp.Name == name).FirstOrDefault();
        
        if (temp != null)
        {
            if (temp.Consumable)
            {
                temp.RemainingUses--;

                if (temp.RemainingUses == 0)
                {
                    Console.WriteLine(temp.ConsumeMessage + "\n");
                    _inventory.Remove(temp);
                }
            }
        }

        return temp;
    }
    public static Item? GetItem(string name)
    {
        Item? temp = _inventory.Where(temp => temp.Name == name).FirstOrDefault();
        
        return temp;
    }
    public static void AddItem(Item item)
    {
        _inventory.Add(item);
    }

    public static bool RemoveItem(Item item)
    {
        return _inventory.Remove(item);
    }

    public static void DisplayInventory()
    {
        // causes null reference exception
        foreach (Item item in _inventory)
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

    public static Location? Current
    {
        get => _current;
        set => _current = value;
    }

    public static Obstacle? Focus
    {
        get => _focus;
        set => _focus = value;
    }
}