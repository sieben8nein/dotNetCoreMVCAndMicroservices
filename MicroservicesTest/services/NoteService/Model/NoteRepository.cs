using MongoDB.Bson;
using MongoDB.Driver;
using NoteService.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteService.Model
{
    public class NoteRepository : INoteRepository
    {
        private readonly INoteContext _context;

        public NoteRepository(INoteContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Note>> GetAllNotes()
        {
            return await _context
                            .Notes
                            .Find(_ => true)
                            .ToListAsync();
        }
        public Task<Note> GetNote(long id)
        {
            FilterDefinition<Note> filter = Builders<Note>.Filter.Eq(m => m.Id, id);
            return _context
                    .Notes
                    .Find(filter)
                    .FirstOrDefaultAsync();
        }
        public async Task Create(Note note)
        {
            await _context.Notes.InsertOneAsync(note);
        }
        public async Task<bool> Update(Note note)
        {
            ReplaceOneResult updateResult =
                await _context
                        .Notes
                        .ReplaceOneAsync(
                            filter: g => g.Id == note.Id,
                            replacement: note);
            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
        public async Task<bool> Delete(long id)
        {
            FilterDefinition<Note> filter = Builders<Note>.Filter.Eq(m => m.Id, id);
            DeleteResult deleteResult = await _context
                                                .Notes
                                              .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
        public async Task<long> GetNextId()
        {
            return await _context.Notes.CountDocumentsAsync(new BsonDocument()) + 1;
        }
    }
}
