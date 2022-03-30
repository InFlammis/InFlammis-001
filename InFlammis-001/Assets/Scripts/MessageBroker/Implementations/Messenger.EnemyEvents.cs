﻿using FightShipArena.Assets.Scripts.MessageBroker.Events;
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

        HasDied IEnemyEventsMessenger.HasDied => _Enemy_HasDied;
        PlayerScored IEnemyEventsMessenger.PlayerScored => _Enemy_PlayerScored;

        void IEnemyEventsPublisher.PublishHasDied(object publisher, string target)
        {
            _Enemy_HasDied.Invoke(publisher, target);
        }

        void IEnemyEventsPublisher.PublishPlayerScored(object publisher, string target, int score)
        {
            _Enemy_PlayerScored.Invoke(publisher, target, score);
        }
    }
}
