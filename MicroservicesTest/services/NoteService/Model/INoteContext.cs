using MongoDB.Driver;
using NoteService.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteService.Model
{
    public interface INoteContext
    {
        IMongoCollection<Note> Notes { get; }
    }
}
