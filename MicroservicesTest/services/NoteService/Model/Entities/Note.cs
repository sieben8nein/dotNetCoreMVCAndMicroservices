using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteService.Model.Entities
{
    public class Note
    {
        [BsonId]
        public ObjectId InternalId { get; set; }
        public long Id { get; set; }
        public String Title { get; set; }
        public String Topic { get; set; }
        public String Content { get; set; }
    }
}
