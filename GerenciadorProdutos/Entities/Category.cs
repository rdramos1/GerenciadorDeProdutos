using System.Text;
using GerenciadorProdutos.DTO;
using GerenciadorProdutos.Exceptions;

namespace GerenciadorProdutos.Entities {
    public class Category {
        public string Name { get; private set; }
        public int Id { get; private set; }
        public List<Product> Products { get; private set; }
        public Category(string name, int id) {
            Name = name;
            Id = id;
            Products = new List<Product>();
            if (string.IsNullOrEmpty(name)) {
                throw new CategoryException("Name cannot be null or empty");
            }

        }
       
        public CategoryDTO ToDTO() {
            return new CategoryDTO {
                Name = Name,
                Id = Id
            };
        }
        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Category: {Name}");
            sb.AppendLine($"ID: {Id}");
            return sb.ToString();
        }


    }
}