using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightShipArena.Assets.Scripts.MessageBroker
{
    public partial class Messenger : IEnemyEventsPublisher, IEnemyEventsMessenger
    {
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
