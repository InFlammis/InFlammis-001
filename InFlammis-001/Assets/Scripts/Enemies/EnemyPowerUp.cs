﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Scripting;

namespace FightShipArena.Assets.Scripts.Enemies
{
    /// <summary>
    /// Base class modeling a power-up
    /// </summary>
    [Serializable]
    public class EnemyPowerUp
    {
        /// <summary>
        /// Instance of a PowerUp GameObject
        /// </summary>
        [RequiredMember]
        public GameObject PowerUp;

        /// <summary>
        /// Probability this power up is spawned on enemy's destruction.
        /// </summary>
        [Range(0, 1)]
        public float ReleaseRate;
    }
}
