using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Model.Entities;

namespace UserService.Model
{
    public interface IUserContext
    {
        IMongoCollection<User> Users { get; }
    }
}
