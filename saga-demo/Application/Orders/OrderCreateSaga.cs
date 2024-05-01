using Rebus.Bus;
using Rebus.Handlers;
using Rebus.Sagas;

namespace saga_demo.Application.Orders
{
    public class OrderCreateSagaData : ISagaData
    {
        public Guid Id { get; set; }
        public int Revision { get; set; }
        public Guid OrderId { get; set; }
        public bool ConfirmationEmailSent { get; set; }

        public bool PaymentRequestSent { get; set; }
    }

    public class OrderCreateSaga : Saga<OrderCreateSagaData>,
        IAmInitiatedBy<OrderCreatedEvent>,
        IHandleMessages<OrderConfirmationEmailSentEvent>,
        IHandleMessages<PaymentRequestSentEvent>
    {
        private readonly IBus _bus;

        public OrderCreateSaga(IBus bus)
        {
            _bus = bus;
        }

        protected override void CorrelateMessages(ICorrelationConfig<OrderCreateSagaData> config)
        {
            config.Correlate<OrderCreatedEvent>(m => m.OrderId, d => d.OrderId);
            config.Correlate<OrderConfirmationEmailSentEvent>(m => m.OrderId, d => d.OrderId);
            config.Correlate<PaymentRequestSentEvent>(m => m.OrderId, d => d.OrderId);
        }

        public async Task Handle(OrderCreatedEvent message)
        {
            if (!IsNew)
            {
                return;
            }

            await _bus.Send(new SendOrderConfirmationEmailCommand(message.OrderId));
        }

        public async Task Handle(OrderConfirmationEmailSentEvent message)
        {
            Data.ConfirmationEmailSent = true;

            await _bus.Send(new SendPaymentRequestCommand(Data.OrderId));
        }


        public Task Handle(PaymentRequestSentEvent message)
        {
            Data.PaymentRequestSent = true;

            MarkAsComplete();

            return Task.CompletedTask;
        }

    }
}