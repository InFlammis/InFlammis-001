using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FightShipArena.Assets.Scripts.MessageBroker
{
    public partial class Messenger : MonoBehaviour
    {
    }

    public interface IEventPublisher
    {
    }
    public interface IEventMessenger
    {

    }

    public interface IEventSubscriber
    {
        Messenger Messenger { get; }
    }
}
