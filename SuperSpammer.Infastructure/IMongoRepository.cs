using MongoDB.Driver;

namespace SuperSpammer.Infastructure;

public interface IMongoRepository
{
    IMongoCollection<T> GetCollection<T>(string name);
}