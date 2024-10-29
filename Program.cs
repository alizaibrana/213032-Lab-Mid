
using System;
using System.Collections.Generic;

public class Society
{
    public string Name { get; }
    public List<string> Events { get; }
    public decimal Funding { get; private set; }

    public Society(string name)
    {
        Name = name;
        Events = new List<string>();
        Funding = 0;
    }

    public void AddEvent(string eventName)
    {
        Events.Add(eventName);
    }

    public void AllocateFunding(decimal amount)
    {
        Funding += amount;
    }

    public string DisplayFunding()
    {
        return $"{Name} has been allocated ${Funding} in funding.";
    }

    public string DisplayEvents()
    {
        return Events.Count == 0 ? $"No events registered for {Name}." : $"Events for {Name}: {string.Join(", ", Events)}";
    }
}

public class ClubManagementSystem
{
    private Dictionary<string, Society> societies = new Dictionary<string, Society>();

    public void RegisterSociety(string name)
    {
        if (societies.ContainsKey(name))
        {
            Console.WriteLine($"Society '{name}' is already registered.");
        }
        else
        {
            societies[name] = new Society(name);
            Console.WriteLine($"Society '{name}' has been registered.");
        }
    }

    public void RegisterEvent(string societyName, string eventName)
    {
        if (societies.TryGetValue(societyName, out Society society))
        {
            society.AddEvent(eventName);
            Console.WriteLine($"Event '{eventName}' registered for society '{societyName}'.");
        }
        else
        {
            Console.WriteLine($"Society '{societyName}' not found.");
        }
    }

    public void AllocateFunding(string societyName, decimal amount)
    {
        if (societies.TryGetValue(societyName, out Society society))
        {
            society.AllocateFunding(amount);
            Console.WriteLine($"Allocated ${amount} to society '{societyName}'.");
        }
        else
        {
            Console.WriteLine($"Society '{societyName}' not found.");
        }
    }

    public void DisplaySocieties()
    {
        if (societies.Count == 0)
        {
            Console.WriteLine("No societies registered.");
        }
        else
        {
            Console.WriteLine("Registered Societies:");
            foreach (var name in societies.Keys)
            {
                Console.WriteLine(name);
            }
        }
    }

    public void DisplaySocietyFunding(string societyName)
    {
        if (societies.TryGetValue(societyName, out Society society))
        {
            Console.WriteLine(society.DisplayFunding());
        }
        else
        {
            Console.WriteLine($"Society '{societyName}' not found.");
        }
    }

    public void DisplayEventsForSociety(string societyName)
    {
        if (societies.TryGetValue(societyName, out Society society))
        {
            Console.WriteLine(society.DisplayEvents());
        }
        else
        {
            Console.WriteLine($"Society '{societyName}' not found.");
        }
    }
}

class Program
{
    static void Main()
    {
        var cms = new ClubManagementSystem();

        while (true)
        {
            Console.WriteLine("\n1. Register Society\n2. Register Event\n3. Allocate Funding\n4. Display Societies\n5. Display Society Funding\n6. Display Events for Society\n7. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter society name: ");
                    string name = Console.ReadLine();
                    cms.RegisterSociety(name);
                    break;
                case "2":
                    Console.Write("Enter society name: ");
                    string societyName = Console.ReadLine();
                    Console.Write("Enter event name: ");
                    string eventName = Console.ReadLine();
                    cms.RegisterEvent(societyName, eventName);
                    break;
                case "3":
                    Console.Write("Enter society name: ");
                    societyName = Console.ReadLine();
                    Console.Write("Enter funding amount: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                    {
                        cms.AllocateFunding(societyName, amount);
                    }
                    else
                    {
                        Console.WriteLine("Invalid amount.");
                    }
                    break;
                case "4":
                    cms.DisplaySocieties();
                    break;
                case "5":
                    Console.Write("Enter society name: ");
                    societyName = Console.ReadLine();
                    cms.DisplaySocietyFunding(societyName);
                    break;
                case "6":
                    Console.Write("Enter society name: ");
                    societyName = Console.ReadLine();
                    cms.DisplayEventsForSociety(societyName);
                    break;
                case "7":
                    Console.WriteLine("Exiting...");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}