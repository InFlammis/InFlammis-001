using FightShipArena.Assets.Scripts.MessageBroker.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FightShipArena.Assets.Scripts.MessageBroker
{
    public partial interface IMessenger : IEnemyEventsPublisher, IEnemyEventsMessenger { }

    public partial class Messenger
    {
        [SerializeField] private HasDied _Enemy_HasDied = new HasDied();
        [SerializeField] private PlayerScored _Enemy_PlayerScored = new PlayerScored();

        HasDied IEnemyEventsMessenger.HasDied { get; } = new HasDied();
        PlayerScored IEnemyEventsMessenger.PlayerScored { get; } = new PlayerScored();

        void IEnemyEventsPublisher.PublishHasDied(object publisher, string target)
        {
            (this as IEnemyEventsMessenger).HasDied.Invoke(publisher, target);
        }

        void IEnemyEventsPublisher.PublishPlayerScored(object publisher, string target, int score)
        {
            (this as IEnemyEventsMessenger).PlayerScored.Invoke(publisher, target, score);
        }
    }
}
