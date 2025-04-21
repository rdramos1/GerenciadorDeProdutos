using GerenciadorProdutos.Entities;
using GerenciadorProdutos.Service;

namespace GerenciadorProdutos {
    class Screen {


        private static Inventory inventory;
        private static SaleRecorder saleRecorder;
        private static bool Running = true;
        private static bool ManageInventoryLoop;
        private static bool SaleOptionsLoop;

        public static void start(Inventory _inventory, SaleRecorder _saleRecorder) {
            inventory = _inventory;
            saleRecorder = _saleRecorder;

            Console.WriteLine("Welcome to the Product Manager!");
            while (Running) {
                try {
                    menu();
                } catch (Exception ex) {
                    Console.WriteLine(ex.ToString());
                    Console.ReadLine();
                }
            }

        }
        private static void menu() {
            Console.WriteLine("1. Sales options");
            Console.WriteLine("2. List Product");
            Console.WriteLine("3. Manage inventory");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");
            string option = Console.ReadLine();
            Console.Clear();
            switch (option) {
                case "1":
                    SaleOptions();
                    PressEnterToContinue();
                    break;
                case "2":
                    ListProducts();
                    PressEnterToContinue();
                    break;
                case "3":
                    ManageInventoryLoop = true;
                    ManageInventory();
                    break;
                case "4":
                    Running = false;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Error.WriteLine("Invalid option, please try again.");
                    Console.ResetColor();
                    PressEnterToContinue();
                    break;
            }
        }
        private static void SaleOptions() {
            SaleOptionsLoop = true;
            while (SaleOptionsLoop) {
                Console.Clear();
                Console.WriteLine("Sales Options");
                Console.WriteLine("1. New Sale");
                Console.WriteLine("2. List Sales");
                Console.WriteLine("3. Undo sale");
                Console.WriteLine("4. Back to Main Menu");
                Console.Write("Choose an option: ");
                string option = Console.ReadLine();
                Console.Clear();
                switch (option) {
                    case "1":
                        NewSale();
                        PressEnterToContinue();
                        break;
                    case "2":
                        saleRecorder.ListSales();
                        PressEnterToContinue();
                        break;
                    case "3":
                        UndoSale();
                        PressEnterToContinue();
                        break;
                    case "4":
                        SaleOptionsLoop = false;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Error.WriteLine("Invalid option, please try again.");
                        Console.ResetColor();
                        PressEnterToContinue();
                        break;
                }
            }

        }
        private static void ListProducts() {
            Console.WriteLine(inventory.GetProductsByCategory());
        }
        private static void NewSale() {
            Console.WriteLine("What product do you want to sell?");
            int productId = GetProductId();
            Product product = inventory.Products.FirstOrDefault(p => p.Id == productId);
            Console.WriteLine("Product found: " + product.Name);
            Console.WriteLine("Product ID: " + product.Id);
            Console.WriteLine("Product Quantity: " + product.Quantity);
            Console.WriteLine("Product Price: " + product.Price);

            Console.WriteLine("How many do you want to sell?");
            int quantity;
            while (!int.TryParse(Console.ReadLine(), out quantity)) {
                Console.Write("Invalid input. Please enter a valid quantity: ");
            }
            double price = product.Price * quantity;
            Console.WriteLine("You are selling " + quantity + " of " + product.Name + "for te price: " + price.ToString("F2"));
            Console.WriteLine("Are you sure? (y/n)");
            string confirm = Console.ReadLine();
            if (confirm.ToLower() != "y") {
                Console.WriteLine("Sale cancelled.");
                return;
            }
            saleRecorder.RegisterSale(product, quantity);
            inventory.SellProduct(productId, quantity);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Sale registered successfully!");
            Console.ResetColor();

        }
        private static void UndoSale() {
            Console.WriteLine("What sale do you want to undo?");
            int saleId = GetSaleId();

            Sale sale = saleRecorder.Sales.FirstOrDefault(s => s.Id == saleId);

            Console.WriteLine("You are undoing the sale of " + sale.Product.Name + " for the price: " + (sale.Product.Price * sale.Quantity).ToString("F2"));
            Console.WriteLine("Are you sure? (y/n)");
            string confirm = Console.ReadLine();
            if (confirm.ToLower() != "y") {
                Console.WriteLine("Sale undo cancelled.");
                return;
            }

            inventory.RestockProduct(sale.Product.Id, sale.Quantity);
            saleRecorder.DeleteSale(sale);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Sale undone successfully!");
            Console.ResetColor();
        }

        private static void ManageInventory() {
            while (ManageInventoryLoop) {
                Console.Clear();
                Console.WriteLine("Manage Inventory");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Update Product");
                Console.WriteLine("3. Back to Main Menu");
                Console.Write("Choose an option: ");
                string option = Console.ReadLine();
                Console.Clear();
                switch (option) {
                    case "1":
                        AddProduct();
                        PressEnterToContinue();
                        break;
                    case "2":
                        UpdateProduct();
                        PressEnterToContinue();
                        break;
                    case "3":
                        ManageInventoryLoop = false;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Error.WriteLine("Invalid option, please try again.");
                        Console.ResetColor();
                        PressEnterToContinue();
                        break;
                }
            }
        }

