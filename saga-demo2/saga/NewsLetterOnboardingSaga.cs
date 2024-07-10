using MassTransit;
using saga_demo2.Messages;

namespace saga_demo2.saga
{
    public class NewsLetterOnboardingSaga: MassTransitStateMachine<NewsLetterOnboardingSagaData>
    {
        public State Welcoming { get; private set; }
        public State FollowingUp { get; private set; }
        public State Onboarding { get; private set; }

        public Event<SubscriberCreated> SubscriberCreated { get; set; }
        public Event<WelcomeEmailSent> WelcomeEmailSent { get; set; }
        public Event<FollowUpEmailSent> FollowUpEmailSent { get; set; }

        public NewsLetterOnboardingSaga()
        {
            InstanceState(x => x.CurrentState);

            Event(() => SubscriberCreated, x => x.CorrelateById(context => context.Message.SubscriberId));
            Event(() => WelcomeEmailSent, x => x.CorrelateById(context => context.Message.SubscriberId));
            Event(() => FollowUpEmailSent, x => x.CorrelateById(context => context.Message.SubscriberId));

            Initially(
                When(SubscriberCreated)
                    .Then(context =>
                    {
                        context.Saga.SubscriberId = context.Message.SubscriberId;
                        context.Saga.Email = context.Message.Email;
                    })
                    .TransitionTo(Welcoming)
                    .Publish(context => new SendWelcomeEmail(context.Message.SubscriberId, context.Message.Email))
            );

            During(Welcoming,
                When(WelcomeEmailSent)
                    .Then(context => context.Saga.WelcomeEmailSent = true)
                    .TransitionTo(FollowingUp)
                    .Publish(context => new SendFollowUpEmail(context.Message.SubscriberId, context.Message.Email))
            );

            During(FollowingUp,
                When(FollowUpEmailSent)
                    .Then(context => context.Saga.FollowUpEmailSent = true)
                    .TransitionTo(Onboarding)
                    .Publish(context => new OnboardingCompleted(context.Message.SubscriberId, context.Message.Email))
                    .Finalize()
            );
        }

    }
}