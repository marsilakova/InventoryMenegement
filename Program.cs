using System;
using System.Collections.Generic;
using System.IO;

namespace InventoryManagement
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }

        public Product(int id, string name, int quantity, decimal price, string category, string description)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            Price = price;
            Category = category;
            Description = description;
        }
    }

    class Program
    {
        static List<Product> products = new List<Product>();
        static int currentId = 1;
        static string filePath = "inventory.txt";

        static void Main(string[] args)
        {
            // 1. Läs in data från filen först (om filen existerar)
            LoadData();

            // 2. Om filen var tom eller inte fanns, lägg till standardprodukter så att listan inte är tom
            if (products.Count == 0)
            {
                products.Add(new Product(currentId++, "Laptop", 10, 8000.00m, "Electronics", "15-inch laptop"));
                products.Add(new Product(currentId++, "Mouse", 50, 299.00m, "Electronics", "Wireless mouse"));
            }

            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("===== INVENTORY MANAGEMENT =====");
                Console.WriteLine("1. Add product");
                Console.WriteLine("2. View all products");
                Console.WriteLine("3. Update product");
                Console.WriteLine("4. Delete product");
                Console.WriteLine("5. Show total value");
                Console.WriteLine("6. Sort by category");
                Console.WriteLine("7. Save data");
                Console.WriteLine("8. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine() ??"";

                switch (choice)
                {
                    case "1":
                        AddProduct();
                        break;
                    case "2":
                        ViewAllProducts(products);
                        break;
                    case "3":
                        UpdateProduct();
                        break;
                    case "4":
                        DeleteProduct();
                        break;
                    case "5":
                        ShowTotalValue();
                        break;
                    case "6":
                        SortByCategory();
                        break;
                    case "7":
                        SaveData();
                        break;
                    case "8":
                        running = false;
                        Console.WriteLine("\nProgrammet avslutas. Hejdå!");
                        break;
                    default:
                        Console.WriteLine("\nOgiltigt val, försök igen.");
                        WaitForKey();
                        break;
                }
            }
        }

        // Automatisk dataladdning från filen vid start
        static void LoadData()
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            try
            {
                products.Clear();
                int maxId = 0;

                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    string[] parts = line.Split(';');
                    if (parts.Length == 6)
                    {
                        int id = int.Parse(parts[0]);
                        string name = parts[1];
                        int quantity = int.Parse(parts[2]);
                        decimal price = decimal.Parse(parts[3], System.Globalization.CultureInfo.InvariantCulture);
                        string category = parts[4];
                        string description = parts[5];

                        products.Add(new Product(id, name, quantity, price, category, description));

                        if (id > maxId)
                        {
                            maxId = id;
                        }
                    }
                }
                currentId = maxId + 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ett fel uppstod när data skulle laddas: {ex.Message}");
                WaitForKey();
            }
        }

        static void ViewAllProducts(List<Product> listToDisplay)
        {
            Console.Clear();
            Console.WriteLine("=== ALLA PRODUKTER I LAGER ===\n");

            if (listToDisplay.Count == 0)
            {
                Console.WriteLine("Lagret är tomt.");
            }
            else
            {
                Console.WriteLine($"{"ID",-5} {"Namn",-20} {"Antal",-10} {"Pris",-12} {"Kategori",-15}");
                Console.WriteLine(new string('-', 65));

                foreach (var p in listToDisplay)
                {
                    Console.WriteLine($"{p.Id,-5} {p.Name,-20} {p.Quantity,-10} {p.Price,-12:C} {p.Category,-15}");
                }
            }
            WaitForKey();
        }

        static void AddProduct()
        {
            Console.Clear();
            Console.WriteLine("=== LÄGG TILL NY PRODUKT ===\n");

            Console.Write("Ange namn: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Ange antal: ");
            int quantity = int.Parse(Console.ReadLine()!);

            Console.Write("Ange pris: ");
            decimal price = decimal.Parse(Console.ReadLine()!, System.Globalization.CultureInfo.InvariantCulture);

            Console.Write("Ange kategori: ");
            string category = Console.ReadLine() ?? "";

            Console.Write("Ange beskrivning: ");
            string description = Console.ReadLine() ?? "";

            products.Add(new Product(currentId++, name, quantity, price, category, description));
            Console.WriteLine("\nProdukten har lagts till!");
            WaitForKey();
        }

        static void UpdateProduct()
        {
            Console.Clear();
            Console.WriteLine("=== UPPDATERA PRODUKT ===");
            Console.Write("Ange ID på produkten: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) return;

            Product p = products.Find(prod => prod.Id == id);
            if (p == null)
            {
                Console.WriteLine("Produkten hittades inte.");
            }
            else
            {
                Console.Write($"Nytt antal (nuvarande: {p.Quantity}): ");
                string qInput = Console.ReadLine() ??"";
                if (!string.IsNullOrEmpty(qInput)) p.Quantity = int.Parse(qInput);

                Console.Write($"Nytt pris (nuvarande: {p.Price:C}): ");
                string pInput = Console.ReadLine() ??"";
                if (!string.IsNullOrEmpty(pInput)) p.Price = decimal.Parse(pInput, System.Globalization.CultureInfo.InvariantCulture);

                Console.WriteLine("\nProdukten har uppdaterats!");
            }
            WaitForKey();
        }

        static void DeleteProduct()
        {
            Console.Clear();
            Console.WriteLine("=== TA BORT PRODUKT ===");
            Console.Write("Ange ID på produkten: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) return;

            Product p = products.Find(prod => prod.Id == id) !;
            if (p != null)
            {
                products.Remove(p);
                Console.WriteLine("\nProdukten raderades.");
            }
            else
            {
                Console.WriteLine("Hittade inte ID.");
            }
            WaitForKey();
        }

        static void ShowTotalValue()
        {
            Console.Clear();
            Console.WriteLine("=== TOTALT LAGERVÄRDE ===\n");
            decimal totalValue = 0;
            foreach (var p in products)
            {
                totalValue += p.Quantity * p.Price;
            }
            Console.WriteLine($"Hela lagrets ekonomiska värde: {totalValue:C}");
            WaitForKey();
        }

        static void SortByCategory()
        {
            List<Product> sortedList = new List<Product>(products);
            sortedList.Sort((x, y) => string.Compare(x.Category, y.Category, StringComparison.OrdinalIgnoreCase));
            ViewAllProducts(sortedList);
        }

        static void SaveData()
        {
            Console.Clear();
            Console.WriteLine("=== SPARAR DATA ===\n");
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var p in products)
                    {
                        writer.WriteLine(
                        $"{p.Id};{p.Name};{p.Quantity};{p.Price.ToString(System.Globalization.CultureInfo.InvariantCulture)};{p.Category};{p.Description}");
                    }
                }
                Console.WriteLine("All data har sparats till inventory.txt!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(

                        "Ett fel uppstod när data skulle sparas: {ex.Message}");
            }
            WaitForKey();
        }

        static void WaitForKey()
        {
            Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
            Console.ReadKey();
        }
    }
}