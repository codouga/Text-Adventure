using System.Collections.Immutable;

class Parser
{
    //Use a list of synonyms for each related word to get to the method call
    private static ImmutableList<string> GO = ImmutableList.Create("go", "move");
    private static ImmutableList<string> DROP = ImmutableList.Create("drop", "discard");
    private static ImmutableList<string> TAKE = ImmutableList.Create("take", "pick", "grab");
    //private static ImmutableList<string> OPEN = ImmutableList.Create("open");
    private static ImmutableList<string> CLOSE = ImmutableList.Create("close", "shut");
    private static ImmutableList<string> THROWS = ImmutableList.Create("throw", "chuck", "yeet", "toss");
    private static ImmutableList<string> PUT = ImmutableList.Create("put", "place");
    private static ImmutableList<string> PUSH = ImmutableList.Create("push", "press");
    private static ImmutableList<string> PULL = ImmutableList.Create("pull", "tug", "yank", "jerk");
    private static ImmutableList<string> TOUCH = ImmutableList.Create("touch", "feel");
    private static ImmutableList<string> QUIT = ImmutableList.Create("quit", "exit", "stop");
    private static ImmutableList<string> YES = ImmutableList.Create("yes", "y");
    private static ImmutableList<string> NO = ImmutableList.Create("no", "n");

    private const string TO = "to";
    private const string USE = "use";
    private const string OPEN = "open";
    private const string ON = "on";
    private const string HELP = "help";
    private const string AT = "at";
    private const string LOOK = "look";
    private const string INVENTORY = "inventory";

    public static bool Parse(string input)
    {

        if (input != "")
        {
            char[] separators = new char[] { ' ', '.' };
            string[] message = input.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            message = message.Select(s => s.ToLowerInvariant()).ToArray();

            if (GO.Contains(message[0]))
            {
                Go(message);
            }

            else if (message[0] == USE)
            {
                Use(message);
            }

            else if (TAKE.Contains(message[0]))
            {
                Take(message);
            }

            else if (PUSH.Contains(message[0]))
            {
                Push(message);
            }

            else if (PULL.Contains(message[0]))
            {
                Pull(message);
            }

            else if (DROP.Contains(message[0]))
            {
                Drop(message);
            }

            else if (THROWS.Contains(message[0]))
            {
                Throw(message);
            }

            else if (PUT.Contains(message[0]))
            {
                Put(message);
            }

            else if (OPEN.Contains(message[0], StringComparison.OrdinalIgnoreCase))
            {
                Open(message);
            }

            else if (LOOK.Contains(message[0], StringComparison.OrdinalIgnoreCase))
            {
                Look(message);
            }

            else if (INVENTORY.Contains(message[0], StringComparison.OrdinalIgnoreCase))
            {
                Inventory();
            }

            else if (CLOSE.Contains(message[0]))
            {
                Close(message);
            }

            else if (TOUCH.Contains(message[0]))
            {
                Touch(message);
            }

            else if (QUIT.Contains(message[0]))
            {
                return Quit();
            }

            else if (message[0] == HELP)
            {
                DisplayHelp();
            }

            else
            {
                DisplayError();
            }

            return false;
        }

        else
        {
            return false;
        }
    }

    // keywords: go, move; auxiliary: to; direction: up, down, left, right, north, south, east, west, object/location
    private static void Go(string[] message)
    {
        Console.WriteLine("In go");
        


    }

