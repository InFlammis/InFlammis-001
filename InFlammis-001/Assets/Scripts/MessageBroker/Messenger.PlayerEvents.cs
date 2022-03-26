using FightShipArena.Assets.Scripts.MessageBroker.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightShipArena.Assets.Scripts.MessageBroker
{
    public partial class Messenger : IPlayerEventsPublisher, IPlayerEventsMessenger
    {
        HasDied IPlayerEventsMessenger.HasDied { get; } = new HasDied();
        ScoreMultiplierCollected IPlayerEventsMessenger.ScoreMultiplierCollected { get; } = new ScoreMultiplierCollected();

        HealthLevelChanged IPlayerEventsMessenger.HealthLevelChanged { get; } = new HealthLevelChanged();

        void IPlayerEventsPublisher.PublishHealthLevelChanged(object publisher, string target, int healthLevel, int maxHealthLevel)
        {
            (this as IPlayerEventsMessenger).HealthLevelChanged.Invoke(publisher, target,healthLevel, maxHealthLevel);
        }

        void IPlayerEventsPublisher.PublishHasDied(object publisher, string target)
        {
            (this as IPlayerEventsMessenger).HasDied.Invoke(publisher, target);
        }

        void IPlayerEventsPublisher.PublishScoreMultiplierCollected(object publisher, string target, int scoreMultiplier)
        {
            (this as IPlayerEventsMessenger).ScoreMultiplierCollected?.Invoke(publisher, target, scoreMultiplier);
        }
    }
}
