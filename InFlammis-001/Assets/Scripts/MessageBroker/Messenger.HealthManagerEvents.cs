using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightShipArena.Assets.Scripts.MessageBroker
{
    public partial class Messenger : IHealthManagerEventsPublisher, IHealthManagerEventsMessenger
    {
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
