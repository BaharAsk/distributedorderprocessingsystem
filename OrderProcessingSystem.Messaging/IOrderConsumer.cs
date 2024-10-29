using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace OrderProcessingSystem.Messaging
{
    public interface IOrderConsumer
    {
        void ConsumeOrder();
    }
}
