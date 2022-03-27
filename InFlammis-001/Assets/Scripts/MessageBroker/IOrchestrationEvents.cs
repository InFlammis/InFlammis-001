﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;

namespace FightShipArena.Assets.Scripts.MessageBroker
{
    [Serializable] public class OrchestrationStarted : UnityEvent<object, string> { }
    [Serializable] public class OrchestrationCancelled : UnityEvent<object, string> { }
    [Serializable] public class OrchestrationComplete : UnityEvent<object, string> { }

    public interface IOrchestrationEventsPublisher : IEventPublisher
    {
        void PublishOrchestrationStarted(object publisher, string target);
        void PublishOrchestrationCancelled(object publisher, string target);
        void PublishOrchestratorComplete(object publisher, string target);
    }

    public interface IOrchestrationEventsSubscriber : IEventSubscriber
    {
        void OrchestrationStarted(object publisher, string target);
        void OrchestrationCancelled(object publisher, string target);
        void OrchestrationComplete(object publisher, string target);
    }

    public interface IOrchestrationEventsMessenger
    {
        OrchestrationStarted OrchestrationStarted { get; }
        OrchestrationCancelled OrchestrationCancelled { get; }
        OrchestrationComplete OrchestrationComplete { get; }
    }
}
