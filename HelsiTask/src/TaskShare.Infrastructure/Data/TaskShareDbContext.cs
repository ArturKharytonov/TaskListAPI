using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using TaskShare.Core.Entities;

namespace TaskShare.Infrastructure.Data;

public class TaskShareDbContext
{
    private readonly IMongoDatabase _db;
    private readonly IConfiguration _configuration;

    public TaskShareDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        var dbName = _configuration.GetSection("MongoDb:DbName").Value!;
        var client = new MongoClient(connectionString);
        _db = client.GetDatabase(dbName);
    }

    public IMongoCollection<TaskList> TaskLists 
        => _db.GetCollection<TaskList>(_configuration.GetSection("MongoDb:CollectionName").Value!);
}