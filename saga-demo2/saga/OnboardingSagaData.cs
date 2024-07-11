using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;

namespace saga_demo2.saga
{
    public class OnboardingSagaData : SagaStateMachineInstance
    {
        public Guid CorrelationId { get ; set ; }

        public string CurrentState { get; set; }

        public Guid SubscriberId { get; set; }
        public string Email { get; set; }

        public bool WelcomeEmailSent { get; set; }

        public bool FollowUpEmailSent { get; set; }

        public bool OnboardingCompleted { get; set; }
        public DateTime SubscribedAt { get; set; }
 
 
    }
}