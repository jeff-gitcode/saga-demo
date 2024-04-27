using Rebus.Bus;
using Rebus.Handlers;
using Rebus.Sagas;

namespace SagaDemo
{
    public class OrderSaga : Saga<OrderSagaData>,
    IAmInitiatedBy<PlaceOrderCommand>,
    IHandleMessages<CompletePaymentCommand>
    {
        private readonly IBus _bus;

        public OrderSaga(IBus bus)
        {
            _bus = bus;
        }

        protected override void CorrelateMessages(ICorrelationConfig<OrderSagaData> config)
        {
            config.Correlate<PlaceOrderCommand>(message => message.OrderId, data => data.OrderId);
            config.Correlate<CompletePaymentCommand>(message => message.OrderId, data => data.OrderId);
        }
        public async Task Handle(PlaceOrderCommand message)
        {
            // Process order placement logic
            Console.WriteLine($"Order placed: {message.OrderId}");

            // Initiating the payment
            await _bus.Send(new InitiatePaymentCommand { OrderId = message.OrderId });
            // Save saga data
            Data.OrderId = message.OrderId;
            Data.Status = "OrderPlaced";
        }
        public async Task Handle(CompletePaymentCommand message)
        {
            // Process payment completion logic
            Console.WriteLine($"Payment completed for order: {message.OrderId}");
            // Save saga data
            Data.Status = "PaymentCompleted";
            // Mark the saga as complete
            MarkAsComplete();
        }
    }
}