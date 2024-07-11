namespace saga_demo2.Messages;

public record Message1(Guid SubscriberId, string Email);

public record Message2(Guid SubscriberId, string Email);

public record Message3(Guid SubscriberId, string Email);

public record Message0(string Email);

public record Message1Sent
{
    public Guid SubscriberId { get; init; }

    public string Email { get; init; }
}
 
public record Message2Sent
{
    public Guid SubscriberId { get; init; }

    public string Email { get; init; }
}

public record Message3Sent
{
    public Guid SubscriberId { get; init; }

    public string Email { get; init; }
}
