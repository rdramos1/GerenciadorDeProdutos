
using GerenciadorProdutos.Entities;

namespace GerenciadorProdutos {
    public class Program {
        public static void Main(string[] args) {
            Category category = new Category("Electronics", 1);
            Product product = new Product("Laptop", 1, 10, 1500.00, category);
            Console.WriteLine(product.ToString());

            try {
                product.ChangeQuantity(5);
            } catch (Exception ex) {
                Console.WriteLine(ex);
            }

            Console.WriteLine("Say Hi");
            Console.ReadLine();
        }
    }
}
