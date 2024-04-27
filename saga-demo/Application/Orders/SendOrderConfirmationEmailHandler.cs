using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rebus.Bus;
using Rebus.Handlers;

namespace saga_demo.Application.Orders
{
    public class SendOrderConfirmationEmailHandler : IHandleMessages<SendOrderConfirmationEmailCommand>
    {
        private ILogger<OrderCreateSaga> _logger;
        private readonly IBus _bus;

        public SendOrderConfirmationEmailHandler(ILogger<OrderCreateSaga> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        public async Task Handle(SendOrderConfirmationEmailCommand message)
        {
            _logger.LogInformation("Sending order confirmation email for order {OrderId}", message.OrderId);

            await Task.Delay(2000);

            _logger.LogInformation("Order confirmation email sent for order {OrderId}", message.OrderId);

            await _bus.Send(new OrderConfirmationEmailSentEvent(message.OrderId));
        }
    }
}