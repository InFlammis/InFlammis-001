﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightShipArena.Assets.Scripts.Weapons;
using UnityEngine;

namespace FightShipArena.Assets.Scripts.Player
{
    /// <summary>
    /// Configuration for the Player
    /// </summary>
    [CreateAssetMenu(fileName = "New Player settings", menuName = "Player/Player InitSettings")]
    public class PlayerSettings : ScriptableObject
    {
        /// <summary>
        /// Initial health
        /// </summary>
        public int InitHealth;

        /// <summary>
        /// Damage applied to enemies on collision
        /// </summary>
        public int DamageAppliedOnCollision;

        /// <summary>
        /// Max speed on movements
        /// </summary>
        [Range(0, 10)]
        public float MaxSpeed;

        /// <summary>
        /// Deceleration value when stopping
        /// </summary>
        [Range(0, 1)]
        public float Deceleration;

        /// <summary>
        /// Factor that controls the amount of force applied during movements
        /// </summary>
        public float ForceMultiplier;

        /// <summary>
        /// Settings for weapons
        /// </summary>
        public List<WeaponSettings> WeaponSettings;
    }
}
