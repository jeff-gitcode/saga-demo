using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MediatR;

namespace saga_demo.Application.Orders
{
    public record OrderCreatedEvent(Guid OrderId);

    public record OrderConfirmationEmailSentEvent(Guid OrderId);

    public record PaymentRequestSentEvent(Guid OrderId);

    public record OrderCreateCommand(Guid OrderId): IRequest<Unit>;

    public record SendOrderConfirmationEmailCommand(Guid OrderId);

    public record SendPaymentRequestCommand(Guid OrderId);

}