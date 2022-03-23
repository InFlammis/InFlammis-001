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
        /// <summary>
        /// Event raised when the enemy dies
        /// </summary>
        event Action<IEnemyControllerCore> HasDied;

        /// <summary>
        /// Reference to the Player controller core
        /// </summary>
        IPlayerControllerCore PlayerControllerCore { get; set; }

        /// <summary>
        /// Reference to the EnemyController parent
        /// </summary>
        IEnemyController Parent { get; }

        /// <summary>
        /// Quick reference to the Transform
        /// </summary>
        Transform Transform { get; }

        /// <summary>
        /// Quick reference to the Rigidbody
        /// </summary>
        Rigidbody2D Rigidbody { get; }

        /// <summary>
        /// Settings for the Enemy
        /// </summary>
        EnemySettings InitSettings { get; }

        /// <summary>
        /// Instance of the HealthManager for the enemy
        /// </summary>
        public IHealthManager HealthManager { get; }

        /// <summary>
        /// Method invoked on Start
        /// </summary>
        void OnStart();

        /// <summary>
        /// Move the enemy
        /// </summary>
        void Move();

        /// <summary>
        /// Handle collisions with player
        /// </summary>
        void HandleCollisionWithPlayer();
    }
}
