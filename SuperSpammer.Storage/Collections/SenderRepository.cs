using MongoDB.Driver;
using SuperSpammer.Common;
using SuperSpammer.Infastructure;
using SuperSpammer.Storage.Infrastructure;

namespace SuperSpammer.Storage.Collections;

public class SenderRepository : ISenderRepository
{
    public SenderRepository(IMongoRepository repository)
    {
        _collection = repository.GetCollection<SenderDto>("senders");
    }

    public async Task<IEnumerable<SenderDto>> GetAll()
    {
        var senders = await _collection.Find(_ => true).ToListAsync();
        return senders;
    }

    readonly IMongoCollection<SenderDto> _collection;
}