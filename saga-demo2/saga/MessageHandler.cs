using MassTransit;
using saga_demo2.Messages;

public class Message0Handler(AppDbContext appDbContext) : IConsumer<Message0>
{
    public async Task Consume(ConsumeContext<Message0> context)
    {
        var subscriber = appDbContext.Subscribers.Add(new Subscriber
        {
            Id = Guid.NewGuid(),
            Email = context.Message.Email,
            SubscribedAt = DateTime.UtcNow
        });

        await appDbContext.SaveChangesAsync();

        await context.Publish(new Message1Sent
        {
            SubscriberId = subscriber.Entity.Id,
            Email = subscriber.Entity.Email
        });
    }
}

public class Message1Handler : IConsumer<Message1>
{
    public async Task Consume(ConsumeContext<Message1> context)
    {
        await Console.Out.WriteLineAsync($"Sending welcome email to {context.Message.Email}");

        await context.Publish(new Message2Sent
        {
            SubscriberId = context.Message.SubscriberId,
            Email = context.Message.Email
        });
    }
}

public class Message2Handler : IConsumer<Message2>
{
    public async Task Consume(ConsumeContext<Message2> context)
    {
        await Console.Out.WriteLineAsync($"Sending follow up email to {context.Message.Email}");

        await context.Publish(new Message3Sent
        {
            SubscriberId = context.Message.SubscriberId,
            Email = context.Message.Email
        });
    }
}

public class Message3Handler : IConsumer<Message3>
{
    public Task Consume(ConsumeContext<Message3> context)
    {
        Console.Out.WriteLine($"Onboarding completed for {context.Message.Email}");

        return Task.CompletedTask;
    }
}