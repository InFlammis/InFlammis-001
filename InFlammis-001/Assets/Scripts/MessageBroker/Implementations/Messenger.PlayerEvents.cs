﻿using FightShipArena.Assets.Scripts.MessageBroker.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FightShipArena.Assets.Scripts.MessageBroker
{
    public partial interface IMessenger : IPlayerEventsPublisher, IPlayerEventsMessenger { }

    public partial class Messenger
    {
        [SerializeField] private HasDied _Player_HasDied = new HasDied();
        [SerializeField] private ScoreMultiplierCollected _Player_ScoreMultiplierCollected;
        [SerializeField] private HealthLevelChanged _Player_HealthLevelChanged;


        HasDied IPlayerEventsMessenger.HasDied  => _Player_HasDied;
        ScoreMultiplierCollected IPlayerEventsMessenger.ScoreMultiplierCollected => _Player_ScoreMultiplierCollected;

        HealthLevelChanged IPlayerEventsMessenger.HealthLevelChanged => _Player_HealthLevelChanged;

        void IPlayerEventsPublisher.PublishHealthLevelChanged(object publisher, string target, int healthLevel, int maxHealthLevel)
        {
            _Player_HealthLevelChanged.Invoke(publisher, target,healthLevel, maxHealthLevel);
        }

        void IPlayerEventsPublisher.PublishHasDied(object publisher, string target)
        {
            _Player_HasDied.Invoke(publisher, target);
        }

        void IPlayerEventsPublisher.PublishScoreMultiplierCollected(object publisher, string target, int scoreMultiplier)
        {
            _Player_ScoreMultiplierCollected?.Invoke(publisher, target, scoreMultiplier);
        }
    }
}
