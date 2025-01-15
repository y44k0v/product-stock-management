using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using Pastel;



namespace ProductStockManagement {
    class Program {
        static void Main(string[] args) {
            List<Product> inventory = new List<Product>();

            while (true) {
                Console.WriteLine("\n***** Product Stock Management *****".Pastel(Color.Green));
                Console.WriteLine("1. Add New Product");
                Console.WriteLine("2. Update Stock");
                Console.WriteLine("3. View All Products");
                Console.WriteLine("4. Remove Product");
                Console.WriteLine("5. Exit");
                Console.Write("Select an option: ".Pastel(Color.Yellow));

                string choice = Console.ReadLine() ?? "";

                switch (choice) {
                    case "1":
                        AddProduct(inventory);
                        break;
                    case "2":
                        UpdateStock(inventory);
                        break;
                    case "3":
                        ViewProducts(inventory);
                        break;
                    case "4":
                        RemoveProduct(inventory);
                        break;
                    case "5":
                        Console.WriteLine("Exiting... Goodbye!".Pastel(Color.Green));
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.".Pastel(Color.Purple));
                        break;
                }
            }
        }

        static void AddProduct(List<Product> inventory) {
            Console.Write("Enter product name: ".Pastel(Color.Purple));
            string name = Console.ReadLine() ?? "";

            Console.Write("Enter product price: ".Pastel(Color.Purple));
            if (!decimal.TryParse(Console.ReadLine(), out decimal price)) {
                Console.WriteLine("Invalid price. Product not added.".Pastel(Color.Red));
                return;
            }

            Console.Write("Enter stock quantity: ".Pastel(Color.Purple));
            if (!int.TryParse(Console.ReadLine(), out int stock)) {
                Console.WriteLine("Invalid stock quantity. Product not added.".Pastel(Color.Red));
                return;
            }

            inventory.Add(new Product { Name = name, Price = price, Stock = stock });
            Console.WriteLine("Product added successfully!".Pastel(Color.Green));
        }

        static void UpdateStock(List<Product> inventory) {
            Console.Write("Enter product name: ");
            string name = Console.ReadLine() ?? "";

            Product? product = inventory.Find(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (product == null) {
                Console.WriteLine("Product not found.".Pastel(Color.Red));
                return;
            }

            Console.Write("Enter stock change (positive to restock, negative to sell): ".Pastel(Color.Purple));
            if (!int.TryParse(Console.ReadLine(), out int stockChange)) {
                Console.WriteLine("Invalid stock change.".Pastel(Color.Red));   
                return;
            }

            product.Stock += stockChange;

            if (product.Stock < 0) {
                product.Stock = 0;
                Console.WriteLine("Stock cannot be negative. Set to 0.".Pastel(Color.Red));
            }

            Console.WriteLine("Stock updated successfully!".Pastel(Color.Green));
        }

        static void ViewProducts(List<Product> inventory) {
            if (inventory.Count == 0) {
                Console.WriteLine("No products in inventory.".Pastel(Color.Red));
                return;
            }
            // Set the culture to a specific culture, e.g., "en-US" for US dollars
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            Console.WriteLine("\n***** Product List *****".Pastel(Color.Blue));
            foreach (var product in inventory) {
                Console.WriteLine($"Name: {product.Name}, Price: {product.Price:C}, Stock: {product.Stock}".Pastel(Color.Pink));
            }
        }

        static void RemoveProduct(List<Product> inventory) {
            Console.Write("Enter product name to remove: ");
            string name = Console.ReadLine() ?? "";

            Product? product = inventory.Find(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (product == null) {
                Console.WriteLine("Product not found.");
                return;
            }

            inventory.Remove(product);
            Console.WriteLine("Product removed successfully!");
        }
    }
}