using System;
using System.Collections.Generic;
using System.Linq;

// Domain Models
class Property
{
    public int Id { get; set; }
    public int PropertyManagerRef { get; set; }
    public string Name { get; set; }
    public string Area { get; set; }
    public decimal RentalPrice { get; set; }
    public decimal CurrentValue { get; set; }
    public bool Occupied { get; set; }
}

class PropertyManager
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
}

// Repository
class PropertyManagementRepository
{
    private List<Property> Properties = new List<Property>();
    private List<PropertyManager> Managers = new List<PropertyManager>();
    private int propertyCounter = 1;
    private int managerCounter = 1;

    public PropertyManagementRepository()
    {
        SeedData();
    }

    private void SeedData()
    {
        Managers.Add(new PropertyManager { Id = managerCounter++, Name = "Solomon", Password = "67839!" });
        Managers.Add(new PropertyManager { Id = managerCounter++, Name = "Arjun", Password = "78658$" });
        
        Properties.Add(new Property { Id = propertyCounter++, PropertyManagerRef = 1, Name = "Adarsh Residency", Area = "JP Nagar", RentalPrice = 55000, CurrentValue = 60000, Occupied = false });
        Properties.Add(new Property { Id = propertyCounter++, PropertyManagerRef = 1, Name = "Jains Prakriti", Area = "Jaya Nagar", RentalPrice = 45000, CurrentValue = 40000, Occupied = false });
        Properties.Add(new Property { Id = propertyCounter++, PropertyManagerRef = 2, Name = "Saaya Serene", Area = "Panduranga Nagar", RentalPrice = 75000, CurrentValue = 70000, Occupied = true });
        Properties.Add(new Property { Id = propertyCounter++, PropertyManagerRef = 2, Name = "Ranka Colony", Area = "BTM Layout", RentalPrice = 125000, CurrentValue = 100000, Occupied = false });
    }

    public IEnumerable<Property> GetAllProperties() => Properties;
    public Property GetLowestValueProperty() => Properties.OrderBy(p => p.CurrentValue).FirstOrDefault();
    public IEnumerable<Property> GetOccupiedProperties() => Properties.Where(p => p.Occupied);
    public IEnumerable<Property> GetPropertiesByArea(string area) => Properties.Where(p => p.Area.Equals(area, StringComparison.OrdinalIgnoreCase));
    public PropertyManager AuthenticateManager(string name, string password) => Managers.FirstOrDefault(m => m.Name == name && m.Password == password);
    public decimal CalculateManagerSalary(int managerId) => Properties.Where(p => p.PropertyManagerRef == managerId).Sum(p => p.RentalPrice) * 0.1m;
    
    public void AddProperty(string name, string area, decimal rentalPrice, decimal currentValue, bool occupied, string managerName)
    {
        var manager = Managers.FirstOrDefault(m => m.Name == managerName);
        if (manager == null)
        {
            manager = new PropertyManager { Id = managerCounter++, Name = managerName, Password = "default123" };
            Managers.Add(manager);
            Console.WriteLine("New Property Manager created with default password.");
        }
        Properties.Add(new Property { Id = propertyCounter++, PropertyManagerRef = manager.Id, Name = name, Area = area, RentalPrice = rentalPrice, CurrentValue = currentValue, Occupied = occupied });
    }
}

// Entry Point
class Program
{
    static void Main()
    {
        var repo = new PropertyManagementRepository();
        while (true)
        {
            Console.WriteLine("\nProperty Management System");
            Console.WriteLine("1. View All Properties");
            Console.WriteLine("2. View Lowest Value Property");
            Console.WriteLine("3. View Occupied Properties");
            Console.WriteLine("4. View Properties by Area");
            Console.WriteLine("5. Add New Property");
            Console.WriteLine("6. Calculate Manager Salary");
            Console.WriteLine("7. Exit");
            Console.Write("Enter Choice: ");
            
            switch (Console.ReadLine())
            {
                case "1":
                    foreach (var p in repo.GetAllProperties())
                        Console.WriteLine($"{p.Name} in {p.Area}, Rental: {p.RentalPrice}, Value: {p.CurrentValue}, Occupied: {p.Occupied}");
                    break;
                case "2":
                    var lowest = repo.GetLowestValueProperty();
                    Console.WriteLine($"Lowest Value Property: {lowest.Name} in {lowest.Area}, Value: {lowest.CurrentValue}");
                    break;
                case "3":
                    foreach (var p in repo.GetOccupiedProperties())
                        Console.WriteLine($"Occupied: {p.Name} in {p.Area}");
                    break;
                case "4":
                    Console.Write("Enter Area: ");
                    string area = Console.ReadLine();
                    foreach (var p in repo.GetPropertiesByArea(area))
                        Console.WriteLine($"{p.Name} in {p.Area}, Rental: {p.RentalPrice}");
                    break;
                case "5":
                    Console.Write("Property Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Area: ");
                    string propArea = Console.ReadLine();
                    Console.Write("Rental Price: ");
                    decimal rentalPrice = Convert.ToDecimal(Console.ReadLine());
                    Console.Write("Current Value: ");
                    decimal currentValue = Convert.ToDecimal(Console.ReadLine());
                    Console.Write("Occupied (true/false): ");
                    bool occupied = Convert.ToBoolean(Console.ReadLine());
                    Console.Write("Manager Name: ");
                    string managerName = Console.ReadLine();
                    repo.AddProperty(name, propArea, rentalPrice, currentValue, occupied, managerName);
                    break;
                case "6":
                    Console.Write("Manager Name: ");
                    string mName = Console.ReadLine();
                    Console.Write("Password: ");
                    string mPassword = Console.ReadLine();
                    var manager = repo.AuthenticateManager(mName, mPassword);
                    if (manager != null)
                        Console.WriteLine($"Manager {mName}'s Salary: {repo.CalculateManagerSalary(manager.Id)}");
                    else
                        Console.WriteLine("Invalid credentials");
                    break;
                case "7":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }
}
