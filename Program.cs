using System;
using System.Collections.Generic;

namespace ProductStockManagement {
    class Program {
        static void Main(string[] args) {
            List<Product> inventory = new List<Product>();

            while (true) {
                Console.WriteLine("\n***** Product Stock Management *****");
                Console.WriteLine("1. Add New Product");
                Console.WriteLine("2. Update Stock");
                Console.WriteLine("3. View All Products");
                Console.WriteLine("4. Remove Product");
                Console.WriteLine("5. Exit");
                Console.Write("Select an option: ");

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
                        Console.WriteLine("Exiting... Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void AddProduct(List<Product> inventory) {
            Console.Write("Enter product name: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Enter product price: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price)) {
                Console.WriteLine("Invalid price. Product not added.");
                return;
            }

            Console.Write("Enter stock quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int stock)) {
                Console.WriteLine("Invalid stock quantity. Product not added.");
                return;
            }

            inventory.Add(new Product { Name = name, Price = price, Stock = stock });
            Console.WriteLine("Product added successfully!");
        }

        static void UpdateStock(List<Product> inventory) {
            Console.Write("Enter product name: ");
            string name = Console.ReadLine() ?? "";

            Product? product = inventory.Find(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (product == null) {
                Console.WriteLine("Product not found.");
                return;
            }

            Console.Write("Enter stock change (positive to restock, negative to sell): ");
            if (!int.TryParse(Console.ReadLine(), out int stockChange)) {
                Console.WriteLine("Invalid stock change.");
                return;
            }

            product.Stock += stockChange;

            if (product.Stock < 0) {
                product.Stock = 0;
                Console.WriteLine("Stock cannot be negative. Set to 0.");
            }

            Console.WriteLine("Stock updated successfully!");
        }

        static void ViewProducts(List<Product> inventory) {
            if (inventory.Count == 0) {
                Console.WriteLine("No products in inventory.");
                return;
            }

            Console.WriteLine("\n***** Product List *****");
            foreach (var product in inventory) {
                Console.WriteLine($"Name: {product.Name}, Price: {product.Price:C}, Stock: {product.Stock}");
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

    class Product {
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}