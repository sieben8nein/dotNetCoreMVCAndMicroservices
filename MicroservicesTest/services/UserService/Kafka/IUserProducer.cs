﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Kafka
{
    public interface IUserProducer
    {
        void produce(string userName);
    }
}
