using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;

namespace FightShipArena.Assets.Scripts.MessageBroker
{
    [Serializable] public class SetCentralMessage : UnityEvent<object, string, string> { }

    public interface IHudEventsPublisher : IEventPublisher
    {
        void PublishSetCentralMessage(object publisher, string target, string message);
    }

    public interface IHudEventsMessenger : IEventMessenger
    {
        SetCentralMessage SetCentralMessage { get; }
    }
}
