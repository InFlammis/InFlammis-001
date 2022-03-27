using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;

namespace FightShipArena.Assets.Scripts.MessageBroker
{
    [Serializable] public class ScoreChanged : UnityEvent<object, string, int> { }
    [Serializable] public class MultiplierChanged : UnityEvent<object, string, int> { }
    [Serializable] public class HiScoreChanged : UnityEvent<object, string, int> { }

    public interface IScoreManagerEventsPublisher: IEventPublisher
    {
        void PublishScoreChanged(object publisher, string target, int score);
        void PublishMultiplierChanged(object publisher, string target, int multiplier);
        void PublishHiScoreChanged(object publisher, string target, int hiScore);
    }

    public interface IScoreManagerEventsMessenger : IEventMessenger
    {
        ScoreChanged ScoreChanged { get; }
        MultiplierChanged MultiplierChanged { get; }
        HiScoreChanged HiScoreChanged { get; }
    }
}
