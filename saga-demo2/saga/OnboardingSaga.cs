using MassTransit;
using saga_demo2.Messages;

namespace saga_demo2.saga
{
    public class OnboardingSaga: MassTransitStateMachine<OnboardingSagaData>
    {
        // States
        public State State1 { get; private set; }
        public State State2 { get; private set; }
        public State State3 { get; private set; }

        // Events
        public Event<Message1Sent> Event1 { get; private set; }
        public Event<Message2Sent> Event2 { get; private set; }
        public Event<Message3Sent> Event3 { get; private set; }

        public OnboardingSaga()
        {
            InstanceState(x => x.CurrentState);

            Event(() => Event1, x => x.CorrelateById(context => context.Message.SubscriberId));
            Event(() => Event2, x => x.CorrelateById(context => context.Message.SubscriberId));
            Event(() => Event3, x => x.CorrelateById(context => context.Message.SubscriberId));

            Initially(
                When(Event1)
                    .Then(context =>
                    {
                        context.Saga.SubscriberId = context.Message.SubscriberId;
                        context.Saga.Email = context.Message.Email;
                    })
                    .TransitionTo(State1)
                    .Publish(context => new Message1(context.Message.SubscriberId, context.Message.Email))
            );

            During(State1,
                When(Event2)
                    .Then(context => context.Saga.IsStep1Done = true)
                    .TransitionTo(State2)
                    .Publish(context => new Message2(context.Message.SubscriberId, context.Message.Email))
            );

            During(State2,
                When(Event3)
                    .Then(context => context.Saga.IsStep2Done = true)
                    .TransitionTo(State3)
                    .Publish(context => new Message3(context.Message.SubscriberId, context.Message.Email))
                    .Finalize()
            );
        }

    }
}