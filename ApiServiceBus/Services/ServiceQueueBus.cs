using Azure.Messaging.ServiceBus;
using System.Diagnostics;

namespace ApiServiceBus.Services
{
    public class ServiceQueueBus
    {
        private ServiceBusClient client;
        private List<string> mensajes;

        public ServiceQueueBus(string keys)
        {
            this.client = new ServiceBusClient(keys);
            this.mensajes = new List<string>();
        }

        public async Task SendMessageAsync(string data)
        {
            ServiceBusSender sender = this.client.CreateSender("coladesarrollo");
            ServiceBusMessage message = new ServiceBusMessage(data);
            await sender.SendMessageAsync(message);
        }

        public async Task<List<string>> ReceiveMessagesAsync()
        {
            ServiceBusProcessor processor = this.client.CreateProcessor("coladesarrollo");

            processor.ProcessMessageAsync += Processor_ProcessMessageAsync;

            processor.ProcessErrorAsync += Processor_ProcessErrorAsync;

            await processor.StartProcessingAsync();

            Thread.Sleep(3000);

            await processor.StopProcessingAsync();

            return this.mensajes;
        }

        private async Task Processor_ProcessMessageAsync(ProcessMessageEventArgs arg)
        {
            string content = arg.Message.Body.ToString();

            this.mensajes.Add(content);

            await arg.CompleteMessageAsync(arg.Message);
        }

        private Task Processor_ProcessErrorAsync(ProcessErrorEventArgs arg)
        {
            Debug.WriteLine(arg.Exception.ToString());
            return Task.CompletedTask;
        }
    }
}