        private static void AddProduct() {
            Console.WriteLine("Add Product");
            Console.Write("Enter product name: ");
            string name = Console.ReadLine();
            Console.Write("Enter product quantity: ");
            int quantity;
            while (!int.TryParse(Console.ReadLine(), out quantity)) {
                Console.Write("Invalid input. Please enter a valid quantity: ");
            }
            Console.Write("Enter product price: ");
            double price;
            while (!double.TryParse(Console.ReadLine(), out price)) {
                Console.Write("Invalid input. Please enter a valid price: ");
            }
            
            int categoryId = GetCategoryId();

            inventory.AddProductByCategoryId(name, quantity, price, categoryId);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Product added successfully!");
            Console.ResetColor();
        }

        private static void UpdateProduct() {
            Console.WriteLine("What product do you want to change?");
            int productId = GetProductId();
            Console.Clear();

            Product product = inventory.Products.FirstOrDefault(p => p.Id == productId);

            Console.WriteLine("What do you want to change?");
            Console.WriteLine("1. Name");
            Console.WriteLine("2. Quantity");
            Console.WriteLine("3. Price");
            Console.WriteLine("4. Category");
            Console.WriteLine("5. Back to Manage Inventory");
            Console.Write("Choose an option: ");
            string option = Console.ReadLine();
            Console.Clear();
            switch (option) {
                case "1":
                    Console.Write("Enter new name: ");
                    string newName = Console.ReadLine();
                    inventory.UpdateProduct(productId, newName, product.Quantity, product.Price, product.Category);
                    break;
                case "2":
                    Console.Write("Enter new quantity: ");
                    int newQuantity;
                    while (!int.TryParse(Console.ReadLine(), out newQuantity)) {
                        Console.Write("Invalid input. Please enter a valid quantity: ");
                    }
                    inventory.UpdateProduct(productId, product.Name, newQuantity, product.Price, product.Category);
                    break;
                case "3":
                    Console.Write("Enter new price: ");
                    double newPrice;
                    while (!double.TryParse(Console.ReadLine(), out newPrice)) {
                        Console.Write("Invalid input. Please enter a valid price: ");
                    }
                    inventory.UpdateProduct(productId, product.Name, product.Quantity, newPrice, product.Category);
                    break;
                case "4":
                    int categoryId = GetCategoryId();
                    Category category = inventory.Categories.FirstOrDefault(c => c.Id == categoryId);
                    inventory.UpdateProduct(productId, product.Name, product.Quantity, product.Price, category);
                    break;
                case "5":
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Error.WriteLine("Invalid option, please try again.");
                    Console.ResetColor();
                    PressEnterToContinue();
                    break;
            }

            

        }

        private static int GetCategoryId() {
            int categoryId;
            while (true) {
                Console.Write("Enter category ID (or press Enter to list all categories): ");
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input)) {
                    Console.WriteLine("Available Categories:");
                    Console.WriteLine(inventory.GetCategories());
                }
                else if (int.TryParse(input, out categoryId)) {
                    if (inventory.Categories.Any(c => c.Id == categoryId)) {
                        return categoryId;
                    }
                    else {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid category ID. Please try again.");
                        Console.ResetColor();
                    }
                }
                else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please enter a valid category ID.");
                    Console.ResetColor();
                }
            }
        }
        private static int GetProductId() {
            int productId;
            while (true) {
                Console.Write("Enter product ID (or press Enter to list all products): ");
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) {
                    Console.WriteLine("Available Products:");
                    Console.WriteLine(inventory.GetProducts());
                }
                else if (int.TryParse(input, out productId)) {
                    if (inventory.Products.Any(p => p.Id == productId)) {
                        return productId;
                    }
                    else {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid product ID. Please try again.");
                        Console.ResetColor();
                    }
                }
                else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please enter a valid product ID.");
                    Console.ResetColor();
                }
            }
        }
        private static int GetSaleId() {
            int saleId;
            while (true) {
                Console.Write("Enter sale ID (or press Enter to list all sales): ");
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) {
                    Console.WriteLine("Available Sales:");
                    saleRecorder.ListSales();
                }
                else if (int.TryParse(input, out saleId)) {
                    if (saleRecorder.Sales.Any(s => s.Id == saleId)) {
                        return saleId;
                    }
                    else {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid sale ID. Please try again.");
                        Console.ResetColor();
                    }
                }
                else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please enter a valid sale ID.");
                    Console.ResetColor();
                }
            }
        }
        static void PressEnterToContinue() {
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Press Enter to continue...");
            Console.ResetColor();
            Console.ReadLine();
            Console.Clear();
        }

    }
}
