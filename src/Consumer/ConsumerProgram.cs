using System;
using MassTransit;

namespace MTRMQInterop.Consumer
{
    public class ConsumerProgram
    {
        public static void Main(string[] args)
        {
            IServiceBus serviceBus = ServiceBusFactory.New(sbc =>
            {
                sbc.UseRabbitMq();
                sbc.ReceiveFrom("rabbitmq://localhost/MTRMQInterop.Consumer");
                sbc.Subscribe(c => c.Consumer<Consumer>());
            });
            Console.ReadKey();
        }
    }
}
