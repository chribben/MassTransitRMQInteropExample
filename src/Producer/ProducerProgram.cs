using System.Text;
using MTTests.Producer.utils;
using Messages;
using RabbitMQ.Client;

namespace MTRMQInterop.Producer
{
    public class ProducerProgram
    {
        const string message = "{\"destinationAddress\": \"rabbitmq://localhost/Messages:ISomeMessage\",\"headers\": {},\"message\": {\"Message\":\"Here's the message!\"},\"messageType\": [\"urn:message:Messages:ISomeMessage\"],\"retryCount\": 0,\"sourceAddress\": \"rabbitmq://localhost/MTRMQInterop.Producer\"}";
        public static void Main(string[] args)
        {
            var connectionFactory = new ConnectionFactory();
            var connection = connectionFactory.CreateConnection();
            var channel = connection.CreateModel();

            var name = new RabbitMqMessageNameFormatter().GetMessageName(typeof(ISomeMessage));
            channel.ExchangeDeclare(name, ExchangeType.Fanout, true);

            byte[] messageBodyBytes = Encoding.UTF8.GetBytes(message.ToCharArray());
            IBasicProperties props = channel.CreateBasicProperties();
            props.ContentType = "text/plain";
            props.DeliveryMode = 2;
            channel.BasicPublish(name, "", true, false, props, messageBodyBytes);
        }

    }
}
