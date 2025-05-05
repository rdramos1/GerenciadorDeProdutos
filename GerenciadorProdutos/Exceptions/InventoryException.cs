using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorProdutos.Exceptions {
    public class InventoryException : Exception {
        public InventoryException(string message) : base(message) {

        }
        public override string ToString() {
            return $"CategoryException: {Message}";
        }

    }
}
