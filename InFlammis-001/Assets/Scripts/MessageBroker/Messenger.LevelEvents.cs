using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightShipArena.Assets.Scripts.MessageBroker
{
    public partial class Messenger : ILevelEventsPublisher, ILevelEventsMessenger
    {
        GameOver ILevelEventsMessenger.GameOver { get; } = new GameOver();

        GameStarted ILevelEventsMessenger.GameStarted { get; } = new GameStarted();

        void ILevelEventsPublisher.PublishGameOver(object publisher, string target)
        {
            (this as ILevelEventsMessenger).GameOver.Invoke(publisher, target);
        }

        void ILevelEventsPublisher.PublishGameStarted(object publisher, string target)
        {
            (this as ILevelEventsMessenger).GameStarted.Invoke(publisher, target);
        }
    }
}
