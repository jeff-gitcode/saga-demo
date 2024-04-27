using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Rebus.Bus;

namespace saga_demo.Application.Orders
{
    public class OrderCreateCommandHandler : IRequestHandler<OrderCreateCommand, Unit>
    {
        private ILogger<OrderCreateSaga> _logger;
        private readonly IBus _bus;

        public OrderCreateCommandHandler(ILogger<OrderCreateSaga> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        public async Task<Unit> Handle(OrderCreateCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Order {OrderId} created", request.OrderId);

            await _bus.Send(new OrderCreatedEvent(request.OrderId));

            _logger.LogInformation("Order {OrderId} created event sent", request.OrderId);

            return Unit.Value;
        }
    }
}