    // keywords: take, pick; auxiliary: up; item name
    private static void Take(string[] message)
    {
        //Player.Instance.AddItem(new Item("", false));
        try
        {
            if (Player.Focus.GetType() == typeof(Location))
            {
                Item? temp = Player.Current.Contents.Where(item => item.Name == message[message.Length - 1]).FirstOrDefault();
                Player.Current.Contents.Remove(temp);
                Player.AddItem(temp);
                Console.WriteLine($"You've obtained {temp.Name}\n");
            }

            else if (Player.Focus.GetType() == typeof(Container))
            {
                Container? current = Player.Current.RoomContainers.Where(current => current == Player.Focus).FirstOrDefault();
                Item? temp = current.Contents.Where(temp => temp.Name == message[message.Length - 1]).FirstOrDefault();
                current.Contents.Remove(temp);
                Player.AddItem(temp);
                Console.WriteLine($"You've obtained {temp.Name}\n");
            }

            else if (Player.Focus.GetType() == typeof(Obstacle))
            {
                Obstacle tempObstacle = (Obstacle)Player.Focus;
                
                if (tempObstacle.IsCleared)
                {
                    Item? temp = tempObstacle.Contents.Where(temp => temp.Name == message[message.Length - 1]).FirstOrDefault();
                    tempObstacle.Contents.Remove(temp);
                    Player.AddItem(temp);
                    Console.WriteLine($"You've obtained {temp.Name}\n");
                }
            }
        }

        catch (NullReferenceException)
        {
            DisplayError();
        }
    }

    // keywords: drop, discard
    private static void Drop(string[] message)
    {
        Console.WriteLine("In drop");

        try
        {
            Item? temp = Player.GetItem(message[1]);
            Player.Current.Contents.Add(temp);
            Player.RemoveItem(temp);
            Console.WriteLine($"You've dropped {temp.Name}\n");
        }

        catch (Exception e) when (e is NullReferenceException || e is IndexOutOfRangeException)
        {
            DisplayError();
        }
    }

    // keywords: throw, toss, yeet, chuck; auxiliary: at, away; target
    private static void Throw(string[] message)
    {
        Console.WriteLine("In throw");
    }

    // keywords: open; auxiliary: target door/container
    private static void Open(string[] message)
    {
        Console.WriteLine("In open");
    }

    // keywords: close, shut; auxiliary: target door/container
    private static void Close(string[] message)
    {
        Console.WriteLine("In close");
    }

    // keywords: use; auxiliary: on; item; target
    private static void Use(string[] message, Object? target = null)
    {
        // figure out how to update uses
        //Console.WriteLine("In use");

        try
        {
            Item? temp = Player.UseItem(message[1]);


            if (temp != null && Player.Current != null)
            {
                if (temp.CanBurn == true && Player.Current.CanBurn == true)
                {
                    Player.Current.CanBurn = false;


                    Player.Current.IsCleared = true;
                    Console.WriteLine(Player.Current.Reaction + "\n"
                        + Player.Current.PostDescription + "\n");
                }

                else if (temp.CanCut == true && Player.Current.CanCut == true)
                {
                    Player.Current.CanCut = false;

                    Player.Current.IsCleared = true;
                    Console.WriteLine(Player.Current.Reaction + "\n"
                        + Player.Current.PostDescription + "\n");
                }

                else if (temp.CanLightUp == true && Player.Current.CanLightUp == true)
                {
                    Player.Current.CanLightUp = false;

                    Player.Current.IsCleared = true;
                    Console.WriteLine(Player.Current.Reaction + "\n"
                        + Player.Current.PostDescription + "\n");
                }

                else if (temp.CanSmash == true && Player.Current.CanSmash == true)
                {
                    Player.Current.CanSmash = false;

                    Player.Current.IsCleared = true;
                    Console.WriteLine(Player.Current.Reaction + "\n"
                        + Player.Current.PostDescription + "\n");
                }

                else if (temp.CanShoot == true && Player.Current.CanShoot == true)
                {
                    Player.Current.CanShoot = false;

                    Player.Current.IsCleared = true;
                    Console.WriteLine(Player.Current.Reaction + "\n"
                        + Player.Current.PostDescription + "\n");
                }

                else if (temp.CanUnlock == true && Player.Current.CanUnlock == true)
                {
                    Player.Current.CanUnlock = false;

                    Player.Current.IsCleared = true;
                    Console.WriteLine(Player.Current.Reaction + "\n"
                        + Player.Current.PostDescription + "\n");
                }
            }

            else
            {
                DisplayError();
            }
        }

        catch (Exception e) when (e is NullReferenceException || e is IndexOutOfRangeException)
        {
            DisplayError();
        }


    }

    // keywords: push, press, shove; target/button
    private static void Push(string[] message)
    {
        Console.WriteLine("In push");
    }

