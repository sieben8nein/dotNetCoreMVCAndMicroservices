using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Model.Entities
{
    public class User
    {
        [BsonId]
        public ObjectId InternalId { get; set; }
        public long Id { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }
        public String Contact { get; set; }
    }
}
