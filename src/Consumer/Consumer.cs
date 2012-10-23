using System;
using MassTransit;
using Messages;

namespace MTRMQInterop.Consumer
{
    public class Consumer : Consumes<ISomeMessage>.All
    {
        public void Consume(ISomeMessage message)
        {
            Console.WriteLine(message.Message);
        }
    }
}