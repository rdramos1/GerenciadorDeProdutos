using GerenciadorProdutos.Entities;
using Newtonsoft.Json;

namespace GerenciadorProdutos.Repositories {
    public class JsonRepository<AppData> : IRepository<AppData> {
        private readonly string _filePath;
        public JsonRepository(string filePath) {
            _filePath = filePath;
        }
        public AppData GetAll() {
            if (!File.Exists(_filePath)) {
                return default;
            }
            var json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<AppData>(json);
        }

        public void SaveAll(AppData data) {
            var settings = new JsonSerializerSettings {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };
            var json = JsonConvert.SerializeObject(data, settings);
            File.WriteAllText(_filePath, json);

        }
    }
}

