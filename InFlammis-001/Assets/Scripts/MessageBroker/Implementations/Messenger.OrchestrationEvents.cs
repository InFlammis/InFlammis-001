using FightShipArena.Assets.Scripts.MessageBroker.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FightShipArena.Assets.Scripts.MessageBroker
{
    public partial interface IMessenger : IOrchestrationEventsPublisher, IOrchestrationEventsMessenger { }

    public partial class Messenger
    {
        [SerializeField] private OrchestrationStarted _Orchestration_OrchestrationStarted = new OrchestrationStarted();
        [SerializeField] private OrchestrationCancelled _Orchestration_OrchestrationCancelled = new OrchestrationCancelled();
        [SerializeField] private OrchestrationComplete _Orchestration_OrchestrationComplete = new OrchestrationComplete();

        OrchestrationStarted IOrchestrationEventsMessenger.OrchestrationStarted => _Orchestration_OrchestrationStarted;
        OrchestrationCancelled IOrchestrationEventsMessenger.OrchestrationCancelled => _Orchestration_OrchestrationCancelled;
        OrchestrationComplete IOrchestrationEventsMessenger.OrchestrationComplete => _Orchestration_OrchestrationComplete;

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
