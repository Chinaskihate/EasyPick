using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBroker.Common;

public interface IMessageConsumer<T>
{
    Task<Message<T>> ReadAsync();
}