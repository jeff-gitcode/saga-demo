using MassTransit;
using saga_demo2.Messages;

public class SubscribeToNewsletterHandler(AppDbContext appDbContext) : IConsumer<SubscribeToNewsletter>
{
    public async Task Consume(ConsumeContext<SubscribeToNewsletter> context)
    {
        var subscriber = appDbContext.Subscribers.Add(new Subscriber
        {
            Id = Guid.NewGuid(),
            Email = context.Message.Email,
            SubscribedAt = DateTime.UtcNow
        });

        await appDbContext.SaveChangesAsync();

        await context.Publish(new SubscriberCreated
        {
            SubscriberId = subscriber.Entity.Id,
            Email = subscriber.Entity.Email
        });
    }
}

public class SendWelcomeEmailHandler : IConsumer<SendWelcomeEmail>
{
    public async Task Consume(ConsumeContext<SendWelcomeEmail> context)
    {
        await Console.Out.WriteLineAsync($"Sending welcome email to {context.Message.Email}");

        await context.Publish(new WelcomeEmailSent
        {
            SubscriberId = context.Message.SubscriberId,
            Email = context.Message.Email
        });
    }
}

public class SendFollowUpEmailHandler : IConsumer<SendFollowUpEmail>
{
    public async Task Consume(ConsumeContext<SendFollowUpEmail> context)
    {
        await Console.Out.WriteLineAsync($"Sending follow up email to {context.Message.Email}");

        await context.Publish(new FollowUpEmailSent
        {
            SubscriberId = context.Message.SubscriberId,
            Email = context.Message.Email
        });
    }
}

public class OnboardingCompletedHandler : IConsumer<OnboardingCompleted>
{
    public Task Consume(ConsumeContext<OnboardingCompleted> context)
    {
        Console.Out.WriteLine($"Onboarding completed for {context.Message.Email}");

        return Task.CompletedTask;
    }
}