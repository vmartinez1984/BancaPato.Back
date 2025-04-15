using MongoDB.Driver;

namespace Banca.Api.Repositories
{
    public class BaseRepo
    {
        protected readonly IMongoDatabase _mongoDatabase;
        public BaseRepo(IConfiguration configurations)
        {
            var conectionString = configurations.GetConnectionString("MongoDb");
            var mongoClient = new MongoClient(conectionString);
            var nombreDeLaDb = conectionString.Split("/").Last().Split("?").First();
            _mongoDatabase = mongoClient.GetDatabase(nombreDeLaDb);
        }
    }
  
}
