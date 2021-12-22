﻿using FightShipArena.Assets.Scripts.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightShipArena.Assets.Scripts.Managers.HealthManagement;
using UnityEngine;

namespace FightShipArena.Assets.Scripts.Enemies
{
    public interface IEnemyControllerCore
    {
        IPlayerControllerCore PlayerControllerCore { get; set; }
        IMyMonoBehaviour Parent { get; }
        Transform Transform { get; } 
        EnemySettings InitSettings { get; }
        public IHealthManager HealthManager { get; }

        void Move();
        void HandleCollisionWithPlayer();
        void HandleCollisionWithBullet();
    }
}