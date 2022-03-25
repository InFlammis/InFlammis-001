﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FightShipArena.Assets.Scripts.PowerUps
{
    /// <summary>
    /// Settings for a power-up
    /// </summary>
    [CreateAssetMenu(fileName = "New Power-Up InitSettings", menuName = "Power-Ups/Power-Up InitSettings")]
    public class PowerUpSettings : ScriptableObject
    {
        /// <summary>
        /// Power-up type
        /// </summary>
        public PowerUpType PowerUpType;

        /// <summary>
        /// Value carried over
        /// </summary>
        public float Value;

        /// <summary>
        /// Lifetime
        /// </summary>
        public float Duration;
    }
}