    // keywords: pull; target
    private static void Pull(string[] message)
    {
        Console.WriteLine("In pull");
    }

    // keywords: inventory
    private static void Inventory()
    {
        //Console.WriteLine("In inventory");
        Player.DisplayInventory();
    }

    // keywords: put, place; auxiliary: on, in; item; target
    private static void Put(string[] message)
    {
        Console.WriteLine("In put");
    }

    // keywords: look; auxiliary: at, around; direction
    private static void Look(string[] message)
    {
        if (Player.Focus != null && Player.Focus.GetType() == typeof(Location))
        {
            if (Player.Current.IsCleared)
            {
                Console.WriteLine(Player.Current.PostDescription + "\n");
                Player.Current.DisplayItems();
            }

            else
            {
                Console.WriteLine(Player.Current.Description + "\n");
                Player.Current.DisplayItems();
            }
        }

        else if (Player.Focus != null && Player.Focus.GetType() == typeof(Obstacle))
        {
            Obstacle temp = (Obstacle)Player.Focus;

            if (temp.IsCleared)
            {
                Console.WriteLine(temp.PostDescription + "\n");
            }

            else
            {
                Console.WriteLine(temp.Description + "\n");
            }
        }

        else if (Player.Focus != null && Player.Focus.GetType() == typeof(Container))
        {
            Container temp = (Container)Player.Focus;

            Console.WriteLine(temp.Description + "\n");
            temp.DisplayItems();
        }

        else
        {
            DisplayError();
        }
    }

    // keywords: hide; auxiliary: in, under, behind; objlect
    private static void Hide(string[] message)
    {
        Console.WriteLine("In hide");
    }

    // keywords: touch, feel; auxiliary: in, under, behind; object
    private static void Touch(string[] message)
    {
        Console.WriteLine("In touch");
    }

    private static bool Quit()
    {
        string? input;
        char[] separators = new char[] { ' ', '.' };
        Console.Write("Are you sure you want to quit (Y/N)? ");
        input = Console.ReadLine();

        if (input != "" && input is not null)
        {
            string[] message = input.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            if (YES.Contains(message[0].ToLower()))
            {
                return true;
            }

            else if (NO.Contains(message[0].ToLower()))
            {
                return false;
            }

            else
            {
                return false;
            }
        }
        return false;
    }

    private static void DisplayHelp()
    {
        Console.WriteLine("keyword: \"go\"\t\t -- syntax: go + {direction}\t\t\t -- moves player that direction\n"
            + "keyword: \"use\"\t\t -- syntax: use + {item}\t\t\t -- uses designated item\n"
            + "\t\t\t -- syntax: use + {item} + on + {target}\t -- uses designated item on specified target in current area\n"
            + "keyword: \"push\"\t\t -- syntax: push + {obstacle}\t\t\t -- moves objects or pushes buttons\n"
            + "keyword: \"press\"\t -- syntax: press + {button}\t\t\t -- presses buttons or switches\n"
            + "keyword: \"take\"\t\t -- syntax: take + {item}\t\t\t -- takes designated item from room and places it into your inventory\n"
            + "keyword: \"throw\"\t -- syntax: throw + {item}\t\t\t -- throws designated item, dropping it in the process\n"
            + "\t\t\t -- syntax: throw + {item} + at + {target}\t -- throws designated item at a specified target, dropping it or potentially breaking it in the process\n"
            + "keyword: \"open\"\t\t -- syntax: open + {door/container}\t\t -- opens a closed door/container\n"
            + "keyword: \"close\"\t -- syntax: close + {door/container}\t\t -- closes an opened door/container\n"
            + "keyword: \"look\"\t\t -- syntax: look\t\t\t\t -- looks around the general area\n"
            + "\t\t\t -- syntax: look +  at + {target}\t\t -- looks at a specific target\n"
            + "\t\t\t -- syntax: throw + {direction}\t\t\t -- looks in a specified direction and reports what is seen\n");
        // continue adding rest of the keywords and functions
    }

    private static void DisplayError()
    {
        Console.WriteLine("-- You can't do that --");
    }
}