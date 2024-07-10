using MediatR;

namespace saga_demo2.cqrs;

public record MakeCarBookingCommand(string CarBooking) : IRequest;
