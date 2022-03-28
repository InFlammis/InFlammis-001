using FightShipArena.Assets.Scripts.MessageBroker.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FightShipArena.Assets.Scripts.MessageBroker
{
    public partial interface IMessenger : IHealthManagerEventsPublisher, IHealthManagerEventsMessenger { }

    public partial class Messenger
    {
        [SerializeField] private HasDied _HealthManager_HasDied = new HasDied();
        [SerializeField] private HealthLevelChanged _HealthManager_HealthLevelChanged = new HealthLevelChanged();

        HasDied IHealthManagerEventsMessenger.HasDied { get; } = new HasDied();
        HealthLevelChanged IHealthManagerEventsMessenger.HealthLevelChanged { get; } = new HealthLevelChanged();

        void IHealthManagerEventsPublisher.PublishHasDied(object publisher, string target)
        {
            (this as IHealthManagerEventsMessenger).HasDied.Invoke(publisher, target);
        }

        void IHealthManagerEventsPublisher.PublishHealthLevelChanged(object publisher, string target, int healthLevel, int maxHealthLevel)
        {
            (this as IHealthManagerEventsMessenger).HealthLevelChanged.Invoke(publisher, target, healthLevel, maxHealthLevel);
        }
    }
}
