namespace saga_demo2.Messages;

public record SendWelcomeEmail(Guid SubscriberId, string Email);

public record SendFollowUpEmail(Guid SubscriberId, string Email);

public record OnboardingCompleted(Guid SubscriberId, string Email);

public record SubscribeToNewsletter(string Email);

public record SubscriberCreated
{
    public Guid SubscriberId { get; init; }

    public string Email { get; init; }
}
 
public record WelcomeEmailSent
{
    public Guid SubscriberId { get; init; }

    public string Email { get; init; }
}

public record FollowUpEmailSent
{
    public Guid SubscriberId { get; init; }

    public string Email { get; init; }
}
