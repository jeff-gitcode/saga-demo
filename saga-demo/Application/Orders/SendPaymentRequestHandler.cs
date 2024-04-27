using Rebus.Bus;
using Rebus.Handlers;

namespace saga_demo.Application.Orders
{
    public class SendPaymentRequestHandler : IHandleMessages<SendPaymentRequestCommand>
    {
        private ILogger<OrderCreateSaga> _logger;
        private readonly IBus _bus;

        public SendPaymentRequestHandler(ILogger<OrderCreateSaga> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        public async Task Handle(SendPaymentRequestCommand message)
        {
            _logger.LogInformation("Sending payment request for order {OrderId}", message.OrderId);

            await Task.Delay(2000);

            _logger.LogInformation("Payment request sent for order {OrderId}", message.OrderId);

            await _bus.Send(new PaymentRequestSentEvent(message.OrderId));
        }
    }


}