using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightShipArena.Assets.Scripts.MessageBroker
{
    public partial class Messenger : IScoreManagerEventsPublisher, IScoreManagerEventsMessenger
    {
        ScoreChanged IScoreManagerEventsMessenger.ScoreChanged { get; } = new ScoreChanged();

        MultiplierChanged IScoreManagerEventsMessenger.MultiplierChanged { get; } = new MultiplierChanged();

        HiScoreChanged IScoreManagerEventsMessenger.HiScoreChanged { get; } = new HiScoreChanged();

        void IScoreManagerEventsPublisher.PublishHiScoreChanged(object publisher, string target, int hiScore)
        {
            (this as IScoreManagerEventsMessenger).HiScoreChanged.Invoke(publisher, target, hiScore);
        }

        void IScoreManagerEventsPublisher.PublishMultiplierChanged(object publisher, string target, int multiplier)
        {
            (this as IScoreManagerEventsMessenger).MultiplierChanged.Invoke(publisher, target, multiplier);
        }

        void IScoreManagerEventsPublisher.PublishScoreChanged(object publisher, string target, int score)
        {
            (this as IScoreManagerEventsMessenger).ScoreChanged.Invoke(publisher, target, score);
        }
    }
}
