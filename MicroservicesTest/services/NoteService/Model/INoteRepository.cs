using NoteService.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteService.Model
{
    public interface INoteRepository
    {
        Task<IEnumerable<Note>> GetAllNotes();

        Task<Note> GetNote(long id);

        Task Create(Note note);

        Task<bool> Update(Note note);

        Task<bool> Delete(long id);
        Task<long> GetNextId();
    }
}
