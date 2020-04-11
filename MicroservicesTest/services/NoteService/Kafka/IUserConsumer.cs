using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteService.Kafka
{
    public interface IUserConsumer
    {
        void Listen(Action<string> message);
    }
}
