using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightShipArena.Assets.Scripts.MessageBroker
{
    public partial class Messenger : IOrchestrationEventsPublisher, IOrchestrationEventsMessenger
    {
        OrchestrationStarted IOrchestrationEventsMessenger.OrchestrationStarted { get; } = new OrchestrationStarted();

        OrchestrationCancelled IOrchestrationEventsMessenger.OrchestrationCancelled { get; } = new OrchestrationCancelled();

        OrchestrationComplete IOrchestrationEventsMessenger.OrchestrationComplete { get; } = new OrchestrationComplete();

        void IOrchestrationEventsPublisher.PublishOrchestrationCancelled(object publisher, string target)
        {
            (this as IOrchestrationEventsMessenger).OrchestrationCancelled.Invoke(publisher, target);
        }

        void IOrchestrationEventsPublisher.PublishOrchestrationStarted(object publisher, string target)
        {
            (this as IOrchestrationEventsMessenger).OrchestrationStarted.Invoke(publisher, target);
        }

        void IOrchestrationEventsPublisher.PublishOrchestratorComplete(object publisher, string target)
        {
            (this as IOrchestrationEventsMessenger).OrchestrationComplete.Invoke(publisher, target);
        }
    }
}
