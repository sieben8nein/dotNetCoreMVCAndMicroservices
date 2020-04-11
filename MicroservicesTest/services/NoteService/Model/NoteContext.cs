using MongoDB.Driver;
using NoteService.Config;
using NoteService.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteService.Model
{
    public class NoteContext : INoteContext
    {
        private readonly IMongoDatabase _db;

        public NoteContext(MongoDBConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            _db = client.GetDatabase(config.Database);
        }

        public IMongoCollection<Note> Notes => _db.GetCollection<Note>("Notes");
    }
}
