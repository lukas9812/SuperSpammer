using MongoDB.Driver;
using SuperSpammer.Engine.Models;
using SuperSpammer.Infastructure;

namespace SuperSpammer.Storage;

public class MongoRepository : IMongoRepository
{
    public MongoRepository(MongoDbSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        _database = client.GetDatabase(settings.DatabaseName);
    }
    
    public IMongoCollection<T> GetCollection<T>(string name) 
        => _database.GetCollection<T>(name);
    
    async Task EnsureCollectionExists<T>(string name)
    {
        var collectionNames = await (await _database.ListCollectionNamesAsync()).ToListAsync();
        if (!collectionNames.Contains(name))
        {
            await _database.CreateCollectionAsync(name);
        }
    }
    
    readonly IMongoDatabase _database;
}