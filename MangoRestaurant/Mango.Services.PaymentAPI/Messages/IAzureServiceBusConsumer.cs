using Azure.Messaging.ServiceBus;

namespace Mango.Services.PaymentAPI.Messages
{
    public interface IAzureServiceBusConsumer
    {
        Task OnCheckOutMessageReceived(ProcessMessageEventArgs args);
    }
}
