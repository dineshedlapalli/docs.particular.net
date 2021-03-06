﻿// ReSharper disable UnusedParameter.Local

namespace Core6.Routing
{
    using NServiceBus;

    class RoutingAPIs
    {
        void LogicalRouting(TransportExtensions transportExtensions)
        {
            #region Routing-Logical

            var routing = transportExtensions.Routing();
            routing.RouteToEndpoint(
                assembly: typeof(AcceptOrder).Assembly,
                destination: "Sales");

            routing.RouteToEndpoint(
                assembly: typeof(AcceptOrder).Assembly,
                @namespace: "PriorityMessages",
                destination: "Preferred");

            routing.RouteToEndpoint(
                messageType: typeof(SendOrder),
                destination: "Sending");

            #endregion
        }

        void SubscribeRouting(TransportExtensions<MsmqTransport> transportExtensions)
        {
            #region Routing-RegisterPublisher

            var routing = transportExtensions.Routing();
            routing.RegisterPublisher(
                assembly: typeof(OrderAccepted).Assembly,
                publisherEndpoint: "Sales");

            routing.RegisterPublisher(
                assembly: typeof(OrderAccepted).Assembly,
                @namespace: "PriorityMessages",
                publisherEndpoint: "Preferred");

            routing.RegisterPublisher(
                eventType: typeof(OrderSent),
                publisherEndpoint: "Sending");
            #endregion
        }

        class AcceptOrder
        {
        }

        class SendOrder
        {
        }

        class OrderSent
        {
        }

        class OrderAccepted
        {
        }
    }
}