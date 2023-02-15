using ApiServiceBus.Interfaces;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Amqp.Framing;
using System.Text;

namespace ApiServiceBus.Services
{
    public class ServiceTopicBus : IServiceTopicBus
    {
        private readonly ServiceBusClient _client;
        const string topicName = "facturacion-topic";
        const string subscriptionName = "facturacion-subscript";

        public ServiceTopicBus(string keys)
        {
            _client = new ServiceBusClient(keys);
        }

        public async Task SendMessageAsync(string messageBody)
        {
            ServiceBusSender _sender = _client.CreateSender("facturacion-topic");
            var message = new ServiceBusMessage(Encoding.UTF8.GetBytes(messageBody));
            await _sender.SendMessageAsync(message);
        }

        public async Task<List<string>> ReceiveMessageAsync()
        {
            List<string> result = new List<string>();

            ServiceBusReceiver _receiver = _client.CreateReceiver(topicName, subscriptionName, new ServiceBusReceiverOptions { ReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete });
            ServiceBusReceivedMessage message = await _receiver.ReceiveMessageAsync();

            while (message != null)
            {
                string messageBody = Encoding.UTF8.GetString(message.Body);
                result.Add(messageBody);
                message = await _receiver.ReceiveMessageAsync();
            }

            return result;
        }

    }
}
