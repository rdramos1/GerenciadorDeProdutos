using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciadorProdutos.Entities;

namespace GerenciadorProdutos.Repositories {
    public interface IRepository<AppData> {
        public interface IRepository<AppData> {
            public AppData GetAll();
            public void SaveAll(AppData data);
        }

    }
}
