using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FightShipArena.Assets.Scripts.MessageBroker
{
    public partial class Messenger : IScoreManagerEventsPublisher, IScoreManagerEventsMessenger
    {
        [SerializeField] private ScoreChanged _ScoreManager_ScoreChanged = new ScoreChanged();
        [SerializeField] private MultiplierChanged _ScoreManager_MultiplierChanged = new MultiplierChanged();
        [SerializeField] private HiScoreChanged _ScoreManager_HiScoreChanged = new HiScoreChanged();

        ScoreChanged IScoreManagerEventsMessenger.ScoreChanged => _ScoreManager_ScoreChanged;

        MultiplierChanged IScoreManagerEventsMessenger.MultiplierChanged => _ScoreManager_MultiplierChanged;

        HiScoreChanged IScoreManagerEventsMessenger.HiScoreChanged => _ScoreManager_HiScoreChanged;

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
