using MongoDB.Driver;
using MongoDB.Bson;

    public class MongoDBService
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;



        public MongoDBService(IConfiguration configuration)
        {
            _client = new MongoClient(configuration.GetConnectionString("MongoDB"));
            _database = _client.GetDatabase(configuration["Carros"]);

        }


        public async Task<List<Carro>> ObterTodos()
        {
            var collection = _database.GetCollection<Carro>("Carros");
            return await collection.Find(_ => true).ToListAsync();
        }

        public async Task<Carro> ObterPorId(string id)
        {
            var collection = _database.GetCollection<Carro>("Carros");
            var objetoId = new ObjectId(id);
            return await collection.Find(x => x.Id == objetoId).FirstOrDefaultAsync();
        }

        public async Task Iniciar(Carro carro)
        {
            var collection = _database.GetCollection<Carro>("Carros");
            await collection.InsertOneAsync(carro);
        }

        public async Task Load(Carro carro)
        {
            var collection = _database.GetCollection<Carro>("Carros");
            await collection.ReplaceOneAsync(x => x.Id == carro.Id, carro);
        }

        public async Task Remover(string id)
        {
            var collection = _database.GetCollection<Carro>("Carros");
            var objetoId = new ObjectId(id);
            await collection.DeleteOneAsync(x => x.Id == objetoId);
        }
    }
            