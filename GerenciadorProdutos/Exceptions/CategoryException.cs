using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorProdutos.Exceptions {
    class CategoryException : Exception {
        public CategoryException(string message) : base(message) {

        }
        public override string ToString() {
            return $"CategoryException: {Message}";
        }

    }
}
